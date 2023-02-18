using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChessGraphic2v.UiEngine
{
    public class EngineProccess
    {
        private const string EnginePath = "C:\\Users\\משתמש\\source\\repos\\checkmate-adamnadav\\Chess AdamNadav\\x64\\Debug\\Chess AdamNadav.exe";
        private Process Engine_Process;

        public EngineProccess()
        {
            if (EnginePath == "")
                return;
            Engine_Process = new Process();
            Engine_Process.StartInfo.UseShellExecute = false;
            Engine_Process.StartInfo.CreateNoWindow = true;
            Engine_Process.StartInfo.RedirectStandardOutput = true;
            Engine_Process.StartInfo.FileName = EnginePath;

            Engine_Process.OutputDataReceived += new DataReceivedEventHandler(newDataRecived);
            Engine_Process.Start();
        }


        // new data has been recived from the process 
        void newDataRecived(object e, DataReceivedEventArgs args)
        {
            Debug.Write(args.Data);
        }
    }
}
