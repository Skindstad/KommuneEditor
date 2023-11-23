using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KommuneEditor.Model
{
    internal class Aarstal : IDataErrorInfo, IComparable<Aarstal>
    {
        public string Year { get; set; }

        public Aarstal()
        {
            Year = "";
        }

        public Aarstal(string year)
        {
            Year = year;
        }

        public override bool Equals(object obj)
        {
            try
            {
                Aarstal aarstal = (Aarstal)obj;
                return Year.Equals(aarstal.Year);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Year.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} ", Year);
        }

        // Implementerer ordning af objekter, så der alene sammenlignes på postnummer.
        public int CompareTo(Aarstal aarstal)
        {
            return Year.CompareTo(aarstal.Year);
        }

        public bool IsValid
        {
            get { return Year != null && YearOk(Year.Trim()); }
        }

        string IDataErrorInfo.Error
        {
            get { return IsValid ? null : "Illegal model object"; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return Validate(propertyName); }
        }

        private string Validate(string property)
        {
            if (property.Equals("Year")) return Year != null && YearOk(Year.Trim()) ? null : "Illegal Year";
            return null;
        }

        private bool YearOk(string year)
        {
            if (year.Length != 4) return false;
            foreach (char c in year) if (c < '0' || c > '9') return false;
            return true;
        }
    }
}
