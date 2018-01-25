using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoInc.ModdingTool.Logic.Data
{
    /// <summary>
    /// represents a Error-Entry
    /// </summary>
    public class Error
    {
        public Exception Exception { get; set; }

        public string Method { get; set; }
        
        public DateTime Time { get; set; }

        public Error(Exception ex, string method = "")
        {
            this.Exception = ex;
            this.Method = "";
            this.Time = DateTime.Now;
        }
    }
}
