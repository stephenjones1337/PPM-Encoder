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
    public partial class ProgressBar : Window, INotifyPropertyChanged {
        private BackgroundWorker _bgworker = new BackgroundWorker();
        private int _workerState;
        
        public int WorkerState {
            get{return _workerState;}
            set{
                _workerState = value;
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("WorkerState"));
                }
            }
        }

        public int Time {get;set;}
        public ProgressBar(int time) {
            InitializeComponent();
            Time = time;
            DataContext = this;
            _bgworker.DoWork += (s,e) => {
                for (int i = 0; i <= Time; i++) {
                    Thread.Sleep(100);
                    WorkerState = i;
                }
                this.Close();
            };

        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
