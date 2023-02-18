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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChessGraphic2v.AdditionalScreens
{
    /// <summary>
    /// Interaction logic for LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen : Window
    {
        public LoadingScreen()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BoardHandler.MainWindowHandler.ChangeNotification("Test", true);
        }

        public void Window_FadeOut()
        {
            Storyboard sb = (Storyboard)FindResource("FadeOut");
            this.BeginStoryboard(sb);

            Task.Delay(TimeSpan.FromSeconds(0.75));
            this.Close();
        }

        public void ChangeDockingPos(double HostHeight, double HostWidth, double HostTop, double HostLeft, WindowState HostWindowState)
        {
            if (HostWindowState == WindowState.Maximized)
            {
                HostLeft = 0;
                HostTop = 0;
            }

            double x = 0, y = 0;
            HostHeight = Math.Max(HostHeight, this.Height);
            HostWidth = Math.Max(HostWidth, this.Width);

            x = HostWidth / 2 + HostLeft;
            y = HostHeight / 2 + HostTop;

            x -= Width / 2;
            y -= Height / 2;

            Top = y;
            Left = x;
        }
    }
}
