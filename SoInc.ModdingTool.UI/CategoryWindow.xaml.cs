using SoInc.ModdingTool.Logic.Data;
using SoInc.ModdingTool.UI.Code;
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
    /// Interaktionslogik für CategoryWindow.xaml
    /// </summary>
    public partial class CategoryWindow : BaseWindow
    {
        /// <summary>
        /// initialize the WIndow
        /// </summary>
        public CategoryWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// initialize the window with the Category
        /// </summary>
        /// <param name="cat"></param>
        public CategoryWindow(Category cat) : this()
        {
            Update(cat);
        }

        /// <summary>
        /// Updates the Window
        /// </summary>
        /// <param name="cat"></param>
        public void Update(Category cat)
        {
            if (cat != null)
            {
                tbName.Text = cat.Name;
                tbDescription.Text = cat.Description;
                sfPopularity.Value = cat.Popularity * 10;
                slRetension.Value = cat.Retention * 100;
                slTimeScale.Value = cat.TimeScale * 100;
                tbIterative.Value = cat.Iterative;
            }
            else
            {
                tbName.Text = "";
                tbDescription.Text = "";
                sfPopularity.Value = 5;
                slRetension.Value = 0;
                slTimeScale.Value = 0;
                tbIterative.Value = 0;
            }
        }

        /// <summary>
        /// returns the current Category
        /// </summary>
        /// <returns></returns>
        public Category GetCategory()
        {
            var res = new Category();
            res.Name = tbName.Text;
            res.Description = tbDescription.Text;
            res.Popularity = sfPopularity.Value / 10;
            res.Retention = slRetension.Value / 100;
            res.TimeScale = slTimeScale.Value / 100;
            res.Iterative = Convert.ToInt32(tbIterative.Value);

            return res;
        }

        /// <summary>
        /// changes the Text for the sliders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbRetension.Text = slRetension.Value + "%";
            tbTimeScale.Text = slTimeScale.Value + "%";
        }

        /// <summary>
        /// saves the Category and add it to the currentSoftwareType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Main.SoftwareTypeManager.SaveCategory(GetCategory());
            this.Close();
        }

        /// <summary>
        /// Closes the Window without saving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
