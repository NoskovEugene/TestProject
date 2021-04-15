using Client.ViewModels;
using Microsoft.Extensions.Configuration;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using Client.Extensions;
using Client.Configuration;
using Client.Converters.Base;
using Client.Web;
using Common.Mapper;
using Client.Web.Repositories;

namespace Client
{
    public class Bootstrapper : PrismBootstrapper
    {
        public IConfiguration Configuration { get; }

        public Bootstrapper(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Configure<ColorConfig>(Configuration.GetSection("ColorConfig"));
            containerRegistry.Configure<NetworkConfiguration>(Configuration.GetSection("Network"));
            containerRegistry.RegisterSingleton<IAppConfig, AppConfig>();
            containerRegistry.Register<MainViewModel>();
            containerRegistry.Register<IWebClient, WebClient>();
            containerRegistry.Register<IProductRepository, ProductRepository>();
            containerRegistry.Register<ITestingRepository, TestingRepository>();
            containerRegistry.Register<ICartRepository, CartRepository>();
            containerRegistry.AddAutoMapper(x =>
            {
                x.AddProfile<AutoMapperProfile>();
            });
            ResolverBase.Resolve = Container.Resolve;
            ResolverBase.NamedResolve = Container.Resolve;
        }
    }
}