using KommuneEditor.DataAccess;
using KommuneEditor.Model;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace KommuneEditor.ViewModel
{
    public class DataViewModel : ViewModelBase, IDataErrorInfo
    {
        public RelayCommand OkCommand { get; private set; }
        public RelayCommand ModCommand { get; private set; }
        public RelayCommand RemCommand { get; private set; }
        protected Data model;
        protected DataRepository repository;

        public DataViewModel(Data model, DataRepository repository)
        {
            this.model = model;
            this.repository = repository;
            OkCommand = new RelayCommand(p => Add(), p => CanUpdate());
            ModCommand = new RelayCommand(p => Update(), p => CanUpdate());
            RemCommand = new RelayCommand(p => Remove());
        }

        public Data Model
        {
            get { return model; }
        }

        public string DataId
        {
            get { return model.DataId; }
            set
            {
                if (!model.DataId.Equals(value))
                {
                    model.DataId = value;
                    OnPropertyChanged("DataId");
                }
            }
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

        public string Gruppe
        {
            get { return model.Gruppe; }
            set
            {
                if (!model.Gruppe.Equals(value))
                {
                    model.Gruppe = value;
                    OnPropertyChanged("Gruppe");
                }
            }
        }

        public string Year
        {
            get { return model.Year; }
            set
            {
                if (!model.Year.Equals(value))
                {
                    model.Year = value;
                    OnPropertyChanged("Year");
                }
            }
        }

        public string Num
        {
            get { return model.Num; }
            set
            {
                if (!model.Num.Equals(value))
                {
                    model.Num = value;
                    OnPropertyChanged("Num");
                }
            }
        }

        public void Clear()
        {
            model = new Data();
            OnPropertyChanged("DataId");
            OnPropertyChanged("KomNr");
            OnPropertyChanged("City");
            OnPropertyChanged("Gruppe");
            OnPropertyChanged("Year");
            OnPropertyChanged("Num");
        }

        public void Update()
        {
            if (IsValid)
            {
                try
                {
                    repository.Update(model);
                    OnClose();
                }
                catch (Exception ex)
                {
                    OnWarning(ex.Message);
                }
            }
        }

        public void Remove()
        {
            try
            {
                repository.Remove(model.DataId);
                OnClose();
            }
            catch (Exception ex)
            {
                OnWarning(ex.Message);
            }
        }

        public void Add()
        {
            if (IsValid)
            {
                try
                {
                    repository.Add(model);
                    Clear();
                }
                catch (Exception ex)
                {
                    OnWarning(ex.Message);
                }
            }
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

        private bool IsValid
        {
            get { return model.IsValid; }
        }

        private bool CanUpdate()
        {
            return IsValid;
        }
    }
}
