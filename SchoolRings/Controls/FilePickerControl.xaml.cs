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
using Microsoft.Win32;

namespace SchoolRings.Controls
{
    /// <summary>
    /// Interaction logic for FilePickerControl.xaml
    /// </summary>
    public partial class FilePickerControl : UserControl
    {
        public FilePickerControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SelectedFileProperty = DependencyProperty.Register("SelectedFile", typeof(string), typeof(FilePickerControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string SelectedFile
        {
            get {
                return (string)GetValue(SelectedFileProperty);
            }
            set {
                SetValue(SelectedFileProperty, value);
            }
        }

        public event EventHandler<string> FileSelected = new EventHandler<string>(((sender, s) => {})); 

        public string Filter
        {
            get;
            set;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (Filter != null)
            {
                openFileDialog.Filter = Filter;
            }
            if (openFileDialog.ShowDialog() == true)
            {
                SelectedFile = openFileDialog.FileName;
                this.FileSelected(this, SelectedFile);
            }
        }
    }
}
