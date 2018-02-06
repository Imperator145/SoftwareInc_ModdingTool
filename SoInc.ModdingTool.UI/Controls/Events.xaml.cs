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
    /// Interaktionslogik für Events.xaml
    /// </summary>
    public partial class Events : BaseUserControl, IUpdateable
    {
        /// <summary>
        /// initializes the UI
        /// </summary>
        public Events()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// updates the UI
        /// </summary>
        public void Update()
        {
            var e = Main.EventManager.Events.Current;
            if(e != null)
            {
                tbName.Text = e.Name;
                lbCompanies.ItemsSource = null;
                lbCompanies.ItemsSource = e.Companies;
            }
            else
            {
                tbName.Text = "";
                lbCompanies.ItemsSource = null;
                dgEvents.ItemsSource = null;
            }
            
            dgEvents.ItemsSource = Main.EventManager.Events;
            var items = Main.CompanyManager.Companies.GetList();
            acCompanyAdd.CustomSource = null;
            acCompanyAdd.CustomSource = items;
        }

        /// <summary>
        /// Updates the UI with the event
        /// </summary>
        /// <param name="ev"></param>
        public void Update(Event ev)
        {
            Main.EventManager.Events.SetCurrent(ev);
            Update();
        }

        /// <summary>
        /// shows the selcted item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgEvents_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grid.GridSelectionChangedEventArgs e)
        {
            if (dgEvents.SelectedItem != null)
            {
                Main.EventManager.Events.SetCurrent(dgEvents.SelectedItem as Event);
                Update();
            }
        }

        /// <summary>
        /// Saves the Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var c = Main.EventManager.Events.GetCurrent();
            c.Name = tbName.Text;
            Update();
            Main.Update();
        }

        /// <summary>
        /// Deletes the Current Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show(Properties.Resources.SureQuestion, "!!", MessageBoxButton.YesNo);
            if(answer == MessageBoxResult.Yes)
            {
                Main.EventManager.Events.RemoveCurrent();
                Update(null);
            }

        }

        /// <summary>
        /// clears the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.Update(null);
        }

        /// <summary>
        /// adds the New Company
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCompaniesAdd_Click(object sender, RoutedEventArgs e)
        {
            var entry = acCompanyAdd.Text;
            if (!string.IsNullOrEmpty(entry))
            {
                Main.EventManager.Events.GetCurrent().Companies.Add(entry);
                Update();
            }
        }

        /// <summary>
        /// deletes the current Company
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCompaniesDelete_Click(object sender, RoutedEventArgs e)
        {
            if(lbCompanies.SelectedItem != null)
            {
                Main.EventManager.Events.Current.Companies.Remove(lbCompanies.SelectedItem.ToString());
                this.Update();
            }
        }
    }
}
