using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using KommuneEditor.Model;
using KommuneEditor.DataAccess;


namespace KommuneEditor.ViewModel
{
    public class KommuneViewModel : ViewModelBase, IDataErrorInfo
    {
        public RelayCommand SelectCommand { get; private set; }
        public RelayCommand RemoveCommand { get; private set; }
        public RelayCommand InsertCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand UpdateCommand { get; private set; }
        private Kommune model = new Kommune();
        private KommuneRepository repository = new KommuneRepository();
        private ObservableCollection<Kommune> info;

        public KommuneViewModel()
        {
            repository.RepositoryChanged += ModelChanged;
            Search();
            UpdateCommand = new RelayCommand(p => Update(), p => CanUpdate());
            SelectCommand = new RelayCommand(p => Search());
            ClearCommand = new RelayCommand(p => Clear());
            InsertCommand = new RelayCommand(p => Add(), p => CanAdd());
            RemoveCommand = new RelayCommand(p => Remove(), p => CanRemove());
        }

        public ObservableCollection<Kommune> Info
        {
            get { return info; }
            set
            {
                if (info != value)
                {
                    info = value;
                    OnPropertyChanged("Info");
                }
            }
        }

        public void ModelChanged(object sender, DbEventArgs e)
        {
            if (e.Operation != DbOperation.SELECT)
            {
                Clear();
            }
            Info = new ObservableCollection<Kommune>(repository);
        }

        public string KomNr
        {
            get { return model.KomNr; }
            set
            {
                if (!model.KomNr.Equals(value))
                {
                    model.KomNr = value;
                    OnPropertyChanged("KomNr");
                }
            }
        }

        public string City
        {
            get { return model.City; }
            set
            {
                if (!model.City.Equals(value))
                {
                    model.City = value;
                    OnPropertyChanged("City");
                }
            }
        }

        public Kommune SelectedModel
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged("KomNr");
                OnPropertyChanged("City");
                OnPropertyChanged("SelectedModel");
            }
        }

        private void Clear()
        {
            SelectedModel = null;
            model = new Kommune();
            OnPropertyChanged("KomNr");
            OnPropertyChanged("City");
            OnPropertyChanged("SelectedModel");
        }

        public void Search()
        {
            repository.Search(KomNr, City);
        }

        public void Update()
        {
            try
            {
                repository.Update(KomNr, City);
            }
            catch (Exception ex)
            {
                OnWarning(ex.Message);
            }
        }

        private bool CanUpdate()
        {
            return model.IsValid;
        }

        public void Add()
        {
            try
            {
                repository.Add(KomNr, City);
            }
            catch (Exception ex)
            {
                OnWarning(ex.Message);
            }
        }

        public bool CanAdd()
        {
            return model.IsValid;
        }

        public void Remove()
        {
            try
            {
                repository.Remove(KomNr);
            }
            catch (Exception ex)
            {
                OnWarning(ex.Message);
            }
        }

        private bool CanRemove()
        {
            return KomNr != null && KomNr.Length > 0;
        }

        string IDataErrorInfo.Error
        {
            get { return (model as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;
                try
                {
                    error = (model as IDataErrorInfo)[propertyName];
                }
                catch
                {
                }
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }
    }
}
