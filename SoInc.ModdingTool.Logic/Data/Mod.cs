using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoInc.ModdingTool.Logic.Data
{
    public class Mod
    {
        /// <summary>
        /// Gets or Sets the name of the Mod
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Get or Sets the Local Path of the Mod
        /// </summary>
        public string Path { get; set; } = "";

        /// <summary>
        /// Gets or Sets the Name of the Creator
        /// </summary>
        public string Creator { get; set; } = "";
    }
}
