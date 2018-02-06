using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoInc.ModdingTool.Logic.Data
{
    /// <summary>
    /// represents a list of types
    /// </summary>
    public class Types : List<Type>
    {
        public Types()
        {

        }
    }

    /// <summary>
    /// Represents a Type for a CompanyType
    /// </summary>
    public class Type
    {
        public Type(string text) : base()
        {
            Text = text;
        }

        public Type() { }
        
        [XmlAttribute(AttributeName ="Software")]
        public string Text { get; set; }
        
        [XmlText()]
        public int INT { get; set; } = 1;
    }
}
