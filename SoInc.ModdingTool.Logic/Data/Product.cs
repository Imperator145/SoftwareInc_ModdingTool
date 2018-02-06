using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoInc.ModdingTool.Logic.Data
{
    /// <summary>
    /// Represents a List of Products
    /// </summary>
    public class Products : AdvancedList<Product>
    {

    }

    /// <summary>
    /// Represents a Product
    /// </summary>
    public class Product
    {
        public string Name { get; set; }

        [XmlIgnore()]
        public int ReleaseMonth { get; set; }

        [XmlIgnore()]
        public int ReleaseYear { get; set; }

        [XmlElement("Release")]
        public string Release
        {
            get
            {
                return ReleaseMonth + "-" + ReleaseYear;
            }
            set
            {
                string[] items = value.Split('_');
                if (items.Length >= 2)
                {
                    ReleaseMonth = int.TryParse(items[0], out int output) ? output : 0;
                    ReleaseYear = int.TryParse(items[1], out output) ? output : 0;
                }
                else
                {
                    ReleaseMonth = 0;
                    ReleaseYear = 0;
                }
            }
        }

        public string Type { get; set; }

        public string Category { get; set; }

        public double Quality { get; set; }

        [XmlIgnore()]
        public bool OpenSource { get; set; }

        [XmlElement(ElementName = "OpenSource")]
        public string OpenSourceString
        {
            get
            {
                return OpenSource.ToString();
            }
            set
            {
                bool.TryParse(value, out bool output);
                OpenSource = output;
            }
        }

        [XmlIgnore()]
        public bool InHouse { get; set; }

        [XmlElement(ElementName = "InHouse")]
        public string InHouseString
        {
            get
            {
                return InHouse.ToString();
            }
            set
            {
                bool.TryParse(value, out bool output);
                InHouse = output;
            }
        }

        public int Reception { get; set; }

        public double Popularity { get; set; }

        public string SequelTo { get; set; }

        [XmlArray(ElementName = "OS")
            , XmlArrayItem(ElementName = "Name")]
        public List<string> OS { get; set; } = new List<string>();

        public string Needs { get; set; } = "";

        [XmlArray(ElementName = "Features"),
            XmlArrayItem(ElementName = "Feature")]
        public List<string> Features { get; set; } = new List<string>();
    }
}
