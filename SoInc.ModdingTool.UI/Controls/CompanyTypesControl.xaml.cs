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
                lbTypes.ItemsSource = null;
                lbTypes.ItemsSource = ct.Types;
            }
            else
            {
                tbSpecialization.Text = "";
                slPerYear.Value = 0;
                tbMin.Value = 0;
                tbMax.Value = 0;
                lbTypes.ItemsSource = null;
                dgCompanyTypes.ItemsSource = null;
            }
            dgCompanyTypes.ItemsSource = Main.CompanyTypeManager.CompanyTypes;
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
            Update();
        }

        /// <summary>
        /// deletes the current selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(Main.CompanyTypeManager.CompanyTypes.Current != null)
            {
                Main.CompanyTypeManager.CompanyTypes.RemoveCurrent();
                this.Update();
            }            
        }

        /// <summary>
        /// clears the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Update(null);
        }

        /// <summary>
        /// aktualize the Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slPerYear_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbPerYear.Text = slPerYear.Value.ToString() + " %";
        }

        /// <summary>
        /// adds a new Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTypesAdd_Click(object sender, RoutedEventArgs e)
        {
            var entry = EntryBox.Show(Properties.Resources.EntryType);
            if (!string.IsNullOrEmpty(entry))
            {
                Main.CompanyTypeManager.CompanyTypes.GetCurrent().Types.Add(new Logic.Data.Type(entry));
                Update();
            }
        }

        /// <summary>
        /// deletes the selected Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTypesDelete_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show(Properties.Resources.SureQuestion, "!!!", MessageBoxButton.YesNo);
            if(answer == MessageBoxResult.Yes)
            {
                Main.CompanyTypeManager.CompanyTypes.RemoveCurrent();
                this.Update(null);
            }
        }

        /// <summary>
        /// set the UI to the Selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgCompanyTypes_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grid.GridSelectionChangedEventArgs e)
        {
            if(dgCompanyTypes.SelectedItem != null)
            {
                var item = dgCompanyTypes.SelectedItem as CompanyType;
                Main.CompanyTypeManager.CompanyTypes.SetCurrent(item);
                Update();
            }
        }
    }
}
