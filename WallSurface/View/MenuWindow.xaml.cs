using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using WPFFolderBrowser;

namespace WallSurface.View {
    public partial class MenuWindow {
        public MenuWindow() { InitializeComponent(); }

#region Control header buttons
        private void BtnClose_OnClick(object sender, RoutedEventArgs e) {
            if (new ResponseExit().ShowDialog() == false) { return; }
            Application.Current.Shutdown();
        }
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
            if (new ViewWindow(openFileDialog.FileName).ShowDialog() == true)
                App.ServiceProvider.GetService<MenuWindow>()?.Show();
        }
        private void OpenFolder_OnClick(object sender, RoutedEventArgs e) {
            var folderBrowserDialog = new WPFFolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == false) return;
            var listImage = new List<string>();
            listImage.AddRange(from item in Directory.GetFiles(folderBrowserDialog.FileName)
                               where Path.GetExtension(item) == ".png" || Path.GetExtension(item) == ".jpg" ||
                                     Path.GetExtension(item) == ".gif" || Path.GetExtension(item) == ".bmp" ||
                                     Path.GetExtension(item) == ".jpeg"
                               select item);
            Close();
            if (new AlbumWindow(listImage).ShowDialog() == true) App.ServiceProvider.GetService<MenuWindow>()?.Show();
        }
        private void SearchInternet_OnClick(object sender, RoutedEventArgs e) {
            var responseExit = new InputWindow();
            if (responseExit.ShowDialog() == false) return;
            var url =
                $"https://www.google.com/search?q={InputWindow.TextToSearch}&source=lnms&tbm=isch&sa=X&ved=2ahUKEwi1tICUjeLxAhXp-ioKHWwSB9MQ_AUoAXoECAEQAw&biw=1879&bih=980";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") {CreateNoWindow = true});
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                Process.Start("open", url);
            }
        }
#endregion
    }
}