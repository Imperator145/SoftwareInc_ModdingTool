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
    /// 
    /// </summary>
    public partial class ProductsControl : BaseUserControl, IUpdateable
    {
        /// <summary>
        /// initializes the UC
        /// </summary>
        public ProductsControl()
        {
            InitializeComponent();
            Main.NeedUpdate += (sender, e) => this.Update();
            Main.CompanyManager.Companies.PropertyChanged += (sender, e) => Update();
        }

        /// <summary>
        /// updates the UI
        /// </summary>
        public void Update()
        {
            if (Main.CompanyManager.Companies.Current == null)
                IsEnabled = false;
            else
            {
                IsEnabled = true;
                if (Main.CompanyManager.Companies.Current.Products.Current != null)
                {
                    var p = Main.CompanyManager.Companies.Current.Products.Current;
                    tbProductName.Text = p.Name;
                    tbReleaseYear.Value = p.ReleaseYear;
                    cbReleaseMonth.SelectedIndex = p.ReleaseMonth - 1;
                    tbType.Text = p.Type;
                    tbCategory.Text = p.Category;
                    slQuality.Value = p.Quality * 100;
                    cbOpenSource.IsChecked = p.OpenSource;
                    cbInHouse.IsChecked = p.InHouse;
                    tbReception.Value = p.Reception;
                    ratPopularity.Value = p.Popularity;
                    tbSequelTo.Text = p.SequelTo;
                    lbFeatures.ItemsSource = null;
                    lbFeatures.ItemsSource = p.Features;
                    lbOS.ItemsSource = null;
                    lbOS.ItemsSource = p.OS;
                }
                else
                {
                    tbProductName.Text = "";
                    tbReleaseYear.Value = 1960;
                    cbReleaseMonth.SelectedIndex = 0;
                    tbType.Text = "";
                    tbCategory.Text = "";
                    slQuality.Value = 0;
                    cbOpenSource.IsChecked = false;
                    cbInHouse.IsChecked = false;
                    tbReception.Value = 0;
                    ratPopularity.Value = 5;
                    tbSequelTo.Text = "";
                    lbFeatures.ItemsSource = null;
                    lbOS.ItemsSource = null;
                    dgProducts.ItemsSource = null;
                }
                //dgProducts.ItemsSource = null;
                dgProducts.ItemsSource = Main.CompanyManager.Companies.Current.Products;
            }
        }

        /// <summary>
        /// sets the Current Product and updates the UI
        /// </summary>
        /// <param name="item"></param>
        public void Update(Product item)
        {
            Main.CompanyManager.Companies.Current.Products.SetCurrent(item);
            this.Update();
        }

        /// <summary>
        /// sets the Current selected Product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgProducts_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grid.GridSelectionChangedEventArgs e)
        {
            if (dgProducts.SelectedItem != null && dgProducts.SelectedItem is Product)
            {
                Main.CompanyManager.Companies.Current.Products.SetCurrent(dgProducts.SelectedItem as Product);
                this.Update();
            }
        }

        /// <summary>
        /// adds a OS to the List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOSAdd_Click(object sender, RoutedEventArgs e)
        {
            var entry = new EntryBox(Properties.Resources.EntryOS).Show();
            if (!string.IsNullOrEmpty(entry))
            {
                Main.CompanyManager.Companies.Current.Products.GetCurrent().OS.Add(entry);
                this.Update();
            }
        }

        /// <summary>
        /// removes the selected OS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOSDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbOS.SelectedItem != null)
            {
                Main.CompanyManager.Companies.Current.Products.GetCurrent().OS.Remove(lbOS.SelectedItem.ToString());
                this.Update();
            }
        }

        /// <summary>
        /// adds a new Feature to the OS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFeatureAdd_Click(object sender, RoutedEventArgs e)
        {
            var entry = new EntryBox(Properties.Resources.EntryFeature).Show();
            if (!string.IsNullOrEmpty(entry))
            {
                Main.CompanyManager.Companies.Current.Products.GetCurrent().Features.Add(entry);
                this.Update();
            }
        }

        /// <summary>
        /// removes the selected Feature
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFeatureDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbFeatures.SelectedItem != null)
            {
                Main.CompanyManager.Companies.Current.Products.GetCurrent().Features.Remove(lbFeatures.SelectedItem.ToString());
                this.Update();
            }
        }

        /// <summary>
        /// saves the Current Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Product p = Main.CompanyManager.Companies.GetCurrent().Products.GetCurrent();
            p.Name = tbProductName.Text;
            p.ReleaseMonth = cbReleaseMonth.SelectedIndex + 1;
            p.ReleaseYear = Convert.ToInt32(tbReleaseYear.Value);
            p.Type = tbType.Text;
            p.Category = tbCategory.Text;
            p.Quality = slQuality.Value / 100;
            p.OpenSource = (bool)cbOpenSource.IsChecked;
            p.InHouse = (bool)cbInHouse.IsChecked;
            p.Reception = Convert.ToInt32(tbReception.Value);
            p.Popularity = ratPopularity.Value;
            p.SequelTo = tbSequelTo.Text;

            if (Main.CompanyManager.Companies.Current.Products.Current == null)
                Main.CompanyManager.Companies.Current.Products.New(p);

            this.Saved = true;
            this.Update(null);
        }


        /// <summary>
        /// removes the Current selected Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show(Properties.Resources.SureQuestion, "!!!", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {
                Main.CompanyManager.Companies.Current.Products.RemoveCurrent();
                this.Update();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (!Saved)
            {
                var answer = MessageBox.Show(Properties.Resources.ChangesNotSave, "", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes)
                {
                    this.Update(null);
                    this.Saved = true;
                }
            }
            else this.Update(null);
        }
    }
}
