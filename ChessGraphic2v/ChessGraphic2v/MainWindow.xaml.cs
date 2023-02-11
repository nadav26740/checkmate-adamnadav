using ChessGraphic2v.UiEngine;
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
    public class ComputerInfo
    {
        public string Name { get; set; }
        public string Ip { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void AddChessMoveToHistory(UiEngine.ChessHistoryMove move)
        {
            HistoryList.Items.Add(move);
            HistoryList.SelectedIndex = HistoryList.Items.Count - 1;
            HistoryList.ScrollIntoView(HistoryList.SelectedItem);
        }

        public MainWindow()
        {
            InitializeComponent();
            Framer.Content = new Board();
            BoardHandler.LoaderScreen = new AdditionalScreens.LoadingScreen();
            Loaded += new RoutedEventHandler(showLoadingScreen);
            BoardHandler.MainWindowHandler = this;
            int x = 1;

        }

        private void showLoadingScreen(object sender, RoutedEventArgs e)
        {
            // showing the loading screen in the middle of the main window
            BoardHandler.LoaderScreen.Owner = this;
            BoardHandler.LoaderScreen.Show();

            // reseting the board
            BoardHandler.ResetBoard();
            BoardHandler.boardhandle = (Board)Framer.Content;
            BoardHandler.Elinker = new UiEngine.EngineLinker();

            Application.Current.MainWindow = this;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (BoardHandler.LoaderScreen != null)
                BoardHandler.LoaderScreen.Close();
            
            BoardHandler.Elinker.Dispose();
            App.Current.Shutdown();

        }

        // Showing Notification Image
        public void ChangeNotification(string newMessage, bool CriticalMessage = true)
        {
            NotificationMessage.Text = newMessage;
            
            // getting the image
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri((CriticalMessage ?
                @"pack://application:,,,/ResourcesPack/x.png" : @"pack://application:,,,/ResourcesPack/exclamation-mark-2.png"), UriKind.Absolute);
            image.EndInit();

            // rendering the notification image
            ImageBrush imgbrush = new ImageBrush();
            imgbrush.ImageSource = image;
            RenderOptions.SetBitmapScalingMode(imgbrush, BitmapScalingMode.Fant);
            
            NotificationIcon.Background = imgbrush;
            NotificationIcon.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(CriticalMessage ?
                "#E74C3C" : "#ECF0F1"));

            // change colors of the gardiant in the border
            ((GradientStop)FindName("GradientStop2")).Color = ((Color)FindResource
                (CriticalMessage ? "CriticalGradientStop2" : "NonCriticalGradientStop2"));
            ((GradientStop)FindName("GradientStop1")).Color = ((Color)FindResource
                (CriticalMessage ? "CriticalGradientStop1" : "NonCriticalGradientStop1"));

            // story board
            Storyboard sb = (Storyboard)FindResource("NotificationBorderAnimation");
            NotificationBorder.BeginStoryboard(sb);

            // starting animation
            sb = (Storyboard)FindResource("NotificationBorderStoryBoard");
            NotificationBorder.BeginStoryboard(sb);
            
        }

        private void GameStarted(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.IsEnabled)
            {
               
                Storyboard sb = (Storyboard)FindResource("TintBoarderDisapear");
                Tint.BeginStoryboard(sb);
            }
        }

        public void GameEnded()
        {
            this.IsEnabled = false;
            Storyboard sb = (Storyboard)FindResource("TintBoarderShowing");
            Tint.BeginStoryboard(sb);
        }

    }
}
