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
using System.Windows.Input;

namespace KommuneEditor.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand CreateCommand { get; private set; }
        public RelayCommand DataCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public static DataRepository repository = new DataRepository();
        private ObservableCollection<Data> bindData;
        private ObservableCollection<string> cityOptions;



        public MainViewModel()
        {
            //SearchCommand = new RelayCommand(p => Search(), p => CanSearch());
            //CreateCommand = new RelayCommand(p => (new CreateWindow()).ShowDialog());
            //DataCommand = new RelayCommand(p => (new DataWindow()).ShowDialog());
            ClearCommand = new RelayCommand(p => Clear());
            bindData = new ObservableCollection<Data>(repository);
            repository.RepositoryChanged += Refresh;
            CityOptions = new ObservableCollection<string>(repository.Select(data => data.City).Distinct());

        }

        private void Refresh(object sender, DbEventArgs e)
        {
            BindData = new ObservableCollection<Data>(repository);
        }

        public ObservableCollection<Data> BindData
        {
            get
            {
                return bindData ?? (bindData = new ObservableCollection<Data>());
            }
            set
            {
                if (!ReferenceEquals(bindData, value))
                {
                    bindData = value;
                    OnPropertyChanged(nameof(BindData));
                }
            }
        }

        public ObservableCollection<string> CityOptions
        {
            get { return cityOptions; }
            set
            {
                if (cityOptions != value)
                {
                    cityOptions = value;
                    OnPropertyChanged(nameof(CityOptions));
                }
            }
        }

        private void Clear()
        {

        }
        /*
        private void Search()
        {
            try
            {
                repository.Search();
                BindData = new ObservableCollection<Data>(repository);
            }
            catch (Exception ex)
            {
                OnWarning(ex.Message);
            }
        }
        /*
        public void UpdateContact(Data data)
        {
            UpdateWindow dlg = new UpdateWindow(bindData);
            dlg.ShowDialog();
        }
        */
        private bool CanSearch()
        {
            return true;
        }
        
    }
}

