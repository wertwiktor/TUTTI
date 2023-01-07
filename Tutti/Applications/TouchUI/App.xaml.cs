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
using TouchUI.Dialogs.UserExit;
using TouchUI.Services.Navigation;
using System.Linq;
using TouchUI.ViewModels;
using System.Collections.Generic;

namespace TouchUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ILogger _logger;
        private IContainer _diContainer;
         
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitializeLogger();
            InitializeDependencyInjectionContainer();
            RegisterViewModelsInNavigationService();
            InitializeDevelopersWindow();
            InitializeMainWindow();
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
            builder.RegisterType<UserExitDialogController>().As<IUserExitDialogController>().SingleInstance();
            //Frontend services
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            //ViewModels
            builder.RegisterType<MainViewModel>().SingleInstance();
            builder.RegisterType<HomeViewModel>().SingleInstance();

            _diContainer = builder.Build();
        }

        private void RegisterViewModelsInNavigationService()
        {
            var navigationService = _diContainer.Resolve<INavigationService>();
            var registeredViewModelsTypes = _diContainer.ComponentRegistry.Registrations.Where(r => typeof(ViewModelBase).IsAssignableFrom(r.Activator.LimitType)).Select(r => r.Activator.LimitType);
            foreach (var viewModelType in registeredViewModelsTypes)
            {
                var viewModel = _diContainer.Resolve(viewModelType);
            }

        }

        private void InitializeDevelopersWindow()
        {
            var devWindow = new DevelopersWindow();
            devWindow.Show();
        }

        private void InitializeMainWindow()
        {
            var mainWindow = new MainWindow();
            mainWindow.DataContext = _diContainer.Resolve<MainViewModel>();
            InitializeDefaultView();
            mainWindow.Show();
        }

        private void InitializeDefaultView()
        {
            var mainViewModel = _diContainer.Resolve<MainViewModel>();
            mainViewModel.CurrentViewModel = _diContainer.Resolve<HomeViewModel>();
        }

    }
}
