using ChessGraphic2v.UiEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace ChessGraphic2v
{
    public static class BoardHandler
    {
        public static Board boardhandle;
        public static Tools[,] ToolsPos;
        public static MainWindow MainWindowHandler;
        public static UiEngine.EngineLinker Elinker;
        public static AdditionalScreens.LoadingScreen LoaderScreen;


        // reseting the Tools Array
        public static void ResetBoard()
        {
            boardhandle = null;
            ToolsPos = new Tools[8,8];
            for (int i = 0; i < ToolsPos.GetLength(0); i++)
            {
                for (int j = 0; j < ToolsPos.GetLength(1); j++)
                {
                    ToolsPos[i, j] = null;
                }
            }
        }

        // setting the table by the string that has been sent by the engine1
        public static void SetNewTableByString(string str)
        {
            int temp1 = 0, temp2 = 0;
            for (int i = 0; i < str.Length - 1; i++)
            {
                temp2 = i % 8;
                temp1 = i / 8;
                CreateNewTool(str[i], new Pose(temp1 + 1, temp2 + 1));
            }

            SetToolsPresenterInBoard();
        }

        // public for debuging 
        // getting char and pose creating by that tool and putting
        // it in the array of the tools that defining the board
        public static void CreateNewTool(char c, Pose ps)
        {
            Tools tool = ConvertCharToTool(c);
            Debug.WriteLine(c + " => " + (ps.y - 1).ToString() + ", " + (ps.x - 1).ToString() + " => " +
                 ToolsPos.GetLength(0).ToString() + ", " + ToolsPos.GetLength(1).ToString());

            ToolsPos[ps.y - 1, ps.x - 1] = tool;
            if (tool == null)
                return;

            tool.pos = ps;
            PutsPresenter(tool);
        }

        // converting the char into the type of tool
        private static Tools ConvertCharToTool(char c)
        {
            Tools tool = new Tools();
            if (c >= 'a' && c <= 'z')
            {
                tool.isBlack = true;
            }
            else if (c == '#')
            {
                tool.Type = ToolsType.None;
                tool.presenter = null;
                return null;
            }
            
            // converting it into lower case to make better
            c = char.ToLower(c);

            // setting the type of the tool
            switch (c)
            {
                case 'r':
                    tool.Type = ToolsType.Rock;
                    break;
                case 'n':
                    tool.Type = ToolsType.Knight;
                    break;
                case 'b':
                    tool.Type = ToolsType.Bishop;
                    break;
                case 'k':
                    tool.Type = ToolsType.King;
                    break;
                case 'q':
                    tool.Type = ToolsType.Queen;
                    break;
                case 'p':
                    tool.Type = ToolsType.Pawn;
                    break;
            }

            return tool;
        }

        private static void PutsPresenter(Tools tool)
        {
            // checking if there is a tool there
            if (tool == null)
                return;

            char temp_c = 'x';
            string temp_str;

            // setting the presenter image
            tool.image = new BitmapImage();
            tool.image.BeginInit();
            switch (tool.Type)
            {
                case ToolsType.Rock:
                    temp_c = 'r';
                    break;

                case ToolsType.Knight:
                    temp_c = 'n';
                    break;

                case ToolsType.Bishop:
                    temp_c = 'b';
                    break;

                case ToolsType.King:
                    temp_c = 'k';
                    break;

                case ToolsType.Queen:
                    temp_c = 'q';
                    break;

                case ToolsType.Pawn:
                    temp_c = 'p';
                    break;
            }
            temp_str = @"pack://application:,,,/ResourcesPack/" + temp_c.ToString() + "_" + (tool.isBlack ? "black" : "white") + ".png";
            tool.image.UriSource = new Uri(temp_str, UriKind.RelativeOrAbsolute);
            tool.image.EndInit();

            // creating the presenter
            tool.presenter = new ContentPresenter();
            System.Windows.Controls.Image cc = new System.Windows.Controls.Image();
            tool.presenter.IsHitTestVisible = false;
            cc.HorizontalAlignment = HorizontalAlignment.Center;
            cc.VerticalAlignment = VerticalAlignment.Center;
            cc.Margin = new Thickness(12);
            cc.Source = tool.image;
            tool.presenter.Content = cc;

        }

        // public for debuging 
        // Setting the presenters on the UI board
        public static void SetToolsPresenterInBoard()
        {

            // running on all the tools
            for (int i = 0; i < ToolsPos.GetLength(0); i++)
            {
                for (int j = 0; j < ToolsPos.GetLength(1); j++)
                {
                    if (ToolsPos[i, j] != null)
                    {
                        // setting the tool presenter location in grid
                        Grid.SetColumn  (ToolsPos[i, j].presenter, ToolsPos[i, j].pos.y);
                        Grid.SetRow     (ToolsPos[i, j].presenter, ToolsPos[i, j].pos.x);

                        // setting the tool it self in the grid
                        boardhandle.GridBoard.Children.Add(ToolsPos[i, j].presenter);
                    }
                }
            }
        }   

        // new pose (new tool) is the tool that being eaten
        // old pose (old tool) is the tool that doing the move
        public static void ChangeToolPosition(Pose oldP, Pose NewP) 
        { 
            if (ToolsPos[NewP.x, NewP.y].presenter != null)
            {
                // removing the new tool presenter
                boardhandle.GridBoard.Children.Remove(ToolsPos[NewP.x, NewP.y].presenter);
            }

            if (ToolsPos[oldP.x, oldP.y].presenter != null)
            {
                // removing the old tool presenter
                boardhandle.GridBoard.Children.Remove(ToolsPos[oldP.x, oldP.y].presenter);

                // Setting new grid location
                Grid.SetColumn  (ToolsPos[oldP.x, oldP.y].presenter,  NewP.y);
                Grid.SetRow     (ToolsPos[oldP.x, oldP.y].presenter,  NewP.x);

                // removing the new tool
                ToolsPos[NewP.x, NewP.y] = ToolsPos[oldP.x, oldP.y];
                ToolsPos[NewP.x, NewP.y] *= ToolsType.None;

                // setting the presenter in the grid
                boardhandle.GridBoard.Children.Add(ToolsPos[NewP.x, NewP.y].presenter);
            }
        }

        private static void ChangeToolType(Pose current, ToolsType type)
        {

        }
    }
}
