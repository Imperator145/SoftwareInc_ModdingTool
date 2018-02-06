using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoInc.ModdingTool.Logic.Data
{
    /// <summary>
    /// represents a list of Relationships
    /// </summary>
    public class Relationships : List<Relation>
    {

    }

    /// <summary>
    /// represents a Relation
    /// </summary>
    public class Relation
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlText()]
        public double Value { get; set; }
    }
}
