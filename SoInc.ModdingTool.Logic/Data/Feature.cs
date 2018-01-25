using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoInc.ModdingTool.Logic.Data
{
    public class Feature : INotifyPropertyChanged
    {
        #region Events
        /// <summary>
        /// Fires when any Property is Changing
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fires the event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged([CallerMemberName]string name = "")
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion Events
        
        public string Name { get; set; }

        public string Description { get; set; }

        public int DevTime { get; set; }

        public int Innovation { get; set; }

        public int Usability { get; set; }

        public int Stability { get; set; }

        public double CodeArt { get; set; }

        [XmlElement("Dependency")]
        public List<Dependency> Dependencies { get; set; }

        [XmlElement("SoftwareCategory")]
        public string SoftwareCategory { get; set; }

        [XmlAttribute("Category")]
        public string SoftwareCategoryCat { get; set; }
        
        [XmlIgnore()]
        private string from;

        [XmlAttribute("From")]
        public string From
        {
            get { return from; }
            set { from = value;
                OnPropertyChanged();
            }
        }


        [XmlAttribute("Research")]
        public string Research { get; set; }

        [XmlIgnore()]
        public bool Vital { get; set; }

        [XmlAttribute("Vital")]
        public string VitalString
        {
            get
            {
                return Vital.ToString().ToUpper();
            }
            set
            {
                bool.TryParse(value, out bool output);
                Vital = output;
            }
        }

        [XmlIgnore()]
        public bool Forced { get; set; }

        [XmlAttribute("Forced")]
        public string ForcedString
        {
            get
            {
                return Forced.ToString().ToUpper();
            }
            set
            {
                bool.TryParse(value, out bool output);
                Forced = output;
            }
        }


    }
}
