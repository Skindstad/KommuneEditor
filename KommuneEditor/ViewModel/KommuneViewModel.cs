using KommuneEditor.DataAccess;
using KommuneEditor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KommuneEditor.ViewModel
{
    public class KommuneViewModel : ViewModelBase
    {
        protected Kommune model;
        protected KommuneRepository repository;

        public KommuneViewModel(Kommune model, KommuneRepository repository)
        {
            this.model = model;
            this.repository = repository;
        }

        public Kommune Model
        {
            get { return model; }
        }
    }
}
