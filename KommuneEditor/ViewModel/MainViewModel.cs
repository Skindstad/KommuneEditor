using KommuneEditor.DataAccess;
using KommuneEditor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using KommuneEditor.View;

namespace KommuneEditor.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand CreateCommand { get; private set; }
        public RelayCommand KommuneCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public static DataRepository repository = new DataRepository();
        private ObservableCollection<Data> bindData;
        private string dataId = "";
        private string komNr = "";
        private string city = "";
        private string gruppe = "";
        private string year = "";
        private string num = "";


        public MainViewModel()
        {
            SearchCommand = new RelayCommand(p => Search(), p => CanSearch());
            CreateCommand = new RelayCommand(p => (new CreateWindow()).ShowDialog());
            KommuneCommand = new RelayCommand(p => (new KommuneWindow()).ShowDialog());
            ClearCommand = new RelayCommand(p => Clear());
            bindData = new ObservableCollection<Data>(repository);
            repository.RepositoryChanged += Refresh;
        }

        private void Refresh(object sender, DbEventArgs e)
        {
            BindData = new ObservableCollection<Data>(repository);
        }
        
        public ObservableCollection<Data> BindData
        {
            get { return bindData; }
            set
            {
                if (!bindData.Equals(value))
                {
                    bindData = value;
                    OnPropertyChanged("BindData");
                }
            }
        }

        public string DataId
        {
            get { return dataId; }
            set
            {
                if (!dataId.Equals(value))
                {
                    dataId = value;
                    OnPropertyChanged("DataId");
                }
            }
        }

        public string KomNr
        {
            get { return komNr; }
            set
            {
                if (!komNr.Equals(value))
                {
                    komNr = value;
                    OnPropertyChanged("KomNr");
                }
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                if (!city.Equals(value))
                {
                    city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        public string Gruppe
        {
            get { return gruppe; }
            set
            {
                if (!gruppe.Equals(value))
                {
                    gruppe = value;
                    OnPropertyChanged("Gruppe");
                }
            }
        }

        public string Year
        {
            get { return year; }
            set
            {
                if (!year.Equals(value))
                {
                    year = value;
                    OnPropertyChanged("Year");
                }
            }
        }

        public string Num
        {
            get { return num; }
            set
            {
                if (!num.Equals(value))
                {
                    num = value;
                    OnPropertyChanged("Num");
                }
            }
        }

        private void Clear()
        {
            DataId = "";
            KomNr = "";
            City = "";
            Gruppe = "";
            Year = "";
            Num = "";
        }
        
        private void Search()
        {
            try
            {
                repository.Search(komNr, city, gruppe, year);
                BindData = new ObservableCollection<Data>(repository);
            }
            catch (Exception ex)
            {
                OnWarning(ex.Message);
            }
        }
        
        public void UpdateContact(Data data)
        {
            UpdateWindow dlg = new UpdateWindow(data);
            dlg.ShowDialog();
        }
        
        private bool CanSearch()
        {
            return komNr.Length > 0 || city.Length > 0 ||
                gruppe.Length > 0 || year.Length > 0 ||
                num.Length > 0;
        }
        
    }
}

