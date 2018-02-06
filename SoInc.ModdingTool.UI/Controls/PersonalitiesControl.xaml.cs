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
    /// Interaktionslogik für Personalities.xaml
    /// </summary>
    public partial class PersonalitiesControl : BaseUserControl, IUpdateable
    {
        /// <summary>
        /// initialize the Control
        /// </summary>
        public PersonalitiesControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// updates the UI
        /// </summary>
        public void Update()
        {
            if(Main.PersonalityManager.Personalities.Current != null)
            {
                var p = Main.PersonalityManager.Personalities.GetCurrent();
                tbName.Text = p.Name;
                slAptitude.Value = p.Aptitude * 100;
                slLeadership.Value = p.Leadership * 100;
                slDiligence.Value = p.Diligence * 100;
                lbRelationships.ItemsSource = null;
                lbRelationships.ItemsSource = p.Relationsships;
            }
            else
            {
                tbName.Text = "";
                slAptitude.Value = 0;
                slLeadership.Value = 0;
                slDiligence.Value = 0;
                lbRelationships.ItemsSource = null;
                dgPersonalities.ItemsSource = null;
            }
            dgPersonalities.ItemsSource = Main.PersonalityManager.Personalities;
        }

        /// <summary>
        /// set the current personality and updates the ui
        /// </summary>
        /// <param name="p"></param>
        public void Update(Personality p)
        {
            Main.PersonalityManager.Personalities.SetCurrent(p);
            Update();
        }
        
        /// <summary>
        /// sets the current personality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgPersonalities_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grid.GridSelectionChangedEventArgs e)
        {
            if (dgPersonalities.SelectedItem is Personality)
            {
                Main.PersonalityManager.Personalities.SetCurrent(dgPersonalities.SelectedItem as Personality);
                Update();
            }
        }
        
        #region slider
        /// <summary>
        /// updates the Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slAptitude_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbAptitude.Text = slAptitude.Value + " %";
        }

        /// <summary>
        /// updates the Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slLeadership_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbLeadership.Text = slLeadership.Value + " %";
        }

        /// <summary>
        /// updates the Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slDiligence_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbDiligence.Text = slDiligence.Value + " %";
        }
        #endregion slider

        #region Relationships
        /// <summary>
        /// adds a new Relation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRelationAdd_Click(object sender, RoutedEventArgs e)
        {
            var res = RelationShipWindow.Show();
            if(res != null)
            {
                Main.PersonalityManager.Personalities.GetCurrent().Relationsships.Add(res);
                Update();
            }
        }

        /// <summary>
        /// deletes the selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRelationDelete_Click(object sender, RoutedEventArgs e)
        {
            if(lbRelationships.SelectedItem is Relation)
            {
                Main.PersonalityManager.Personalities.GetCurrent().Relationsships.Remove(lbRelationships.SelectedItem as Relation);
                Update();
            }
        }
        #endregion RelationShips

        #region Personality
        /// <summary>
        /// clears the UI without saving changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Update(null);
        }

        /// <summary>
        /// delete the current Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show(Properties.Resources.SureQuestion, "", MessageBoxButton.YesNo);
            if(answer == MessageBoxResult.Yes)
            {
                Main.PersonalityManager.Personalities.RemoveCurrent();
                Update();
            }
        }

        /// <summary>
        /// saves the changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var p = Main.PersonalityManager.Personalities.GetCurrent();
            p.Name = tbName.Text;
            p.Aptitude = slAptitude.Value / 100;
            p.Leadership = slLeadership.Value / 100;
            p.Diligence = slDiligence.Value / 100;
            Update(null);
        }
        #endregion Personality
    }
}
