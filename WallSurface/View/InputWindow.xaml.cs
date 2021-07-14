using System.Windows;

namespace WallSurface.View {
    public partial class InputWindow {
        public static string TextToSearch;
        public InputWindow() { InitializeComponent(); }
        private void BtnSelect_OnClick(object sender, RoutedEventArgs e) {
            TextToSearch = SearchText.Text;
            DialogResult = true;
        }
        private void BtnCancel_OnClick(object sender, RoutedEventArgs e) { DialogResult = false; }
    }
}