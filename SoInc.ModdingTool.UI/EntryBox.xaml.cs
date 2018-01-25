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
using System.Windows.Shapes;

namespace SoInc.ModdingTool.UI
{
    /// <summary>
    /// Interaktionslogik für EntryBox.xaml
    /// </summary>
    public partial class EntryBox : BaseWindow
    {
        /// <summary>
        /// Internal Text for the InputText
        /// </summary>
        private string text;

        /// <summary>
        /// Initialize a new Entry Box
        /// </summary>
        /// <param name="question">question for the textblock</param>
        public EntryBox(string question)
        {
            InitializeComponent();
            tbQuestion.Text = question;
        }

        /// <summary>
        /// returns the Text 
        /// </summary>
        /// <returns></returns>
        public new string Show()
        {
            ShowDialog();

            return text;
        }

        /// <summary>
        /// Initialize a new EntryBox and show it. Returns the Result
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static string Show(string question)
        {
            var entry = new EntryBox(question);
            return entry.Show();
        }

        /// <summary>
        /// Returns a Empty String
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            text = "";
            this.Close();
        }

        /// <summary>
        /// Returns the Result-string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            text = tbEntry.Text;
            this.Close(); 
        }
    }
}
