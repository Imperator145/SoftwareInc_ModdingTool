using SoInc.ModdingTool.Logic.Data;
using SoInc.ModdingTool.UI.Code;
using System.Windows;

namespace SoInc.ModdingTool.UI.Controls
{
    /// <summary>
    /// Interaktionslogik für FeatureManager.xaml
    /// </summary>
    public partial class FeatureManager : BaseUserControl, IUpdateable
    {
        /// <summary>
        /// Gets or sets the Manager for the FeatureDiagram
        /// </summary>
        protected FeatureDiagramManager FeatureDiagramManager { get; set; }

        /// <summary>
        /// Initialize the Component
        /// </summary>
        public FeatureManager()
        {
            InitializeComponent();

            Main.SoftwareTypeManager.SoftwareTypes.CollectionChanged += (sender, e) => this.Update();
            Main.SoftwareTypeManager.SoftwareTypes.PropertyChanged += (sender, e) => this.Update();
            

            FeatureDiagramManager = new FeatureDiagramManager(dvFeatures, dmFeatures);
        }

        /// <summary>
        /// Updates the Diagram
        /// </summary>
        public void Update()
        {
            dgFeatures.ItemsSource = null;
            if (Main.SoftwareTypeManager.SoftwareTypes.Current != null)
            {
                btnAdd.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnEdit.IsEnabled = true;
                dgFeatures.ItemsSource = Main.SoftwareTypeManager.SoftwareTypes.Current.Features;
                FeatureDiagramManager.Features = Main.SoftwareTypeManager.SoftwareTypes.Current.Features;
                FeatureDiagramManager.UpdateDiagram(true);
            }
            else
            {
                btnAdd.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnEdit.IsEnabled = false;
                FeatureDiagramManager.Clear();
            }
        }

        /// <summary>
        /// Opens the Window for a new Feature
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Main.SoftwareTypeManager.SoftwareTypes.Current != null)
            {
                new FeatureWindow().Show();
                Update();
            }
            else MessageBox.Show(Properties.Resources.SoftwareTypeSelectInfo);
        }

        /// <summary>
        /// deletes the Element and every parent prop of the childs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgFeatures.SelectedItem != null && dgFeatures.SelectedItem is Feature)
            {
                var item = dgFeatures.SelectedItem as Feature;

                Main.SoftwareTypeManager.RemoveFeature(item);
                Update();
            }
            else
                MessageBox.Show(Properties.Resources.NoFeatureSelected);

        }

        /// <summary>
        /// opens the window to edit the feature
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(dgFeatures.SelectedItem != null && dgFeatures.SelectedItem is Feature)
            {
                var item = dgFeatures.SelectedItem as Feature;

                new FeatureWindow(item).Show();
                Update();
            }
            else
            {
                MessageBox.Show(Properties.Resources.NoFeatureSelected);
            }
        }
    }
}