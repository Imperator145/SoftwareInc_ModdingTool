using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoInc.ModdingTool.Logic
{
    public class ErrorManager:IOManager
    {
        /// <summary>
        /// Gets or Sets the Path of the Log-File
        /// </summary>
        public string Path { get; set; } = System.IO.Path.Combine(Environment.CurrentDirectory, "Logs", "ErrorLog.txt");

        private static ErrorManager _Current;

        public static ErrorManager Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new ErrorManager();
                }
                return _Current;
            }
            set { _Current = value; }
        }

        public void WriteError(Exception ex, string method = "")
        {
            var error = new Data.Error(ex, method);
            CheckPath(Path, true);
            File.AppendAllText(Path, String.Format("\r\n- {0} - {1}", error.Time, error.Exception.Message));
        }

        public static void WriteError(Exception ex) => Current.WriteError(ex,"");

    }
}
