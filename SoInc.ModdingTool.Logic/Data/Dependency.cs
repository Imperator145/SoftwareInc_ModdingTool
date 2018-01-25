using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoInc.ModdingTool.Logic.Data
{
    public class Dependency
    {
        [XmlAttribute]
        public string Software { get; set; }

        [XmlText()]
        public string Text { get; set; }
    }
}
