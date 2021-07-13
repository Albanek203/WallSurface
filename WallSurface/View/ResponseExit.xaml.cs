using System.Windows;

namespace WallSurface.View {
    public partial class ResponseExit  {
        public ResponseExit() { InitializeComponent(); }
        private void BtnCancel_OnClick(object sender, RoutedEventArgs e) { DialogResult = false; }
        private void BtnExit_OnClick(object   sender, RoutedEventArgs e) { DialogResult = true; }
    }
}