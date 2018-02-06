using SoInc.ModdingTool.Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoInc.ModdingTool.Logic.DataManager
{
    /// <summary>
    /// represents the Manager for Scenarios
    /// </summary>
    public class ScenarioManager : BaseManger
    {
        /// <summary>
        /// internal field
        /// </summary>
        private Scenarios scenarios;

        /// <summary>
        /// gets or sets the list of the Scenarios
        /// </summary>
        public Scenarios Scenarios
        {
            get {
                if(scenarios == null)
                {
                    scenarios = new Scenarios();
                }
                return scenarios;
            }
            set { scenarios = value; }
        }

    }
}
