using System.Windows;

namespace Project_Encode {
    /// <summary>
    /// Interaction logic for PopupInput.xaml
    /// </summary>
    public partial class PopupInput : Window {
        public string result;
        public PopupInput(int maxLength) {
            InitializeComponent();
            if (maxLength > 254) {
                txtInput.MaxLength = 254;
            } else {
                txtInput.MaxLength = maxLength;
            }
        }

        private void GetInput() {
            result =  txtInput.Text;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e) {
            GetInput();
            this.Close();
        }
    }
}
