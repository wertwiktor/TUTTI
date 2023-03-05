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
using TouchUI.ViewModels;
using TouchUI.Services.Login;
using Framework.ExtensionMethods;

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
            var container = InitializeDependencyInjectionContainer();
            InitializeDevelopersWindow(container);
            InitializeMainWindow(container);
        }

        private void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File(CreateLoggingFilePath(),
                                                                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                                                                shared: true,
                                                                rollingInterval: RollingInterval.Day).CreateLogger();
            _logger = Log.Logger.ForContext<App>();
            _logger.Information("Starting the application.".Here());
        }

        private string CreateLoggingFilePath()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TUTTI\\Logs\\Log.log");
            return path;
        }

        private IContainer InitializeDependencyInjectionContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            //Backend services
            builder.RegisterType<DataServiceSql>().As<IDataService>().SingleInstance();
            builder.RegisterType<IdentificationDeviceServiceBaltech>().As<IIdentificationDeviceService>().SingleInstance();
            //Frontend services
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<LoginService>().As<ILoginService>().SingleInstance();
            //ViewModels
            builder.RegisterType<MainViewModel>().SingleInstance();
            builder.RegisterType<HomeViewModel>().InstancePerDependency();
            builder.RegisterType<RegisterViewModel>().InstancePerDependency();
            builder.RegisterType<HistoryViewModel>().InstancePerDependency();
            builder.RegisterType<ExportViewModel>().InstancePerDependency();

            return builder.Build();
        }

        private void InitializeDevelopersWindow(ILifetimeScope scope)
        {
#if DEBUG
            var devWindow = new DevelopersWindow();
            using (var localScope = scope.BeginLifetimeScope())
            {
                devWindow.DataContext = localScope.Resolve<DevelopersWindowViewModel>();
            }
            devWindow.Show();
#endif
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
