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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double originalWindowHeight;
        double OriginalWindowWidth;

        public struct objecOriginalSize
        {

            public FrameworkElement objref;
            public double Original_height;
            public double Original_width;
        }

        private List<objecOriginalSize> OriginalElements;

        public void AddChessMoveToHistory(UiEngine.ChessHistoryMove move)
        {
            HistoryList.Items.Add(move);
            HistoryList.SelectedIndex = HistoryList.Items.Count - 1;
            HistoryList.ScrollIntoView(HistoryList.SelectedItem);
        }

        public MainWindow()
        {
            InitializeComponent();
            originalWindowHeight = this.Height;
            OriginalWindowWidth = this.Width;

            Framer.Content = new Board();
            BoardHandler.LoaderScreen = new AdditionalScreens.LoadingScreen();
            Loaded += new RoutedEventHandler(showLoadingScreen);
            BoardHandler.MainWindowHandler = this;
            OriginalElements = new List<objecOriginalSize>();
            Loaded += new RoutedEventHandler(load_Originsl_Elements_to_Arrays);
        }


        // to keep all the original size of specific elements
        public void load_Originsl_Elements_to_Arrays(object sender, RoutedEventArgs e)
        {
            objecOriginalSize tempStruct;
            // obj list of the objects that will be effected from size changing
            FrameworkElement[] ElementsToLoad = { Framer };

            //getting orignal window height
            // getting original window width
            
            // here we right every element we wanna save
            // by the next thing 
            foreach (FrameworkElement element in ElementsToLoad)
            {
                tempStruct.objref = element;
                tempStruct.Original_width = element.Width;
                tempStruct.Original_height = element.Height;
                OriginalElements.Add(tempStruct);
            }

            // special cases
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

        private void Window_SizeChanged_Event(object sender, SizeChangedEventArgs e)
        {
            MainWindow_SizeChanged();
        }

        private void MainWindow_SizeChanged()
        {
            if (!IsLoaded)
                return;
            // to store only the smaller scale
            double temp;
            temp = Math.Min((this.ActualHeight / originalWindowHeight), (this.ActualWidth / OriginalWindowWidth));

            foreach (objecOriginalSize obj in OriginalElements)
            {
                obj.objref.Height = obj.Original_height * temp;
                obj.objref.Width = obj.Original_width * temp;
            }

            ElementsGrid.ColumnDefinitions[0].Width = new GridLength( Framer.Width);
        }
    }
}
