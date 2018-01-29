using SoInc.ModdingTool.Logic.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaktionslogik für SoftwareTypes.xaml
    /// </summary>
    public partial class SoftwareTypes : BaseUserControl, INotifyPropertyChanged, Code.IUpdateable
    {
        #region Event
        /// <summary>
        /// raises when any proptery has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged([CallerMemberName]string name = "")
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion Event

        /// <summary>
        /// Internal Field
        /// </summary>
        private bool changeable = true;

        /// <summary>
        /// gets or sets the status, if the DetailBoxes can be changed
        /// </summary>
        public bool Changeable
        {
            get { return changeable; }
            set
            {
                changeable = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internal Field
        /// </summary>
        private bool updateProcess = true;

        /// <summary>
        /// Initialize the TypeControl
        /// </summary>
        public SoftwareTypes()
        {
            InitializeComponent();
            dgTypes.ItemsSource = Main.SoftwareTypeManager.SoftwareTypes;
        }

        /// <summary>
        /// Updates the Entry
        /// </summary>
        public void Update() => this.Update(Main.SoftwareTypeManager.SoftwareTypes.Current);
        
        /// <summary>
        /// Fills the DetailBoxes with the selectedSoftwaretype
        /// </summary>
        /// <param name="type"></param>
        public void Update(SoftwareType type)
        {
            updateProcess = true;
            if (type != null)
            {
                tbName.Text = type.Name;
                tbDescription.Text = type.Description;
                slRandom.Value = type.Random * 100;
                ratPopularity.Value = type.Popularity * 10;
                cbOSSpecific.IsChecked = type.OSSpecific;
                cbOneClient.IsChecked = type.OneClient;
                cbInHouse.IsChecked = type.InHouse;
                tbOSLimit.Text = type.OSLimit;
                tbCategory.Text = type.Category;
                tbOSLimit.Text = type.OSLimit;
                dgCategories.ItemsSource = null;
                dgCategories.ItemsSource = type.Categories;
                Main.SoftwareTypeManager.SoftwareTypes.SetCurrent(type);
            }
            else
            {
                Main.SoftwareTypeManager.SoftwareTypes.ClearCurrent();
                tbName.Text = "";
                tbDescription.Text = "";
                slRandom.Value = 5;
                ratPopularity.Value = 0;
                cbOSSpecific.IsChecked = false;
                cbOneClient.IsChecked = false;
                cbInHouse.IsChecked = false;
                tbOSLimit.Text = "";
                tbCategory.Text = "";
                tbOSLimit.Text = "";
                dgCategories.ItemsSource = null;
                dgTypes.SelectedItem = null;
            }
            dgTypes.ItemsSource = Main.SoftwareTypeManager.SoftwareTypes;
            ucFeatureManager.Update();
            Changeable = true;
            updateProcess = false;
        }

        /// <summary>
        /// saves the Changes
        /// </summary>
        public void SaveData()
        {
            var type = Main.SoftwareTypeManager.SoftwareTypes.GetCurrent();
            type.Name = tbName.Text;
            type.Description = tbDescription.Text;
            type.Random = slRandom.Value / 100;
            type.Popularity = ratPopularity.Value / 10;
            type.OSSpecific = (bool)cbOSSpecific.IsChecked;
            type.OneClient = (bool)cbOneClient.IsChecked;
            type.InHouse = (bool)cbInHouse.IsChecked;
            type.OSLimit = tbOSLimit.Text;
            type.Category = tbCategory.Text;
            Changeable = true;
            Update(null);
        }

        /// <summary>
        /// changes the DetailBoxes if possible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgTypes_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grid.GridSelectionChangedEventArgs e)
        {
            if (!Changeable)
            {
                var answer = MessageBox.Show(Properties.Resources.ChangeSoftwareTypeWarning, "!!!!", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes) Changeable = true;
            }

            if (Changeable && dgTypes.SelectedItem != null && dgTypes.SelectedItem is SoftwareType)
            {
                this.Update((SoftwareType)dgTypes.SelectedItem);
            }
        }

        /// <summary>
        /// Save The Current Changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
        }

        /// <summary>
        /// Deletes the Current Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show(Properties.Resources.SureQuestion + " " + Properties.Resources.NotRecoverable, "!!!!", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
                Main.SoftwareTypeManager.SoftwareTypes.RemoveCurrent();
        }

        /// <summary>
        /// clears the DetailBoxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (!Changeable)
            {
                var answer = MessageBox.Show(Properties.Resources.SureQuestion + " " + Properties.Resources.ChangesNotSave, "!!!!", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes) Changeable = true;
            }

            if (Changeable)
                Update(null);
        }

        /// <summary>
        /// Sets the TextBoxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slRandom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.Changeable = false;
            this.tbRandom.Text = e.NewValue.ToString() + "%";
        }

        /// <summary>
        /// Sets the Changeable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Changed(object sender, EventArgs e)
        {
            if(!updateProcess)
                this.Changeable = false;
        }

        /// <summary>
        /// Opens a Window to add a new Category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            if (Main.SoftwareTypeManager.SoftwareTypes.Current != null)
            {
                new CategoryWindow().ShowDialog();
                Update();
            }
            else
            {
                MessageBox.Show(Properties.Resources.SoftwareTypeSelectInfo);
            }
        }

        /// <summary>
        /// Opens the window to edit the current selected Category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditCategory_Click(object sender, RoutedEventArgs e)
        {
            if(dgCategories.SelectedItem != null)
            {
                new CategoryWindow(dgCategories.SelectedItem as Category).ShowDialog();
                Update();
            }
            else
            {
                MessageBox.Show(Properties.Resources.CategorySelectInfo);
            }
        }
        

        /// <summary>
        /// deletes the category from the softwareType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if(dgCategories.SelectedItem != null)
            {
                Main.SoftwareTypeManager.RemoveCategory(dgCategories.SelectedItem as Category);
            }
            Update();
        }
    }
}
