using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KommuneEditor.Model
{
    internal class Keynummer : IDataErrorInfo, IComparable<Keynummer>
    {
        public string KeyID { get; set; }
        public string Gruppe {  get; set; }


        public Keynummer() 
        {
            KeyID = "";
            Gruppe = "";
        }

        public Keynummer(string keyID, string gruppe)
        {
            KeyID = keyID;
            Gruppe = gruppe;
        }

        public override bool Equals(object obj)
        {
            try
            {
                Keynummer keynummer = (Keynummer)obj;
                return KeyID.Equals(keynummer.KeyID);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return KeyID.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} ", KeyID);
        }

        // Implementerer ordning af objekter, så der alene sammenlignes på postnummer.
        public int CompareTo(Keynummer keynummer)
        {
            return KeyID.CompareTo(keynummer.KeyID);
        }

        public bool IsValid
        {
            get { return KeyID != null && NrOk(KeyID.Trim()) && Gruppe != null && Gruppe.Length > 0; }
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
            if (property.Equals("KeyID")) return KeyID != null && NrOk(KeyID.Trim()) ? null : "Illegal KeyID";
            if (property.Equals("Kommune")) return Gruppe != null && Gruppe.Length > 0 ? null : "Illegal gruppe";
            return null;
        }

        private bool NrOk(string keyID)
        {
            foreach (char c in keyID) if (c < '0' || c > '9') return false;
            return true;
        }
    }
}
