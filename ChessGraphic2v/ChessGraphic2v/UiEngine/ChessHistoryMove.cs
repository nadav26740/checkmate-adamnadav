using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ChessGraphic2v.UiEngine
{
    public class ChessHistoryMove
    {
        public BitmapImage ToolImage { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Description { get; set; }
        public string MoveType { get; set; }
        public string HexColor { get; set; }

        public ChessHistoryMove(Pose P_From, Pose P_To, BitmapImage T_image, AnswersProtocol answer,
            bool IsWhitePlaying = false, string Hex_Color = "") 
        { 
            To = P_To.ToString();
            From = P_From.ToString();
            ToolImage = T_image;
            SetMoveDescription(answer, IsWhitePlaying);
            this.HexColor = Hex_Color;
            
        }

        private void SetMoveDescription(AnswersProtocol answer, bool IsWhitePlaying)
        {
            switch (answer)
            {
                case AnswersProtocol.ValidMove:
                    Description = (IsWhitePlaying ? "White" : "Black") + " Moved his tool";
                    MoveType = "←";
                    break;
                case AnswersProtocol.ValidMoveChess:
                    Description = (IsWhitePlaying ? "White" : "Black") + " made a check";
                    MoveType = "♟";
                    break;
                case AnswersProtocol.EndGame:
                    Description = "Mate By " + (IsWhitePlaying ? "White" : "Black") + "! GG\n" + 
                        (IsWhitePlaying ? "White" : "Black") + " Won";
                    MoveType = "♚";
                    break;
            }
        }

    }
}
