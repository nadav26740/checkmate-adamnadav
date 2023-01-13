using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessGraphic2v
{
    enum BoardStatus
    {
        None,
        FirstChoose
    }

    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : Page
    {
        private Pose FirstPose;
        private StackPanel FirstPanel;
        private Pose LastPose;
        private StackPanel LastPanel;

        private BoardStatus Status;

        public Grid getGridBoard() { return GridBoard; }

        public Board()
        {
            InitializeComponent();
            Loaded += LoadFunctionsIntoPanels;
            Status = BoardStatus.None;
            FirstPose = new Pose();
            LastPose = new Pose();
        }

        // loading the panels and the functions into the panels
        private void LoadFunctionsIntoPanels(object sender, RoutedEventArgs e)
        {
            StackPanel sp;

            // loading the white panels
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    if((i + j) % 2 == 1)
                    {
                        sp = new StackPanel();
                        sp.Background = (RadialGradientBrush)FindResource("WhiteSlotsColor");
                        Grid.SetColumn(sp, i);
                        Grid.SetRow(sp, j);

                        GridBoard.Children.Add(sp);
                    }
                }
                
            }

            // loading functions into the panels
            foreach (UIElement uI in GridBoard.Children)
            {
                if (Grid.GetRow(uI) > 0 && Grid.GetColumn(uI) > 0)
                {
                    sp = uI as StackPanel;
                    sp.MouseLeftButtonDown += StackPanel_MouseLeftButtonDown;
                }
            }
        }

        // The panels function
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            

            // Highlighting the choosen slots

            // to highlight the chossen Slots and Run the command if needed
            if(Status == BoardStatus.None)
            {
                FirstPose.x = Grid.GetColumn(panel);
                FirstPose.y = Grid.GetRow(panel);

                // removing old panels that has chossen highlights
                if (FirstPanel != null)
                {
                    if ((Grid.GetRow(FirstPanel) + Grid.GetColumn(FirstPanel)) % 2 == 1)
                        FirstPanel.Background = (RadialGradientBrush)FindResource("WhiteSlotsColor");
                    else
                        FirstPanel.Background = (RadialGradientBrush)FindResource("BlackSlotsColor");

                    if ((Grid.GetRow(LastPanel) + Grid.GetColumn(LastPanel)) % 2 == 1)
                        LastPanel.Background = (RadialGradientBrush)FindResource("WhiteSlotsColor");
                    else
                        LastPanel.Background = (RadialGradientBrush)FindResource("BlackSlotsColor");
                }


                // setting the first panel
                FirstPanel = panel; 
                Status = BoardStatus.FirstChoose;
            }
            else
            {
                LastPose.x = Grid.GetColumn(panel);
                LastPose.y = Grid.GetRow(panel);
                LastPanel = panel;
                // todo: sending The Command to the server
                BoardHandler.Elinker.SendMoveToEngine(FirstPose, LastPose);


                // changing the status back to normal
                Status = BoardStatus.None;
            }

            if ((Grid.GetRow(panel) + Grid.GetColumn(panel)) % 2 == 1)
                panel.Background = (RadialGradientBrush)FindResource("WhiteSlotChosed");
            else
                panel.Background = (RadialGradientBrush)FindResource("BlackSlotChosed");
        }
    }
}



//StackPanel sened = sender as StackPanel;
//int i = Grid.GetColumn(sened);
//int j = Grid.GetRow(sened);

//ContentPresenter cp = new ContentPresenter();
//Image cc = new Image();
//cp.IsHitTestVisible = false;
//cc.HorizontalAlignment = HorizontalAlignment.Center;
//cc.VerticalAlignment = VerticalAlignment.Center;
//BitmapImage image = new BitmapImage();
//image.BeginInit();
//image.UriSource = new Uri(@"pack://application:,,,/ResourcesPack/p_black.png", UriKind.Absolute);
//image.EndInit();
//cc.Margin = new Thickness(10);
//cc.Source = image;

//cp.Content = cc;
//Grid.SetColumn(cp, i);
//Grid.SetRow(cp, j);
//GridBoard.Children.Add(cp);
//BoardHandler.MainWindowHandler.ChangeNotification("New Tool Created Index: " +
//    GridBoard.Children.IndexOf(cp).ToString(), true);

//cpm = cp;