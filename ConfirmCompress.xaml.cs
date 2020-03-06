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
    /// Interaction logic for ConfirmCompress.xaml
    /// </summary>
    public partial class ConfirmCompress : Window {
        public bool chooseCompress;
        public ConfirmCompress(int compressNum, int decompressNum) {
            InitializeComponent();

            txtCompress.Text = compressNum.ToString() + " bytes";
            txtDecompress.Text = decompressNum.ToString() + " bytes";
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e) {
            chooseCompress = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            chooseCompress = false;
            this.Close();
        }
    }
}
