using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
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
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class ProgressBar : Window {
        public ProgressBar() {
            InitializeComponent();
            Start();
        }
        public void DoSomething(IProgress<int> progress)
        {
            for (int i = 1; i <= 100; i++)
            {
                Thread.Sleep(100);
                if (progress != null)
                    progress.Report(i);
            }
        }
        private async void Start() {
            pbStatus.Value = 0;
            var progress = new Progress<int>(percent =>
            {
                pbStatus.Value = percent;
 
            });
            await Task.Run(() => DoSomething(progress));
        }
    }
}
