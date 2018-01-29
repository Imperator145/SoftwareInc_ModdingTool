using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoInc.ModdingTool.Logic.Data
{
    /// <summary>
    /// Represents a List for CompanyTypes
    /// </summary>
    public class CompanyTypes : AdvancedList<CompanyType>
    {

    }


    /// <summary>
    /// represents a Company Type
    /// </summary>
    public class CompanyType
    {
        public string Specialization { get; set; }

        public double PerYear { get; set; }

        public int Min { get; set; }

        public int Max { get; set; }


        [XmlIgnore()]
        private List<string> types;

        [XmlArray(ElementName = "Types")
            ,XmlArrayItem(ElementName = "Type")
            ,XmlAttribute(AttributeName = "Software")] //TODO: Prüfen ob das funktioniert
        public List<string> Types
        {
            get
            {
                if (types == null)
                {
                    types = new List<string>();
                }
                return types;
            }
            set
            {
                types = value;
            }
        }


    }
}
