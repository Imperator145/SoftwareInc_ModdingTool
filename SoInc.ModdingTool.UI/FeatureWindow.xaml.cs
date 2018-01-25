using SoInc.ModdingTool.Logic.Data;
using System;
using System.Windows;

namespace SoInc.ModdingTool.UI
{
    /// <summary>
    /// Interaktionslogik für FeatureWindow.xaml
    /// </summary>
    public partial class FeatureWindow : BaseWindow
    {
        /// <summary>
        /// Gets the Current Feature 
        /// </summary>
        public Feature CurrentFeature { get; private set; }

        /// <summary>
        /// Initialize the Window
        /// </summary>
        /// <param name="feature">feature for the current window</param>
        public FeatureWindow(Feature feature = null)
        {
            InitializeComponent();

            this.ContentRendered += (sender, e) => Update();
            
            cbParent.ItemsSource = Main.SoftwareTypeManager.SoftwareTypes.Current.Features;

            if (feature != null)
            {
                CurrentFeature = feature;
            }
            else
            {
                CurrentFeature = new Feature();
            }
        }

        /// <summary>
        /// Updates the Window
        /// </summary>
        protected void Update()
        {
            tbName.Text = CurrentFeature.Name;
            tbDescription.Text = CurrentFeature.Description;
            tbDevTime.Value = CurrentFeature.DevTime;
            tbInnovation.Value = CurrentFeature.Innovation;
            tbStability.Value = CurrentFeature.Stability;
            tbUsability.Value = CurrentFeature.Usability;
            tbResearch.Text = CurrentFeature.Research;
            cbForces.IsChecked = CurrentFeature.Forced;
            cbVital.IsChecked = CurrentFeature.Vital;
            cbParent.SelectedValue = CurrentFeature.From;
            slCodeArt.Value = CurrentFeature.CodeArt * 100;
        }

        /// <summary>
        /// Saves the Changes of the Window in the Current Feature
        /// </summary>
        protected void Save()
        {
            CurrentFeature.Name = tbName.Text;
            CurrentFeature.Description = tbDescription.Text;
            CurrentFeature.DevTime = Convert.ToInt32(tbDevTime.Value);
            CurrentFeature.Innovation = Convert.ToInt32(tbInnovation.Value);
            CurrentFeature.Usability = Convert.ToInt32(tbUsability.Value);
            CurrentFeature.Stability = Convert.ToInt32(tbStability.Value);
            CurrentFeature.Vital = (bool)cbVital.IsChecked;
            CurrentFeature.Forced = (bool)cbForces.IsChecked;
            CurrentFeature.From = cbParent.SelectedItem != null ? ((Feature)cbParent.SelectedItem).Name.ToString() : "";
            CurrentFeature.CodeArt = slCodeArt.Value / 100;
            CurrentFeature.Research = tbResearch.Text;

            if (!Main.SoftwareTypeManager.SoftwareTypes.Current.Features.Contains(CurrentFeature))
                Main.SoftwareTypeManager.SoftwareTypes.Current.Features.Add(CurrentFeature);
        }

        /// <summary>
        /// Opens the Window
        /// </summary>
        public new bool Show()
        {
            base.ShowDialog();
            return CurrentFeature != null;
        }

        /// <summary>
        /// Sets the PercentText of the Slider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slCodeArt_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tblCodeArt1.Text = 100 - e.NewValue + "%";
            tblCodeArt2.Text = e.NewValue + "%";
        }

        /// <summary>
        /// Closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentFeature = null;
            this.Close();
        }

        /// <summary>
        /// Saves the Changes and Closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
            this.Close();
        }
    }
}
