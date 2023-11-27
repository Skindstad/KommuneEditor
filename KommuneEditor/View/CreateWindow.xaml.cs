using System;
using System.Windows;
using KommuneEditor.ViewModel;
using KommuneEditor.Model;

namespace KommuneEditor.View
{
    /// <summary>
    /// Interaction logic for CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow : Window
    {
        public CreateWindow()
        {
            InitializeComponent();
            /*
            model.CloseHandler += delegate (object sender, EventArgs e) { Close(); };
            model.WarningHandler += delegate (object sender, MessageEventArgs e) {
                MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
            DataContext = model;
            */
        }
    }
}
