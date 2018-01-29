using SoInc.ModdingTool.Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoInc.ModdingTool.Logic.DataManager
{
    /// <summary>
    /// Represents the Manager of the Software Types
    /// </summary>
    public class SoftwareTypeManager: BaseManger
    {
        /// <summary>
        /// Internal Field
        /// </summary>
        private SoftwareTypes _SoftwareTypes;

        /// <summary>
        /// Gets or Sets the list of the SoftwareTypes
        /// </summary>
        public SoftwareTypes SoftwareTypes
        {
            get
            {
                if (_SoftwareTypes == null)
                {
                    _SoftwareTypes = new Data.SoftwareTypes();
                }
                return _SoftwareTypes;
            }
            set { _SoftwareTypes = value; }
        }

        /// <summary>
        /// Removes the feature from the CurrentSoftwareType
        /// </summary>
        /// <param name="feature"></param>
        public void RemoveFeature(Feature feature) => this.RemoveFeature(feature, SoftwareTypes.Current);

        /// <summary>
        /// removes the feature from the softwaretype
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="swt"></param>
        public void RemoveFeature(Feature feature, SoftwareType swt)
        {
            swt.Features.Remove(feature);
            foreach (var f in swt.Features)
            {
                if (f.From == feature.Name)
                {
                    f.From = "";
                }
            }
        }

        /// <summary>
        /// Removes the Category from the Current SoftwareType
        /// </summary>
        /// <param name="cat"></param>
        public void RemoveCategory(Category cat) => RemoveCategory(cat, SoftwareTypes.Current);

        /// <summary>
        /// Removes the Category from the SOftwareType
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="swt"></param>
        public void RemoveCategory(Category cat, SoftwareType swt)
        {
            swt.Categories.Remove(cat);
        }

        /// <summary>
        /// saves the Category into the current softwaretype
        /// </summary>
        /// <param name="cat"></param>
        public void SaveCategory(Category cat) => SaveCategory(cat, SoftwareTypes.Current);

        /// <summary>
        /// saves the Category
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="swt"></param>
        public void SaveCategory(Category cat, SoftwareType swt)
        {
            if (!swt.Categories.Contains(cat))
            {
                swt.Categories.Add(cat);
            }
        }
    }
}