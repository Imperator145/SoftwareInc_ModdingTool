using SoInc.ModdingTool.Logic.Data;
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
    /// Interaktionslogik für RelationShipWindow.xaml
    /// </summary>
    public partial class RelationShipWindow : Window
    {
        /// <summary>
        /// Gets or Sets the Result of the Window
        /// </summary>
        public Relation Result{ get; private set; }

        /// <summary>
        /// Initialize a new Window
        /// </summary>
        public RelationShipWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens the Windows and returns the Value after closing
        /// </summary>
        /// <returns></returns>
        public static new Relation Show()
        {
            var window = new RelationShipWindow();
            window.ShowDialog();
            return window.Result;
        }

        /// <summary>
        /// sets the SliderText
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbValue.Text = slValue.Value + " %";
        }

        /// <summary>
        /// closes the Window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = null;
            this.Close();
        }

        /// <summary>
        /// sets the Result and returns it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Result = new Relation();
            Result.Name = tbName.Text;
            Result.Value = slValue.Value / 100;
            this.Close();
        }
    }
}
