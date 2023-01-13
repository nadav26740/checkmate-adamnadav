using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChessGraphic2v
{
    public struct Pose
    {
        public int x, y;
        public Pose(int X, int Y)
        {
            this.x = X;
            this.y = Y;
        }

        public override string ToString()
        {
            return (char)(x + 'a' - 1) + y.ToString();
        }
    }

    // protocol:
    // 0 - valid move
    // 1 - valid move (you made chess)
    // 2 - invalid move (not your player)
    // 3 - invalid move (destination is not free)
    // 4 - invalid move (chess will occur)
    // 5 - invalid move (out of bounds)
    // 6 - invalid move (illegal movement with piece)
    // 7 - Invalid move - source and dest are equal
    // 8 - check mate
    public enum AnswersProtocol : byte
    {
        ValidMove,
        ValidMoveChess,
        InvalidMoveNotYourPlayer,
        InvalidMoveDestIsntFree,
        InvalidMoveChessOccur,
        InvalidMoveOutOfIndex,
        InvalidMoveIliegalMove,
        InvalidMoveDontmoving,
        EndGame
    }

    public class Tools
    {
        // public fields
        public ToolsType Type;
        public BitmapImage image;
        public bool isBlack;
        public ContentPresenter presenter;
        public Pose pos;

        public Tools() 
        {
            Type        = ToolsType.None; 
            image       = null;
            isBlack     = false;
            presenter   = null;
        }

        public static Tools operator *(Tools tType, ToolsType type)
        {
            tType.Type = type;
            return tType;
        }

        public static Tools operator !(Tools tTool)
        {
            tTool.isBlack = !tTool.isBlack;
            return tTool;
        }

        private static BitmapImage GetImage(ToolsType type)
        {
            switch (type)
            {
                    case ToolsType.None:    return null;
                    case ToolsType.Queen:   return null;
                    case ToolsType.Pawn:    return null;
                    case ToolsType.Rock:    return null;
                    case ToolsType.Knight:  return null;
                    case ToolsType.King:    return null;
                    case ToolsType.Bishop:  return null;
            }
            return null;
        }
    }

    public enum ToolsType : Byte
    { 
        None,
        Pawn,
        Rock,
        Knight,
        King,
        Queen,
        Bishop
    }
}
