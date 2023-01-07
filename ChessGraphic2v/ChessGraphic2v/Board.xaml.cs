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
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : Page
    {
        private ContentPresenter cpm;

        public Grid getGridBoard() { return GridBoard; }

        public Board()
        {
            InitializeComponent();
            
        }

        private void StackPanel_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel sened = sender as StackPanel;
            int i = Grid.GetColumn(sened);
            int j = Grid.GetRow(sened);

            ContentPresenter cp = new ContentPresenter();
            Image cc = new Image();
            cp.IsHitTestVisible = false;
            cc.HorizontalAlignment = HorizontalAlignment.Center;
            cc.VerticalAlignment = VerticalAlignment.Center;
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(@"pack://application:,,,/ResourcesPack/p_black.png", UriKind.Absolute);
            image.EndInit();
            cc.Margin = new Thickness(10);
            cc.Source = image;

            cp.Content = cc;
            Grid.SetColumn(cp, i);
            Grid.SetRow(cp, j);
            GridBoard.Children.Add(cp);
            MessageBox.Show(GridBoard.Children.IndexOf(cp).ToString());
            cpm = cp;
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel sened = sender as StackPanel;
            int i = Grid.GetColumn(sened);
            int j = Grid.GetRow(sened);
            GridBoard.Children.Remove(cpm);

            Grid.SetColumn(cpm, i);
            Grid.SetRow(cpm, j);
            GridBoard.Children.Add(cpm);
            
        }
    }
}
