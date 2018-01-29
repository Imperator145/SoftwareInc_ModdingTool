using SoInc.ModdingTool.Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoInc.ModdingTool.Logic.DataManager
{
    /// <summary>
    /// Represents the Manager for CompanyTypes
    /// </summary>
    public class CompanyTypeManager : BaseManger
    {
        /// <summary>
        /// Internal field
        /// </summary>
        private CompanyTypes companyTypes= new CompanyTypes();

        /// <summary>
        /// Gets or Sets the CompanyTypes
        /// </summary>
        public CompanyTypes CompanyTypes
        {
            get { return companyTypes; }
            set { companyTypes = value; }
        }
    }
}
