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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessGraphic2v
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AdditionalScreens.LoadingScreen LoaderScreen;

        public MainWindow()
        {
            InitializeComponent();
            Framer.Content = new Board();
            LoaderScreen = new AdditionalScreens.LoadingScreen();
            Loaded += new RoutedEventHandler(showLoadingScreen);
            BoardHandler.MainWindowHandler = this;
        }

        private void showLoadingScreen(object sender, RoutedEventArgs e)
        {
            LoaderScreen.Owner = this;
            LoaderScreen.Show();
            BoardHandler.ResetBoard();
            BoardHandler.boardhandle = (Board)Framer.Content;
            BoardHandler.SetNewTableByString("pppppppp################PPPPPpPPPP");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }

        // Showing Notification Image
        public void ChangeNotification(string newMessage, bool CriticalMessage)
        {
            NotificationMessage.Text = newMessage;
            
            // getting the image
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri((CriticalMessage ?
                @"pack://application:,,,/ResourcesPack/x.png" : @"pack://application:,,,/ResourcesPack/Exclamation mark.png"), UriKind.Absolute);
            image.EndInit();

            // rendering the notification image
            ImageBrush imgbrush = new ImageBrush();
            imgbrush.ImageSource = image;
            RenderOptions.SetBitmapScalingMode(imgbrush, BitmapScalingMode.Fant);
            
            NotificationIcon.Background = imgbrush;
            NotificationIcon.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(CriticalMessage ?
                "#E74C3C" : "#ECF0F1"));


            // starting animation
            Storyboard sb = (Storyboard)FindResource("NotificationBorderStoryBoard");
            NotificationBorder.BeginStoryboard(sb);
        }
    }
}
