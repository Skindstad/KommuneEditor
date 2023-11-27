using KommuneEditor.Model;
using KommuneEditor.ViewModel;
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

namespace KommuneEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel model = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = model;
            model.WarningHandler += delegate (object sender, MessageEventArgs e) {
                MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
            
        }
        private void grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                try
                {
                    DataGridRow row = sender as DataGridRow;
                    Data data = (Data)row.Item;
                    model.UpdateContact(data);
                }
                catch
                {
                }
            }
        }
    }
}
