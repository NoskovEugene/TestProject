using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        public Window OwnerWindow { get; set; }

        public bool TryGetTypedOwner<T>(out T owner)
            where T : Window
        {
            owner = default;
            if(OwnerWindow.GetType() == typeof(T))
            {
                owner = (T)OwnerWindow;
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public T GetTypedOwner<T>()
            where T: Window
        {
            TryGetTypedOwner<T>(out var owner);
            return owner;
        }
    }
}
