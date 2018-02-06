using SoInc.ModdingTool.Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoInc.ModdingTool.Logic.DataManager
{
    /// <summary>
    /// represents the Manager vor Events
    /// </summary>
    public class EventManager : BaseManger
    {
        private Events events;

        public Events Events
        {
            get {
                if (events == null) events = new Events();
                return events;
            }
            set { events = value; }
        }

    }
}
