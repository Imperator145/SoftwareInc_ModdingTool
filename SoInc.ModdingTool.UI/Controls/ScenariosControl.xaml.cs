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
    /// Interaktionslogik für Scenarios.xaml
    /// </summary>
    public partial class ScenariosControl : BaseUserControl, IUpdateable
    {
        /// <summary>
        /// initialize the Control
        /// </summary>
        public ScenariosControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// updates the whole UI
        /// </summary>
        public void Update()
        {
            var s = Main.ScenarioManager.Scenarios.Current;
            if (s != null)
            {
                tbName.Text = s.Name;
                cbSimulation.IsChecked = s.Simulation;

                lbMoney.ItemsSource = null;
                lbMoney.ItemsSource = s.Money;

                lbGoals.ItemsSource = null;
                lbGoals.ItemsSource = s.Goals;

                lbYears.ItemsSource = null;
                lbYears.ItemsSource = s.Years;

                lbEvents.ItemsSource = null;
                lbEvents.ItemsSource = s.Events;
            }
            else
            {
                tbName.Text = "";
                cbSimulation.IsChecked = false;
                lbMoney.ItemsSource = null;
                lbGoals.ItemsSource = null;
                lbYears.ItemsSource = null;
                lbEvents.ItemsSource = null;
            }
            dgScenarios.ItemsSource = null;
            dgScenarios.ItemsSource = Main.ScenarioManager.Scenarios;
            acEvent.ItemsSource = null;
            acEvent.ItemsSource = Main.EventManager.Events;
        }

        /// <summary>
        /// Sets the Current Item and Updates the Ui
        /// </summary>
        /// <param name="sc"></param>
        public void Update(Scenario sc)
        {
            Main.ScenarioManager.Scenarios.SetCurrent(sc);
            Update();
        }

        /// <summary>
        /// Clears the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show(Properties.Resources.SureQuestion, "!!", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {
                Update(null);
            }
        }

        /// <summary>
        /// deletes the Current Selected Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show(Properties.Resources.SureQuestion, "!!", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {
                Main.ScenarioManager.Scenarios.RemoveCurrent();
                Update(null);
            }
        }

        /// <summary>
        /// saves the Scenario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var s = Main.ScenarioManager.Scenarios.GetCurrent();
            s.Name = tbName.Text;
            s.Simulation = (bool)cbSimulation.IsChecked;
            Update(null);
        }

        private void dgScenarios_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grid.GridSelectionChangedEventArgs e)
        {
            if (dgScenarios.SelectedItem != null && dgScenarios.SelectedItem is Scenario)
            {
                Main.ScenarioManager.Scenarios.SetCurrent(dgScenarios.SelectedItem as Scenario);
                Update();
            }
        }

        #region Money
        /// <summary>
        /// adds a new Item to the List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoneyAdd_Click(object sender, RoutedEventArgs e)
        {
            Main.ScenarioManager.Scenarios.Current.Money.Add(Convert.ToInt32(tbMoney.Value));
            Update();
        }

        /// <summary>
        /// removes the selected Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoneyDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbMoney.SelectedItem != null && lbMoney.SelectedItem is int)
            {
                Main.ScenarioManager.Scenarios.Current.Money.Remove((int)lbMoney.SelectedItem);
            }
        }
        #endregion Money
        #region Goals

        /// <summary>
        /// removes the selected Goal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGoalDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbGoals.SelectedItem != null && lbGoals.SelectedItem is Goal)
            {
                Main.ScenarioManager.Scenarios.GetCurrent().Goals.Remove(lbGoals.SelectedItem as Goal);
                Update();
            }
        }

        /// <summary>
        /// adds a new Goal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGoalAdd_Click(object sender, RoutedEventArgs e)
        {
            var g = new Goal();
            g.Money = Convert.ToInt32(tbGoalMoney.Value);
            g.Month = cbGoalMonth.SelectedIndex + 1;
            g.Year = Convert.ToInt32(tbGoalYear.Value);
            Main.ScenarioManager.Scenarios.GetCurrent().Goals.Add(g);
            Update();
        }

        #endregion Goals
        #region Years
        /// <summary>
        /// removes a Year
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYearDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbYears.SelectedItem != null && lbYears.SelectedItem is int)
            {
                Main.ScenarioManager.Scenarios.GetCurrent().Years.Remove((int)lbYears.SelectedItem);
                Update();
            }
        }

        /// <summary>
        /// Adds a new Year
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYearAdd_Click(object sender, RoutedEventArgs e)
        {
            Main.ScenarioManager.Scenarios.GetCurrent().Years.Add(Convert.ToInt32(tbYear.Value));
            Update();
        }

        #endregion Years
        #region Events
        /// <summary>
        /// removes the selected Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbEvents.SelectedItem != null)
            {
                Main.ScenarioManager.Scenarios.GetCurrent().Events.Remove(lbEvents.SelectedItem.ToString());

                Update();
            }
        }

        /// <summary>
        /// adds a new Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(acEvent.Text.ToString()))
            {
                Main.ScenarioManager.Scenarios.GetCurrent().Events.Add(acEvent.Text.ToString());

                Update();
            }
        }

        #endregion Events
    }
}
