using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace WallSurface.View {
    public partial class MenuWindow {
        public MenuWindow() { InitializeComponent(); }

#region Control header buttons
        private void BtnClose_OnClick(object sender, RoutedEventArgs e) { Application.Current.Shutdown(); }
        private void BtnFullWindow_OnClick(object sender, RoutedEventArgs e) {
            WindowState = WindowState == WindowState.Maximized
                              ? WindowState = WindowState.Normal
                              : WindowState = WindowState.Maximized;
        }
        private void BtnHide_OnClick(object sender, RoutedEventArgs e) { WindowState = WindowState.Minimized; }
        private void MoveGrid_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) { DragMove(); }
#endregion

#region Body buttons
        private void OpenFile_OnClick(object sender, RoutedEventArgs e) {
            var openFileDialog = new OpenFileDialog {
                Filter = "Image Files(*.BMP;*.JPG;*JPEG;*.GIF;*PNG)|*.BMP;*.JPG;*.JPEG;*.GIF;*PNG|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == false) return;
            Close();
            if (new ViewWindow(openFileDialog.FileName).ShowDialog() == true) App.ServiceProvider.GetService<MenuWindow>()?.Show();
        }
        private void OpenFolder_OnClick(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }
        private void ScanAllPC_OnClick(object sender, RoutedEventArgs e) { throw new System.NotImplementedException(); }
        private void SearchInternet_OnClick(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }
#endregion
    }
}