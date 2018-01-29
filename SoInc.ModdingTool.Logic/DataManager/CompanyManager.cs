using SoInc.ModdingTool.Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoInc.ModdingTool.Logic.DataManager
{
    /// <summary>
    /// represents the Manager for the Companies
    /// </summary>
    public class CompanyManager : BaseManger
    {
        /// <summary>
        /// Internal Field
        /// </summary>
        private Companies companies;

        /// <summary>
        /// Gets or Sets the CompanyList of the Mod
        /// </summary>
        public Companies Companies
        {
            get {
                if (companies == null)
                    companies = new Companies();
                return companies; }
            set { companies = value; }
        }
    }
}
