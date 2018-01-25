using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoInc.ModdingTool.Logic.Data
{
    /// <summary>
    /// Represents a list of SoftwareTypes
    /// </summary>
    public class SoftwareTypes : AdvancedList<SoftwareType>
    {

    }


    /// <summary>
    /// Represents a SoftwareType
    /// </summary>
    public class SoftwareType
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [XmlIgnore()]
        private List<Category> categories;

        public List<Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = new List<Data.Category>();
                }
                return categories;
            }
            set { categories = value; }
        }


        public double Random { get; set; }

        public double Popularity { get; set; }

        [XmlIgnore()]
        public bool OSSpecific
        {
            get
            {
                return bool.TryParse(OSSpecificString, out bool output) ? output : false;
            }
            set
            {
                OSSpecificString = value.ToString().ToUpper();
            }
        }

        [XmlIgnore()]
        public bool OneClient
        {
            get
            {
                return bool.TryParse(OneClientString, out bool output) ? output : false;
            }
            set
            {
                OneClientString = value.ToString().ToUpper();
            }
        }

        [XmlIgnore()]
        public bool InHouse
        {
            get
            {
                return bool.TryParse(InHouseString, out bool output) ? output : false;
            }
            set
            {
                InHouseString = value.ToString().ToUpper();
            }
        }

        [XmlElement("OSSpecific")]
        public string OSSpecificString { get; set; }

        [XmlElement("OneClient")]
        public string OneClientString { get; set; }

        [XmlElement("InHouse")]
        public string InHouseString { get; set; }

        public string Category { get; set; }

        public string OSLimit { get; set; }

        public string NameGenerator { get; set; }

        public List<Feature> Features { get; set; } = new List<Feature>();
    }
}
