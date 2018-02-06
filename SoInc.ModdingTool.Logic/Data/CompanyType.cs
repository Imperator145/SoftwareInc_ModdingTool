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
        private Types types;
        
        //TODO: XML Serialization
        public Types Types
        {
            get
            {
                if (types == null)
                {
                    types = new Types();
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
