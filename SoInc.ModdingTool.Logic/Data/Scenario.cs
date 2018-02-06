using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoInc.ModdingTool.Logic.Data
{
    /// <summary>
    /// represents a List of Scenarios
    /// </summary>
    public class Scenarios : AdvancedList<Scenario>
    {

    }

    /// <summary>
    /// represents a scenario 
    /// </summary>
    public class Scenario
    {
        public string  Name { get; set; }

        [XmlIgnore()]
        public bool Simulation { get; set; }

        [XmlElement("Simulation")]
        public string SimText
        {
            get
            {
                return Simulation.ToString().ToUpper();
            }
            set
            {
                var b = false;
                bool.TryParse(value, out b);
                Simulation = b;
            }
        }

        [XmlArray(ElementName = "Money"), XmlArrayItem(ElementName ="Amount")]
        public List<int> Money { get; set; } = new List<int>();

        [XmlArray(ElementName = "Goals"), XmlArrayItem(ElementName ="Goal")]
        public List<Goal> Goals { get; set; } = new List<Goal>();

        [XmlArray(ElementName = "Years"), XmlArrayItem(ElementName = "Year")]
        public List<int> Years { get; set; } = new List<int>();

        [XmlArray(ElementName ="Events"), XmlArrayItem(ElementName ="Event")]
        public List<string> Events { get; set; } = new List<string>();
    }
}
