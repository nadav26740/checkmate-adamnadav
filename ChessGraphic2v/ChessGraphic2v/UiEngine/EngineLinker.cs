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
        public Thread Thread_thread;

        public EngineLinker() 
        {
            pipes = new pipe();
            Thread_thread = new Thread(this.WaitforConnection);
            Thread_thread.Start();

            Console.WriteLine("Teestttt");
        }

        ~EngineLinker() 
        {
            Thread_thread.Abort();  
        }

        private void WaitforConnection()
        {

            if (pipes.connect())
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
                });
                return;
            }

            BoardHandler.LoaderScreen.Dispatcher.Invoke(() =>
            {
                BoardHandler.LoaderScreen.MainText.Text = "Error: Failed To Connect the Engine";

                BoardHandler.LoaderScreen.MainText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFBD081C"));
            });
            Task.Delay(5000).Wait();
            App.Current.Shutdown();
        }

        // for making sure the thread getting closed too!
        public void Dispose()
        {
            pipes.Dispose();
        }
        
    }
}
