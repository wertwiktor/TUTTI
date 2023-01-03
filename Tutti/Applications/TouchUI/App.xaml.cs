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

            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterType<DataServiceSql>().As<IDataService>().SingleInstance();
            builder.RegisterType<IdentificationDeviceServiceBaltech>().As<IIdentificationDeviceService>().SingleInstance();

            IContainer container = builder.Build();

            DISource.Resolver = container.Resolve;
            InitializeDevelopersWindow();
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

        private void InitializeDevelopersWindow()
        {
            var devWindow = new DevelopersWindow();
            devWindow.Show();
        }
    }
}
