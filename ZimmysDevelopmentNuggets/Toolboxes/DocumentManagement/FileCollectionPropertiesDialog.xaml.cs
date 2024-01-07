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
using Microsoft.VisualStudio.PlatformUI;

namespace ZimmysDevelopmentNuggets.Toolboxes.DocumentManagement
{
    /// <summary>
    /// Interaktionslogik für FileCollectionPropertiesDialog.xaml
    /// </summary>
    public partial class FileCollectionPropertiesDialog : DialogWindow
    {
        public FileCollectionPropertiesDialog()
        {
            InitializeComponent();

            this.HasMinimizeButton = false;
            this.HasMaximizeButton = false;
        }

        public string Name { get; set; }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            Name = txtName.Text;
            DialogResult = true;
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
