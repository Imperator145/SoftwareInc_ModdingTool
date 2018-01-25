using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SoInc.ModdingTool.UI
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        [System.STAThread()]
        public static void Main()
        {
            var app = new App();

            var window = new Welcome();

            app.Run(window);
        }

    }
}
