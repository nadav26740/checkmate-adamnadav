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
            ContentControl cc = new ContentControl();
            cc.FontSize = 30;
            cp.IsHitTestVisible = false;
            cc.HorizontalAlignment = HorizontalAlignment.Center;
            cc.VerticalAlignment = VerticalAlignment.Center;
            cc.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDFD991"));

            cc.Content = "Testter";

            cp.Content = cc;
            Grid.SetColumn(cp, i);
            Grid.SetRow(cp, j);
            GridBoard.Children.Add(cp);
            MessageBox.Show(GridBoard.Children.IndexOf((UIElement)cp).ToString());
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
