using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SoInc.ModdingTool.UI
{
    /// <summary>
    /// Interaktionslogik für Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button)
            {
                var btn = sender as Button;
                switch (btn.Name)
                {
                    case "ButtonGerman":
                        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
                        break;
                    case "ButtonEnglish":
                        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                        break;
                }
            }
            new MainWindow().Show();
            this.Close();
        }
    }
}
