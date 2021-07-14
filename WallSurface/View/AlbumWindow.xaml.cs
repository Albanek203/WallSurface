using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WallSurface.View {
    public partial class AlbumWindow {
        private readonly List<string> _listPathFiles = new();
        private readonly List<Image>  _listImages    = new();
        private readonly int          _totalPages;
        public AlbumWindow() { InitializeComponent(); }
        public AlbumWindow(List<string> pathFolder) {
            InitializeComponent();
            _listPathFiles = pathFolder;
            for (var i = 0; i < 16; i++) {
                var img = new Image {Style = (Style) Resources["ImageStyle"]};
                img.MouseUp += Image_OnMouseUp;
                img.Tag     =  i + 1;
                PicturesGrid.Children.Add(img);
                _listImages.Add(img);
            }
            var count = _listPathFiles.Count > 16 ? 16 : _listPathFiles.Count;
            for (var i = 0; i < count; i++) {
                var bmpImg = new BitmapImage();
                bmpImg.BeginInit();
                bmpImg.CacheOption      = BitmapCacheOption.OnDemand;
                bmpImg.CreateOptions    = BitmapCreateOptions.DelayCreation;
                bmpImg.UriSource        = new Uri(_listPathFiles[i]);
                bmpImg.DecodePixelWidth = 250;
                bmpImg.EndInit();
                _listImages[i].Source = bmpImg;
            }
            _totalPages          = _listPathFiles.Count / 16 + 1;
            LbNameFolder.Content = "Album";
            LbCountPage.Tag      = 1;
            LbCountPage.Content  = $@"Page # 1 / {_totalPages}";
        }
        private void Image_OnMouseUp(object sender, MouseButtonEventArgs e) {
            int.TryParse((sender as Image)?.Tag?.ToString(), out var index);
            Hide();
            if (new ViewWindow(_listImages[index - 1].Source.ToString().Remove(0, 8)).ShowDialog() == true)
                ShowDialog();
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

#region Navigate buttons
        private void ResetImageList() {
            foreach (var image in _listImages) image.Source = null;
        }
        private void LeftButtonView_OnClick(object sender, RoutedEventArgs e) {
            int.TryParse(LbCountPage.Tag.ToString(), out var countPage);
            if (countPage == 1) return;
            var rest        = _listPathFiles.Count - (--countPage * 16);
            var startNumber = _listPathFiles.Count - rest - 16;
            LbCountPage.Tag     = countPage;
            LbCountPage.Content = $@"Page # {countPage} / {_totalPages}";
            ResetImageList();
            for (int i = startNumber, j = 0; i < startNumber + 16; i++, j++) {
                var bmpImg = new BitmapImage();
                bmpImg.BeginInit();
                bmpImg.CacheOption      = BitmapCacheOption.OnDemand;
                bmpImg.CreateOptions    = BitmapCreateOptions.DelayCreation;
                bmpImg.UriSource        = new Uri(_listPathFiles[i]);
                bmpImg.DecodePixelWidth = 250;
                bmpImg.EndInit();
                _listImages[j].Source = bmpImg;
            }
        }
        private void RightButtonView_OnClick(object sender, RoutedEventArgs e) {
            if (_listPathFiles.Count < 16) return;
            int.TryParse(LbCountPage.Tag.ToString(), out var countPage);
            var rest = _listPathFiles.Count - countPage * 16;
            if (rest <= 0) return;
            var startNumber     = _listPathFiles.Count - rest;
            if (rest > 16) rest = 16;
            LbCountPage.Tag     = ++countPage;
            LbCountPage.Content = $@"Page # {countPage} / {_totalPages}";
            ResetImageList();
            for (int i = startNumber, j = 0; i < startNumber + rest; i++, j++) {
                var bmpImg = new BitmapImage();
                bmpImg.BeginInit();
                bmpImg.CacheOption      = BitmapCacheOption.OnDemand;
                bmpImg.CreateOptions    = BitmapCreateOptions.DelayCreation;
                bmpImg.UriSource        = new Uri(_listPathFiles[i]);
                bmpImg.DecodePixelWidth = 250;
                bmpImg.EndInit();
                _listImages[j].Source = bmpImg;
            }
        }
#endregion
    }
}