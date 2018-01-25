using SoInc.ModdingTool.UI.Code;
using System.Windows.Controls;

namespace SoInc.ModdingTool.UI.Controls
{
    public class BaseUserControl : UserControl
    {
        public BaseUserControl()
        {
            Main.NeedUpdate += (sender, e) =>
            {
                if (this is IUpdateable)
                {
                    ((IUpdateable)this).Update();
                }
            };
        }

        /// <summary>
        /// Gets the Main Manager of the App
        /// </summary>
        public MainManager Main
        {
            get { return MainManager.Current; }
        }

        /// <summary>
        /// Gets or sets the Status of the Control
        /// </summary>
        protected bool Saved { get; set; }

    }
}
