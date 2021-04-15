using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Prism.Ioc;


namespace Client.Extensions
{
    public static class ContainerExtensions
    {
        public static IContainerRegistry Configure<T>(this IContainerRegistry containerRegistry, IConfigurationSection section)
            where T: class
        {
            var obj = section.Get<T>();
            var option = Options.Create<T>(obj);
            containerRegistry.RegisterInstance<IOptions<T>>(option);
            return containerRegistry;
        }

        public static IContainerRegistry RegisterConverter<T>(this IContainerRegistry containerRegistry, string converterName)
            where T: IValueConverter
        {
            containerRegistry.Register<IValueConverter, T>(converterName);
            return containerRegistry;
        }

        public static IContainerRegistry RegisterConverter<T>(this IContainerRegistry containerRegistry)
            where T : IValueConverter
        {
            var name = typeof(T).Name;
            containerRegistry.Register<IValueConverter, T>(name);
            return containerRegistry;
        }

        public static IContainerRegistry AddAutoMapper(this IContainerRegistry containerRegistry, Action<MapperConfigurationExpression> buildAciton)
        {
            var expression = new MapperConfigurationExpression();
            buildAciton(expression);
            var mapper = new MapperConfiguration(expression).CreateMapper();
            containerRegistry.RegisterInstance<IMapper>(mapper);
            return containerRegistry;
        }

    }
}
