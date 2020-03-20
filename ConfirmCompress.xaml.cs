using System.Windows;

namespace Project_Encode {
    /// <summary>
    /// Interaction logic for ConfirmCompress.xaml
    /// </summary>
    public partial class ConfirmCompress : Window {
        public int chooseCompress = 0;
        public ConfirmCompress(int RLEcompressNum, int decompressNum, int LZWcompressNum) {
            InitializeComponent();
            btnOK.IsEnabled = false;

            txtRleCompress.Text = RLEcompressNum.ToString() + " bytes";
            txtLzwCompress.Text = LZWcompressNum.ToString() + " bytes";
            txtDecompress.Text  = decompressNum.ToString()  + " bytes";
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            chooseCompress = 0;
            this.Close();
        }

        private void TogRLE_Checked(object sender, RoutedEventArgs e) {
            btnOK.IsEnabled = true;
            chooseCompress  = 1;
        }

        private void TogLZW_Checked(object sender, RoutedEventArgs e) {
            btnOK.IsEnabled = true;
            chooseCompress  = 2;
        }

        private void TogNONE_Checked(object sender, RoutedEventArgs e) {
            btnOK.IsEnabled = true;
            chooseCompress  = 0;
        }
    }
}
