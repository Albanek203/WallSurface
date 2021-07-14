using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WallSurface.View {
    public partial class ViewWindow {
        public ViewWindow(string path) {
            InitializeComponent();

            ViewPicture.Source          = new BitmapImage(new Uri(path));
            LbSourcePathPicture.Content = path;
            using var imageStream = File.OpenRead(path);
            var decoder = BitmapDecoder.Create(imageStream, BitmapCreateOptions.IgnoreColorProfile
                                             , BitmapCacheOption.Default);
            LbDimensionsPicture.Content = $"{decoder.Frames[0].PixelWidth}x{decoder.Frames[0].PixelHeight}";
        }

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
        private void GoBackButton_OnClick(object sender, RoutedEventArgs e) { DialogResult = true; }
        private void MoveGrid_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) { DragMove(); }
#endregion
    }
}