using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SoInc.ModdingTool.Logic.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class Goal
    {
        [XmlIgnore()]
        public int Money { get; set; }

        [XmlIgnore()]
        public int Month { get; set; }

        [XmlIgnore()]
        public int Year { get; set; }

        [XmlIgnore()]
        public string Date
        {
            get
            {
                return Month + "-" + Year;
            }
            set
            {
                var s = value.Split('-');
                if (s.Length == 2)
                {
                    Month = Convert.ToInt32(s[0]);
                    Year = Convert.ToInt32(s[1]);
                }
            }
        }
        
        [XmlText()]
        public string Output
        {
            get
            {
                return "Money " + Money + ",Date " + Date;
            }
            set
            {
                Set(value);
            }
        }

        /// <summary>
        /// reads the input string and fill it in the Properties
        /// </summary>
        /// <param name="input"></param>
        protected void Set(string input)
        {
            var s = input.Split(',');
            if (s.Length == 2)
            {
                int output = 0;
                int.TryParse(s[0].Replace("Money", "").Trim(), out output);
                Money = output;

                
                var d = s[0].Replace("Date", "").Trim().Split('-');
                if(d.Length == 2)
                {
                    output = 0;
                    int.TryParse(d[0], out output);
                    Month = output;


                    output = 0;
                    int.TryParse(d[1], out output);
                    Year = output;
                }
            }
        }
    }
}
