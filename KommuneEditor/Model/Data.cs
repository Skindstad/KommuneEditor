using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KommuneEditor.Model
{
    internal class Data : IDataErrorInfo, IComparable<Data>
    {
        public string DataId { get; set; } // Id
        public string Kommune {  get; set; } // Kommune kom_nr
        public string Keynummer { get; set; } // NøgleTal ID
        public string Aarstal {  get; set; } // Year
        public string Num {  get; set; } // Tal

        public Data() 
        {
            DataId = "";
            Kommune = "";
            Keynummer = "";
            Aarstal = "";
            Num = "";
        }

        public Data(string dataID, string kommune, string keynummer, string aarstal, string num)
        {
            DataId = dataID;
            Kommune = kommune;
            Keynummer = keynummer;
            Aarstal = aarstal;
            Num = num;
        }

        public override bool Equals(object obj)
        {
            try
            {
                Data data = (Data)obj;
                return DataId.Equals(data.DataId);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return DataId.GetHashCode();
        }

      /*  public override string ToString()
        {
            return string.Format("{0} ", DataID);
        }*/

        // Implementerer ordning af objekter, så der alene sammenlignes på postnummer.
        public int CompareTo(Data data)
        {
            return DataId.CompareTo(data.DataId);
        }
        private static readonly string[] validatedProperties = { "DataId", "Kommune", "Keynummer", "Aarstal", "Tal"};
        public bool IsValid
        {
            get {
                foreach (string property in validatedProperties) if (GetError(property) != null) return false;
                return true;
            }
        }

        string IDataErrorInfo.Error
        {
            get { return IsValid ? null : "Illegal model object"; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return Validate(propertyName); }
        }

        private string GetError(string name)
        {
            foreach (string property in validatedProperties) if (property.Equals(name)) return Validate(name);
            return null;
        }

        private string Validate(string name)
        {
            switch (name)
            {
                case "DataId": return ValidateDataId();
                case "Kommune": return ValidateKommune();
                case "Keynummer": return ValidateKeynummer();
                case "Aarstal": return ValidateAarstal();
                case "Tal": return ValidateTal();
            }
            return null;
        }

        private string ValidateDataId()
        {
            foreach (char c in DataId) if (c < '0' || c > '9') return "Id must be a number";
            return null;
        }
        private string ValidateKommune()
        {
            if (Kommune.Length != 3) return "Phone must be a number of 3 digits";
            foreach (char c in Kommune) if (c < '0' || c > '9') return "Kommune must be a number of 3 digits";
            return null;
        }
        private string ValidateKeynummer()
        {
            foreach (char c in Keynummer) if (c < '0' || c > '9') return "Keynummer must be a number";
            return null;
        }
        private string ValidateAarstal()
        {
            if (Aarstal.Length != 4) return "Phone must be a number of 4 digits";
            foreach (char c in Aarstal) if (c < '0' || c > '9') return "Year must be a number of 4 digits";
            return null;
        }
        private string ValidateTal()
        {
            foreach (char c in Num) if (c < '0' || c > '9') return "Number must be a number";
            return null;
        }

     

    }
}
