using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SoInc.ModdingTool.UI.Code
{
    /// <summary>
    /// expands the Main Manager with UI Functions
    /// </summary>
    public class MainManager : Logic.MainManager
    {
        /// <summary>
        /// Internal Field
        /// </summary>
        private static MainManager current;

        /// <summary>
        /// Gets the Current MainManager
        /// </summary>
        public new static MainManager Current
        {
            get
            {
                if (current == null)
                {
                    current = new MainManager();
                }
                return current;
            }
        }

        /// <summary>
        /// creates and sets a new MainManager
        /// </summary>
        /// <param name="mainWindow"></param>
        public static void CreateManager(BaseWindow mainWindow) => current = new MainManager(mainWindow);

        /// <summary>
        /// gets or sets the MainWindow of the Application
        /// </summary>
        public BaseWindow MainWindow { get; private set; }
     
        /// <summary>
        /// Initialize the MainManager
        /// </summary>
        public MainManager()
        {

        }

        /// <summary>
        /// Initialize a new MainManager
        /// </summary>
        /// <param name="mainWindow"></param>
        public MainManager(BaseWindow mainWindow) : this()
        {
            this.MainWindow = mainWindow;
        }
    }
}
