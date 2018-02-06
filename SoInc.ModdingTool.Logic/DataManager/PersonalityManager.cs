using SoInc.ModdingTool.Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoInc.ModdingTool.Logic.DataManager
{
    /// <summary>
    /// represents a Manager for the Personalities
    /// </summary>
    public class PersonalityManager : BaseManger
    {
        /// <summary>
        /// internal field 
        /// </summary>
        [XmlIgnore()]
        private PersonalityGraph list;

        /// <summary>
        /// Gets or sets the list of the PersonalityList
        /// workaround for the xml
        /// </summary>
        public PersonalityGraph List
        {
            get
            {
                if (list == null)
                {
                    list = new PersonalityGraph() { new Personalities()};
                }
                return list;
            }
            set { list = value; }
        }

        /// <summary>
        /// gets or sets the personalities
        /// </summary>
        [XmlIgnore()]
        public Personalities Personalities
        {
            get => List[0];
            set => List[0] = value;
        }
    }
}
