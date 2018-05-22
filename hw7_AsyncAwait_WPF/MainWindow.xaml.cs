using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hw7_AsyncAwait_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int min;
        private int sec;
        private int milliSec;

        private bool isActiv;

        public MainWindow()
        {
            InitializeComponent();

            isActiv = false;

            ResetTime();
        }

        private void ResetTime()
        {
            min = 0;
            sec = 0;
            milliSec = 0;

            DrawTime();
        }

        private void DrawTime()
        {
            lblMin.Content = String.Format("{0:00}", min);
            lblSec.Content = String.Format("{0:00}", sec);
            lblMilliSec.Content = String.Format("{0:00}", milliSec);
        }

        private void btnStartClick(object sender, RoutedEventArgs e)
        {
            isActiv = true;
            Start();

        }

        private async void Start()
        {
            while (true)
            {
                if (isActiv == false)
                    break;

                int result = await GetSome();

                ++milliSec;

                if (milliSec >= 100)
                {
                    sec++;
                    milliSec = 0;

                    if (sec >= 60)
                    {
                        min++;
                        sec = 0;
                    }
                }

                DrawTime();
            }
        }

        private Task<int> GetSome()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(10);
                return 1;
            });
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            isActiv = false;
        }

        private void btnResetClick(object sender, RoutedEventArgs e)
        {
            isActiv = false;
            ResetTime();
        }

        private void btnLapClick(object sender, RoutedEventArgs e)
        {
            listBox.Items.Add(String.Format("{0:00}:{1:00}:{2:00}", min, sec, milliSec));
        }
    }
}
