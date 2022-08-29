using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TwoWayTest
{
    internal class Person : INotifyPropertyChanged
    {
        public int ID;
        private string _fornavn;
        public string Fornavn 
        {
            get
            {
                return _fornavn;
            }
            set
            {
                _fornavn = value;
                OnPropertyChanged("Fornavn");
            }
        }
        
        private string _efternavn;
        public string Efternavn 
        {
            get
            {
                return _efternavn;
            }
            set
            {
                _efternavn = value;
                OnPropertyChanged("Efternavn");
            } 
        }
        private int _formue;

        public int Formue 
        { 
            get
            {
                return _formue;
            }
            set 
            {
                _formue = value;
                OnPropertyChanged("Formue");
            } 
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string PropertyNavn)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyNavn));
            }
        }

        public Person(int ID, string Fornavn, string Efternavn, int Formue)
        {
            this.ID = ID;
            this.Fornavn = Fornavn;
            this.Efternavn = Efternavn;
            this.Formue = Formue;
        }

        

    }

}
