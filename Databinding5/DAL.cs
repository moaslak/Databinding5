using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoWayTest;

namespace Databinding5
{
    internal class DAL
    {
        private ObservableCollection<Person> DataBase; // Da vi ikke har adgang til en database, 
                                                       // simulerer vi med denne private liste....

        private ObservableCollection<Person> _publicListe; // Dette er objektet med elementer vi 
                                                           // "deler ud" til brugeren af vores class.
        
        public DAL()
        {
            DataBase = new ObservableCollection<Person>();
            DataBase.Add(new Person(0, "Svend", "Bendt", 1234));
            DataBase.Add(new Person(1, "Bein", "Stagge", -987654321));
            DataBase.Add(new Person(2, "Turt", "Khorsen", 0));
            DataBase.Add(new Person(3, "Gill", "Bates", int.MaxValue));

            _publicListe = new ObservableCollection<Person>();
        }
        public ObservableCollection<Person> Get()
        {
            _publicListe.Clear();     //Først tømmes den lokale kopi

            //Så løber vi alle elementerne igennem i databasen og overfører til lokal kopi
            foreach (Person p in DataBase)
            {
                _publicListe.Add(p);
            }

            return _publicListe;
        }

        public ObservableCollection<Person> Update(Person newPerson)
        {
            ObservableCollection<Person> data = Get();

            for(int i = 0; i < data.Count; i++)
            {
                if (data[i].ID == newPerson.ID)
                {
                    data[i] = newPerson;
                }
            }
            return data;
        }

        public ObservableCollection<Person> Insert(Person person)
        {
            ObservableCollection<Person> people = Get();
            people.Add(person);
            return people;
        }

        public ObservableCollection<Person> DeletePerson(Person Person)
        {
            ObservableCollection<Person> data = Get();

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].ID == Person.ID)
                {
                    data.RemoveAt(i);
                }
            }
            return data;
        }

        public void Commit()
        {
            DataBase = new ObservableCollection<Person>(_publicListe);
        }
    }
}
