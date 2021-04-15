using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Effects;

namespace Client.Extensions
{
    public static class WindowExtensions
    {
        public static Window CreateAsModal(this Window target, Window parent)
        {
            target.Owner = parent;
            parent.Effect = new BlurEffect();
            return target;
        }

        public static void SetDataContext<T>(this Window window, T context)
            where T : ViewModelBase
        {
            context.OwnerWindow = window;
            window.DataContext = context;
        }

        public static void RestoreOwner(this Window target)
        {
            if (target.Owner == null)
            {
                throw new Exception("Owner in modal cannot be null");
            }
            target.Owner.Effect = null;
        }

        public static T GetDataContext<T>(this Window target)
        {
            if (target.DataContext == null)
            {
                throw new Exception("DataContext cannot be null");
            }

            if (target.DataContext.GetType() != typeof(T))
            {
                throw new Exception($"Type DataContext - {target.GetType()} but need type ${typeof(T)}");
            }

            return (T)target.DataContext;
        }
    }
}
