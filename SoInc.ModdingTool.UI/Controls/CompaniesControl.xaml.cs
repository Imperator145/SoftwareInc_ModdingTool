using SoInc.ModdingTool.Logic.Data;
using SoInc.ModdingTool.UI.Code;
using Syncfusion.UI.Xaml.Grid;
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
    /// Interaktionslogik für Companies.xaml
    /// </summary>
    public partial class CompaniesControl : BaseUserControl, IUpdateable
    {
        public CompaniesControl()
        {
            InitializeComponent();
            Main.CompanyManager.Companies.PropertyChanged += (sender, e) => this.Update();
        }

        /// <summary>
        /// Updates the UI with the Current company
        /// </summary>
        public void Update()
        {
            if (Main.CompanyManager.Companies.Current != null)
            {
                var c = Main.CompanyManager.Companies.Current;
                tbName.Text = c.Name;
                tbMoney.Value = c.Money;
                tbFans.Value = c.Fans;
                tbReputation.Value = c.BusinessReputation;
                cbFoundedMonth.SelectedIndex = c.FoundedMonth - 1;
                tbFoundedYear.Value = c.FoundedYear;
                
            }
            else
            {
                tbName.Text = "";
                tbMoney.Value = 0;
                tbFans.Value = 0;
                tbReputation.Value = 0;
                cbFoundedMonth.SelectedIndex = 0;
                tbFoundedYear.Value = 1960;
                dgCompanies.SelectedItem = null;
                dgCompanies.ItemsSource = null;
            }
            dgCompanies.ItemsSource = Main.CompanyManager.Companies;
            ucProducts.Update();
        }

        /// <summary>
        /// Updates the UI with the company
        /// </summary>
        /// <param name="company"></param>
        public void Update(Company company)
        {
            Main.CompanyManager.Companies.SetCurrent(company);
            this.Update();
        }

        /// <summary>
        /// Sets every UI-element to default value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Update(null);
        }

        /// <summary>
        /// Deletes the Current Selected Companie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgCompanies.SelectedItem != null && dgCompanies.SelectedItem is Company)
            {
                Main.CompanyManager.Companies.RemoveCurrent();
            }
        }

        /// <summary>
        /// Saves The Current Feature
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var c = Main.CompanyManager.Companies.GetCurrent();

            c.Name = tbName.Text;
            c.Money = Convert.ToInt32(tbMoney.Value);
            c.Fans = Convert.ToInt32(tbFans.Value);
            c.FoundedYear = Convert.ToInt32(tbFoundedYear.Value);
            c.FoundedMonth = cbFoundedMonth.SelectedIndex + 1;
            c.BusinessReputation = Convert.ToInt32(tbReputation.Value);
            Main.Update();

            this.Update();
        }

        /// <summary>
        /// sets the Current Company
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgCompanies_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grid.GridSelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is GridRowInfo)
            {
                this.Main.CompanyManager.Companies.SetCurrent((Company)((GridRowInfo)e.AddedItems[0]).RowData);
                this.Update();
            }
        }
    }
}
