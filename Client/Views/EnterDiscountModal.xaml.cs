using Client.Extensions;
using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для EnterPriceModal.xaml
    /// </summary>
    public partial class EnterDiscountModal : Window
    {
        public EnterDiscountModal(EnterDiscountModalViewModel viewModel)
        {
            InitializeComponent();
            this.SetDataContext(viewModel);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.RestoreOwner();
            var context = this.GetDataContext<EnterDiscountModalViewModel>();
            this.DialogResult = context.ModalResult;
        }
    }
}
