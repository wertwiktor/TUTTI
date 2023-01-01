using Autofac;
using Autofac.Features.ResolveAnything;
using Services.IdentificationDeviceService;
using Services.IdentificationDeviceServiceBaltech;
using Services.DataService;
using Services.DataServiceSql;
using System.Windows;
using TouchUI.ViewModels;
using Services.IdentificationDeviceService;

namespace TouchUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterType<DataServiceSql>().As<IDataService>().SingleInstance();
            builder.RegisterType<IdentificationDeviceServiceBaltech>().As<IIdentificationDeviceService>().SingleInstance();

            IContainer container = builder.Build();

            DISource.Resolver = (type) => { return container.Resolve(type); };
        }
    }
}
