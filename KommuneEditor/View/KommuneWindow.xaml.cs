using System;
using System.Windows;
using KommuneEditor.ViewModel;

namespace KommuneEditor.View
{
    /// <summary>
    /// Interaction logic for KommuneWindow.xaml
    /// </summary>
    public partial class KommuneWindow : Window
    {
        private KommuneViewModel model = new KommuneViewModel();

        public KommuneWindow()
        {
            InitializeComponent();
            model.WarningHandler += delegate (object sender, MessageEventArgs e) {
                MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
            model.CloseHandler += delegate (object sender, EventArgs e) { Close(); };
            DataContext = model;
        }
    }
}
