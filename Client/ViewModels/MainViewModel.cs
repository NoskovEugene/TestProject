using Prism.Mvvm;
using Common.Dtos;
using System.Collections.ObjectModel;
using Client.Configuration;
using Prism.Commands;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Client.InternalModels;
using System;
using Client.Extensions;
using System.Windows;
using Client.Web;
using Common.RequestModels;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Web.Repositories;

namespace Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region BindingFields

        private ObservableCollection<ProductDto> products;
        private ObservableCollection<ObservableCartItem> cartItems;
        private string title;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
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
            new DelegateCommand<object>(data => OnProductSelected(data));

        public DelegateCommand<object> OnCartItemRemoveCommand =>
            new DelegateCommand<object>(data => OnCartItemRemove(data));

        #endregion

        #region Dependency

        public IAppConfig AppConfig { get; set; }

        public ITestingRepository TestingRepository { get; set; }

        public IProductRepository ProductRepository { get; set; }

        #endregion

        public MainViewModel() { }


        public MainViewModel(IAppConfig appConfig, IProductRepository productRepository, ITestingRepository testingRepository)
        {
            AppConfig = appConfig;
            TestingRepository = testingRepository;
            ProductRepository = productRepository;
            title = "Клиент";
            cartItems = new();
            products = new();
        }

        private void OnProductSelected(object data)
        {
            if (data is ProductDto)
            {
                var product = (ProductDto)data;

                if (RequestPrice(product, out var discount))
                {
                    var cartItem = new ObservableCartItem()
                    {
                        Id = -1,
                        ProductDto = product,
                        EnteredDiscount = discount
                    };
                    cartItems.Add(cartItem);
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

        private void OnCartItemRemove(object data)
        {
            if (data is ObservableCartItem)
            {
                var item = (ObservableCartItem)data;
                cartItems.Remove(item);
            }
        }
    }
}