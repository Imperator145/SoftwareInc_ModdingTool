using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoInc.ModdingTool.Logic.Data
{
    /// <summary>
    /// Represents the Category
    /// </summary>
    public class Category
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public double Popularity { get; set; }

        public double Retention { get; set; }

        public double TimeScale { get; set; }

        public int Iterative{ get; set; }
    }
}
