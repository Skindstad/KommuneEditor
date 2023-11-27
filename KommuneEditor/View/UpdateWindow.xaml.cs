﻿using KommuneEditor.ViewModel;
using System;
using System.Windows;
using KommuneEditor.Model;

namespace KommuneEditor.View
{
    /// <summary>
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        private DataViewModel model;

        public UpdateWindow(Data data)
        {
            model = new DataViewModel(data, MainViewModel.repository);
            InitializeComponent();
            model.CloseHandler += delegate (object sender, EventArgs e) { Close(); };
            model.WarningHandler += delegate (object sender, MessageEventArgs e) {
                MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
            DataContext = model;
        }
    }
}
