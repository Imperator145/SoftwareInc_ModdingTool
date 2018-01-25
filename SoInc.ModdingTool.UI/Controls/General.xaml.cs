using SoInc.ModdingTool.UI.Code;
using System.Windows;
using System.Windows.Forms;

namespace SoInc.ModdingTool.UI.Controls
{
    /// <summary>
    /// Interaktionslogik für General.xaml
    /// </summary>
    public partial class General : BaseUserControl, IUpdateable
    {
        /// <summary>
        /// Gets or sets the status of the Saveability
        /// </summary>
        protected bool Saveable
        {
            get
            {
                return btnSave.IsEnabled;
            }
            set
            {
                btnSave.IsEnabled = value;
            }
        }

        /// <summary>
        /// Initialize the Control
        /// </summary>
        public General()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            tbCreator.Text = Main.Mod.Creator;
            tbModName.Text = Main.Mod.Name;
            tbOutputPath.Text = Main.Mod.Path;
            Saveable = !string.IsNullOrEmpty(Main.Mod.Path);
        }

        /// <summary>
        /// Opens a Modfolder and Clears the Current Mod without saving 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var fd = new FolderBrowserDialog();
            fd.ShowNewFolderButton = false;
            fd.ShowDialog();

            if (!string.IsNullOrEmpty(fd.SelectedPath))
            {
                tbOutputPath.Text = fd.SelectedPath;
                Main.Open(fd.SelectedPath);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetOutputPath_Click(object sender, RoutedEventArgs e)
        {
            var fd = new FolderBrowserDialog();
            fd.ShowNewFolderButton = true;
            fd.ShowDialog();

            if (!string.IsNullOrEmpty(fd.SelectedPath))
            {
                tbOutputPath.Text = fd.SelectedPath;
            }
        }

        /// <summary>
        /// clears the Whole Mod without saving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Main.Clear();
            Saveable = false;
        }

        /// <summary>
        /// Saves the Whole Mod in the Output Path folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Main.Mod.Path) && Saveable)
            {
                Main.Save();
                Saveable = false;
            }
        }

        #region changes
        /// <summary>
        /// Saves the Changes in the Property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbModName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Main.Mod.Name = tbModName.Text;
            Saveable = true;
        }

        /// <summary>
        /// Saves the Changes in the Property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbOutputPath_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Main.Mod.Path = tbOutputPath.Text;
            Saveable = !string.IsNullOrEmpty(Main.Mod.Path);
        }

        /// <summary>
        /// Saves the Changes in the Property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCreator_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Main.Mod.Creator = tbCreator.Text;
            Saveable = true;
        }
        #endregion changes
    }
}
