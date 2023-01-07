using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Annotations;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChessGraphic2v
{
    public static class BoardHandler
    {
        public static Board boardhandle;
        public static Tools[,] ToolsPos;

        public static void ResetBoard()
        {
            boardhandle = null;
            ToolsPos = new Tools[8,8];

            // create null
            for (int i = 0; i < ToolsPos.GetLength(0); i++)
            {
                for (int j = 0; j < ToolsPos.GetLength(1); j++)
                {
                    ToolsPos[i, j] *= ToolsType.None;
                }
            }

            // create blacks

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

        public static void ChangeToolType(Pose current, ToolsType type)
        {

        }
    }
}
