using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace ChessGraphic2v.UiEngine
{
    public class EngineLinker
    {
        public pipe pipes;
        public Thread Thread_thread;
        public bool IsWhitePlaying { get; private set; }

        public EngineLinker() 
        {
            pipes = new pipe();
            Thread_thread = new Thread(this.WaitforConnection);
            Thread_thread.Start();

            new EngineProccess();

            Console.WriteLine("Teestttt");
        }

        ~EngineLinker() 
        {
            Thread_thread.Abort();
            if (pipes.isConnected())
            {
                pipes.sendEngineMove("quit");
            }
        }

        private void WaitforConnection()
        {

            if (pipes.connect() && pipes.isConnected())
            {

                BoardHandler.LoaderScreen.Dispatcher.Invoke(() =>
                {
                    BoardHandler.LoaderScreen.MainText.Text = "Engine Connected!!!";

                    BoardHandler.LoaderScreen.Window_FadeOut();
                    BoardHandler.LoaderScreen = null;
                });

                BoardHandler.MainWindowHandler.Dispatcher.Invoke(() =>
                {
                    BoardHandler.MainWindowHandler.IsEnabled = true;
                    BoardHandler.MainWindowHandler.ChangeNotification((IsWhitePlaying ? "White " : "Black ") +
                    "Starting!\nGood Luck", false);
                    BoardHandler.SetNewTableByString(pipes.getEngineMessage());
                });

                return;
            }
            BoardHandler.LoaderScreen.Dispatcher.Invoke(() =>
            {
                BoardHandler.LoaderScreen.MainText.Text = "Error: Failed To Connect the Engine";

                BoardHandler.LoaderScreen.MainText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFBD081C"));
            });
        }

        // for making sure the thread getting closed too!
        public void Dispose()
        {
            pipes.Dispose();
        }
        
        public AnswersProtocol SendMoveToEngine(Pose oldPose, Pose newPose)
        {
            string temp;
            AnswersProtocol tempByte;
            pipes.sendEngineMove(oldPose.ToString() + newPose.ToString());
            temp = pipes.getEngineMessage();

            // check that egine still connected
            if (!pipes.isConnected())
            {
                MessageBox.Show("Server has been disconnected!!");
                BoardHandler.MainWindowHandler.Close();
                return AnswersProtocol.ValidMove;
            }


            tempByte = (AnswersProtocol)(temp[0] - '0');

            // todo::::
            switch (tempByte)
            {
                case AnswersProtocol.ValidMove:
                    BoardHandler.MainWindowHandler.AddChessMoveToHistory( new UiEngine.ChessHistoryMove(oldPose, newPose, BoardHandler.GetToolimage(oldPose),
                        tempByte, IsWhitePlaying));
                    BoardHandler.ChangeToolPosition(oldPose, newPose);
                    IsWhitePlaying = !IsWhitePlaying;
                    break;

                case AnswersProtocol.ValidMoveChess:
                    BoardHandler.MainWindowHandler.AddChessMoveToHistory(new UiEngine.ChessHistoryMove(oldPose, newPose, BoardHandler.GetToolimage(oldPose)
                        , tempByte, IsWhitePlaying));

                    BoardHandler.ChangeToolPosition(oldPose, newPose);
                    BoardHandler.MainWindowHandler.ChangeNotification
                        ((IsWhitePlaying ? "White" : "Black") + " Has made a chess");
                    IsWhitePlaying = !IsWhitePlaying;
                    break;

                case AnswersProtocol.InvalidMoveNotYourPlayer:
                    BoardHandler.MainWindowHandler.ChangeNotification
                        ((IsWhitePlaying ? "White" : "Black") + " this isn't your tool");
                    break;

                case AnswersProtocol.InvalidMoveDestIsntFree:
                    BoardHandler.MainWindowHandler.ChangeNotification
                        ((IsWhitePlaying ? "White" : "Black") + " You trying to attack your own tool");
                    break;

                case AnswersProtocol.InvalidMoveChessOccur:
                    BoardHandler.MainWindowHandler.ChangeNotification
                        ((IsWhitePlaying ? "White" : "Black") + " Invalid Move Chess Occur");
                    break;

                case AnswersProtocol.InvalidMoveOutOfIndex:
                    BoardHandler.MainWindowHandler.ChangeNotification
                        ("Invalid Move Chess Occur");
                    break;

                case AnswersProtocol.InvalidMoveIliegalMove:
                    BoardHandler.MainWindowHandler.ChangeNotification
                        ("Iliegal tool move");
                    break;

                case AnswersProtocol.InvalidMoveDontmoving:
                    BoardHandler.MainWindowHandler.ChangeNotification
                        ((IsWhitePlaying ? "White" : "Black") + " You are not moving!");
                    break;

                case AnswersProtocol.EndGame:
                    BoardHandler.MainWindowHandler.AddChessMoveToHistory(new UiEngine.ChessHistoryMove(oldPose, newPose, BoardHandler.GetToolimage(oldPose),
                        tempByte, IsWhitePlaying, "#e3b505"));
                    BoardHandler.ChangeToolPosition(oldPose, newPose);

                    BoardHandler.LoaderScreen = new AdditionalScreens.LoadingScreen();
                    BoardHandler.LoaderScreen.MainText.Text = "GG, " + (IsWhitePlaying ? "White" : "Black") + " Won!";
                    BoardHandler.LoaderScreen.Owner = BoardHandler.MainWindowHandler;
                    BoardHandler.LoaderScreen.MainText.FontSize = 36;
                    BoardHandler.LoaderScreen.Topmost = false;
                    BoardHandler.LoaderScreen.Show();
                    BoardHandler.MainWindowHandler.GameEnded();
                    break;

                default:
                    BoardHandler.MainWindowHandler.ChangeNotification("Error: Unknown Message: " + temp);
                    break;
            }

            return tempByte;
        }
    }
}
