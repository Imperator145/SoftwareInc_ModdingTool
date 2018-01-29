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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoInc.ModdingTool.UI.Controls
{
    /// <summary>
    /// Interaktionslogik für CompanyTypes.xaml
    /// </summary>
    public partial class CompanyTypesControl : BaseUserControl, IUpdateable
    {
        public CompanyTypesControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// updates the UI
        /// </summary>
        public void Update()
        {
            var ct = Main.CompanyTypeManager.CompanyTypes.Current;
            if (ct != null)
            {
                tbSpecialization.Text = ct.Specialization;
                slPerYear.Value = ct.PerYear * 100;
                tbMin.Value = ct.Min;
                tbMax.Value = ct.Max;
                lbTypes.ItemsSource = ct.Types;
            }
            else
            {
                tbSpecialization.Text = "";
                slPerYear.Value = 0;
                tbMin.Value = 0;
                tbMax.Value = 0;
                lbTypes.ItemsSource = null;
            }
        }

        /// <summary>
        /// Sets the Current CompanyType and updates the UI
        /// </summary>
        /// <param name="ct"></param>
        public void Update(CompanyType ct)
        {
            if (ct == null)
                Main.CompanyTypeManager.CompanyTypes.ClearCurrent();
            else Main.CompanyTypeManager.CompanyTypes.SetCurrent(ct);
            this.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var ct = Main.CompanyTypeManager.CompanyTypes.GetCurrent();

            ct.Specialization = tbSpecialization.Text;
            ct.PerYear = slPerYear.Value / 100;
            ct.Min = Convert.ToInt32(tbMin.Value);
            ct.Max = Convert.ToInt32(tbMax.Value);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// aktualize the Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slPerYear_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbPerYear.Text = slPerYear.Value.ToString();
        }

        private void btnTypesAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTypesDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTypesAdd_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
