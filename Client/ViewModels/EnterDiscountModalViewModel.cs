using Client.Extensions;
using Common.Dtos;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class EnterDiscountModalViewModel : ViewModelBase
    {
        public ProductDto Product { get; set; }

        private string enteredDiscountString;
        public int EnteredDiscount { get; set; }

        public bool ModalResult { get; set; } = false;


        private Visibility errorVisibility = Visibility.Collapsed;

        public Visibility ErrorVisibility
        {
            get => errorVisibility;
            set => SetProperty(ref errorVisibility, value);
        }

        private ObservableCollection<string> errors = new();

        public ObservableCollection<string> Errors
        {
            get => errors;
            set => SetProperty(ref errors, value);
        }

        public DelegateCommand OnAcceptClickCommand => new(() => OnAcceptClick());

        public DelegateCommand OnCancelClickCommand => new(() => OnCancelClick());

        public DelegateCommand<string> OnTextChangedCommand => new(data => OnTextChanged(data));

        public void Validate(bool strict)
        {
            Errors.Clear();
            if (!strict && string.IsNullOrEmpty(enteredDiscountString))
            {
                ErrorVisibility = Visibility.Collapsed;
                RaisePropertyChanged(nameof(Errors));
                return;
            }

            if(!int.TryParse(enteredDiscountString, out var dicount))
            {
                Errors.Add("Поле ввода содержить неверные данные");
            }
            else
            {
                EnteredDiscount = dicount;
            }

            if(EnteredDiscount < 0 || EnteredDiscount > 100)
            {
                Errors.Add("Скидка должна быть больше 0 и меньше 100");
            }

            if(Errors.Count > 0)
            {
                ErrorVisibility = Visibility.Visible;
            }
            else
            {
                ErrorVisibility = Visibility.Collapsed;
            }
            RaisePropertyChanged(nameof(Errors));
        }


        public void OnAcceptClick()
        {
            var window = GetTypedOwner<EnterDiscountModal>();
            Validate(true);
            if (Errors.Count == 0)
            {
                ModalResult = true;
                window.Close();
            }
            else
            {
                ErrorVisibility = Visibility.Visible;
            }
        }

        private void OnCancelClick()
        {
            var window = GetTypedOwner<EnterDiscountModal>();
            window.DialogResult = false;
            window.Close();
        }

        public void OnTextChanged(string data)
        {
            enteredDiscountString = data;
            Validate(false);
        }


    }
}
