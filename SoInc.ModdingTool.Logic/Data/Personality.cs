using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoInc.ModdingTool.Logic.Data
{
    /// <summary>
    /// represents the PErsonality Graph -> Workaround
    /// </summary>
    [XmlRoot(ElementName = "PersonalityGraph")]
    public class PersonalityGraph : List<Personalities>
    {
        //TODO: Auslesen geht nicht
    }

    /// <summary>
    /// represents a list of Personalities
    /// </summary>
    [XmlType(TypeName ="Personalities")]
    public class Personalities : AdvancedList<Personality>
    {

    }
    
    /// <summary>
    /// represents a Personality
    /// </summary>
    public class Personality
    {
        public string Name { get; set; }

        public double Aptitude { get; set; }

        public double Leadership { get; set; }

        public double Diligence { get; set; }

        [XmlArray(ElementName ="Relationships"), XmlArrayItem(ElementName ="Relation")]
        public Relationships Relationsships { get; set; } = new Relationships();
    }
}
