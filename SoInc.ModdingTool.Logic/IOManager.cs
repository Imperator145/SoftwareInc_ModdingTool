using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoInc.ModdingTool.Logic
{
    public class IOManager
    {
        /// <summary>
        /// checks the Path and the directory if its exists and create it if its not
        /// </summary>
        /// <param name="path"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public bool CheckPath(string path, bool create = true)
        {
            if (Directory.Exists(Path.GetDirectoryName(path)))
            {
                return true;
            }
            else if (create)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                return true;
            }

            if (File.Exists(path))
            {
                return true;
            }
            else if (create)
            {
                File.Create(path);
                return true;
            }
            return false;
        }
    }
}
