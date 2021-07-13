using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WallSurface.View;

namespace WallSurface {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public static IServiceProvider ServiceProvider;
        public App() {
            var servicesCollection = new ServiceCollection();
            ConfigServices(servicesCollection);
            ServiceProvider = servicesCollection.BuildServiceProvider();
        }
        private static void ConfigServices(IServiceCollection serviceProvider) {
            serviceProvider.AddTransient<MenuWindow>();
        }
        private void App_OnStartup(object sender, StartupEventArgs e) {
            var shadowWindow = new Window {Width = 1, Height = 1, Visibility = Visibility.Hidden};
            var menuWindow = ServiceProvider.GetService<MenuWindow>();
            shadowWindow.Show();
            menuWindow?.Show();
        }
    }
}