using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoInc.ModdingTool.Logic.Data
{
    /// <summary>
    /// Represents a list of all Companies
    /// </summary>
    public class Companies : AdvancedList<Company>
    {

    }
    
    /// <summary>
    /// Represents a Company
    /// </summary>
    public class Company
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Fans { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BusinessReputation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore()]
        public int FoundedMonth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore()]
        public int FoundedYear { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("Founded")]
        public string Founded
        {
            get
            {
                return FoundedMonth + "-" + FoundedYear;
            }
            set
            {
                var items = value.Split('_');
                if (items.Length >= 2)
                {
                    int output;
                    FoundedMonth = int.TryParse(items[0], out output) ? output : 0;
                    FoundedYear = int.TryParse(items[1], out output) ? output : 0;
                }
                else
                {
                    FoundedMonth = 0;
                    FoundedYear = 0;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Products Products { get; set; } = new Products();
    }
}
