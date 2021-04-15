using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Client.Converters.Base
{
    public abstract class ResolverBase : MarkupExtension
    {
        public static Func<Type, object> Resolve { get; set; }

        public static Func<Type, string, object> NamedResolve { get; set; }

        public static T ResolveType<T>(string name = null)
        {
            return name != null ? (T)NamedResolve(typeof(T), name) : (T)Resolve(typeof(T));
        }
    }
}
