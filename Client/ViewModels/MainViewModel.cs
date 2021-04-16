using Common.Dtos;
using System.Collections.ObjectModel;
using Client.Configuration;
using Prism.Commands;
using Client.InternalModels;
using Client.Extensions;
using System.Windows;
using Client.Web.Repositories;
using System.Threading.Tasks;
using System.ComponentModel;
using Client.BackgoundWorkers;
using AutoMapper;
using Client.Web;
using System.Net.Http;
using Common.Models;
using Notifications.Wpf.Core;
using Notifications.Wpf.Core.Controls;
using System;

namespace Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region BindingFields

        private ObservableCollection<ProductDto> products;
        private ObservableCollection<ObservableCartItem> cartItems;
        private string title;
        private int currentCartId;
        private double finalPrice;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public int CurrentCartId
        {
            get => currentCartId;
            set => SetProperty(ref currentCartId, value);
        }

        public double FinalPrice
        {
            get => finalPrice;
            set => SetProperty(ref finalPrice, value);
        }

        public ObservableCollection<ProductDto> Products
        {
            get => products;
        }

        public ObservableCollection<ObservableCartItem> CartItems
        {
            get => cartItems;
        }

        public DelegateCommand<object> OnProductSelectedCommand =>
            new DelegateCommand<object>(async(data) => await OnProductSelected(data));

        public DelegateCommand<object> OnCartItemRemoveCommand =>
            new DelegateCommand<object>(data => OnCartItemRemove(data));

        public BackgroundWorkerBase ProductWorker { get; set; }

        #endregion

        #region Dependency

        public IAppConfig AppConfig { get; set; }

        public IProductRepository ProductRepository { get; }

        public ITestingRepository TestingRepository { get; }

        public ICartRepository CartRepository { get; }

        public IMapper Mapper { get; set; }

        public NotificationManager NotificationManager { get; }

        #endregion

        public ProductCartDto CurrentCart { get; set; }

        public MainViewModel() { }


        public MainViewModel(IAppConfig appConfig, IProductRepository productRepository, ITestingRepository testingRepository, ICartRepository cartRepository, IMapper mapper)
        {
            Mapper = mapper;
            AppConfig = appConfig;
            ProductRepository = productRepository;
            TestingRepository = testingRepository;
            CartRepository = cartRepository;

            title = "Клиент";
            cartItems = new();
            products = new();
            InitializeWorkers();

            NotificationManager = new NotificationManager(NotificationPosition.BottomRight, App.Current.Dispatcher);
        }

        private void InitializeWorkers()
        {
            ProductWorker = new BackgroundWorkerBase(ProductWorker_DoWork, ProductWorker_OnComplete);
        }



        private async Task OnProductSelected(object data)
        {
            if (data is ProductDto)
            {
                var product = (ProductDto)data;

                if (RequestPrice(product, out var discount))
                {
                    var cartItem = await CartRepository.AddProduct(product.Id, currentCartId, discount);
                    var observableItem = Mapper.Map<CartItemDto, ObservableCartItem>(cartItem.Payload);
                    cartItems.Add(observableItem);
                    FinalPrice += cartItem.Payload.FinalPrice;
                    await NotificationManager.ShowAsync(new NotificationContent
                    {
                        Title = "",
                        Message = "Товар успешно добавлен",
                        Type = NotificationType.Success
                    }, expirationTime: TimeSpan.FromSeconds(2));
                }
            }
        }

        private bool RequestPrice(ProductDto productDto, out int price)
        {
            price = 0;
            var viewModel = new EnterDiscountModalViewModel
            {
                Product = productDto
            };
            var modal = new EnterDiscountModal(viewModel).CreateAsModal(Application.Current.MainWindow);
            var value = modal.ShowDialog().Value;
            price = viewModel.EnteredDiscount;
            return value;
        }

        private async Task OnCartItemRemove(object data)
        {
            if (data is ObservableCartItem)
            {
                var item = (ObservableCartItem)data;
                var result = await CartRepository.RemoveCartItem(CurrentCartId, item.Id);
                FinalPrice = result.Payload.TotalSumWithDiscount;
                cartItems.Remove(item);
                await NotificationManager.ShowAsync(new NotificationContent()
                {
                    Message = "Товар успешно удалён",
                    Type = NotificationType.Success
                }, expirationTime: TimeSpan.FromSeconds(2));
            }
        }

        private async void ProductWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var response = ProductRepository.GetProducts().Result;
            foreach (var item in response)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    products.Add(item);
                });
            }
            CurrentCart = CartRepository.InitCart().Result;
            CurrentCartId = CurrentCart.Id;
            await NotificationManager.ShowAsync(new NotificationContent()
            {
                Message = "Данные успешно загружены",
                Type = NotificationType.Success
            });
        }

        private void ProductWorker_OnComplete(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        public void OnLoaded()
        {
            ProductWorker.RunWorker();
        }
    }
}