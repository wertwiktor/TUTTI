using Autofac;
using Autofac.Features.ResolveAnything;
using Services.IdentificationDeviceService;
using Services.IdentificationDeviceServiceBaltech;
using Services.DataService;
using Services.DataServiceSql;
using System.Windows;
using Serilog;
using System;
using System.IO;
using TouchUI.Views;
using TouchUI.Services.Navigation;
using System.Linq;
using TouchUI.ViewModels;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace TouchUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ILogger _logger;
         
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitializeLogger();
            InitializeDependencyInjectionContainer();
            RegisterViewModelsInNavigationService();
        }

        private void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File(CreateLoggingFilePath(),
                                                                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                                                                shared: true, 
                                                                rollingInterval: RollingInterval.Day).CreateLogger();
            _logger = Log.Logger.ForContext<App>();
            _logger.Information("Starting the application.");
        }

        private string CreateLoggingFilePath()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TUTTI\\Logs\\Log.log");
            return path;
        }

        private void InitializeDependencyInjectionContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            //Backend services
            builder.RegisterType<DataServiceSql>().As<IDataService>().SingleInstance();
            builder.RegisterType<IdentificationDeviceServiceBaltech>().As<IIdentificationDeviceService>().SingleInstance();
            //Frontend services
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            //ViewModels
            builder.RegisterType<MainViewModel>().SingleInstance();
            builder.RegisterType<HomeViewModel>().InstancePerDependency();
            builder.RegisterType<RegisterViewModel>().InstancePerDependency();

            var diContainer = builder.Build();
            InitializeDevelopersWindow(diContainer);
            InitializeMainWindow(diContainer);
        }

        private void RegisterViewModelsInNavigationService()
        {
            return;
        }

        private void InitializeDevelopersWindow(ILifetimeScope scope)
        {
            var devWindow = new DevelopersWindow();
            using( var localScope = scope.BeginLifetimeScope())
            {
                devWindow.DataContext = localScope.Resolve<DevelopersWindowViewModel>();
            }
            devWindow.Show();
        }

        private void InitializeMainWindow(ILifetimeScope scope)
        {
            var mainWindow = new MainWindow();
            using (var localScope = scope.BeginLifetimeScope())
            {
                mainWindow.DataContext = localScope.Resolve<MainViewModel>();
                var navigationService = localScope.Resolve<INavigationService>();
                navigationService.Navigate<HomeViewModel>();
            }
            mainWindow.Show();
        }
    }
}
