using SoInc.ModdingTool.UI.Code;
using System.Windows;

namespace SoInc.ModdingTool.UI
{
    public class BaseWindow : Window
    {
        /// <summary>
        /// Initialize the Window
        /// </summary>
        public BaseWindow()
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
        /// Gets the Infrastructure Object
        /// </summary>
        protected MainManager Main => MainManager.Current;
    }
}
