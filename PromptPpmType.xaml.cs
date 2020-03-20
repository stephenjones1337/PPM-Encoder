using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project_Encode {
    /// <summary>
    /// Interaction logic for PromptPpmType.xaml
    /// </summary>
    public partial class PromptPpmType : Window {
        public int ppm = 1;
        public PromptPpmType() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (togAscii.IsChecked == true) {
                ppm = 0;
                this.Close();
            }else if (togRaw.IsChecked == true) {
                ppm = 1;
                this.Close();
            }
        }

        private void TogAscii_Checked(object sender, RoutedEventArgs e) {
            btnSubmit.IsEnabled = true;
        }

        private void TogRaw_Checked(object sender, RoutedEventArgs e) {
            btnSubmit.IsEnabled = true;
        }
    }
}
