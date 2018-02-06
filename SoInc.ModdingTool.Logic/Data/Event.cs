using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoInc.ModdingTool.Logic.Data
{
    /// <summary>
    /// represents a List of Events
    /// </summary>
    public class Events : AdvancedList<Event>
    {

    }

    /// <summary>
    /// represents a Event
    /// </summary>
    public class Event
    {
        public string Name { get; set; }

        [XmlArray("Companies"), XmlArrayItem("Company")]
        public List<string> Companies { get; set; } = new List<string>();
    }
}
