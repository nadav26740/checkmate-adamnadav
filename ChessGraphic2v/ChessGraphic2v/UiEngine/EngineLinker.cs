using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace ChessGraphic2v.UiEngine
{
    public class EngineLinker
    {
        public pipe pipes;

        public EngineLinker() 
        {
            pipes = new pipe();
            new Thread(this.WaitforConnection).Start();

            Console.WriteLine("Teestttt");
        }

        private void WaitforConnection()
        {

            if (pipes.connect())
            {

                BoardHandler.LoaderScreen.Dispatcher.Invoke(() =>
                {
                    BoardHandler.LoaderScreen.Hide();
                    BoardHandler.LoaderScreen = null;
                });

                BoardHandler.MainWindowHandler.Dispatcher.Invoke(() =>
                {
                    BoardHandler.MainWindowHandler.IsEnabled = true;
                });
                
                
                return;
            }

            BoardHandler.LoaderScreen.Dispatcher.Invoke(() =>
            {
                BoardHandler.LoaderScreen.MainText.Text = "Error: Failed To Connect the Engine";

                BoardHandler.LoaderScreen.MainText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(""));
            });
            Task.Delay(5000).Wait();
            App.Current.Shutdown();
        }

        
    }
}
