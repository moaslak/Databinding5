using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TwoWayTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Person> Personer = new List<Person>();
        public int Index = 4;

        Person person = new Person(0, "Svend", "Bendt", 123);
        private ObservableCollection<Person> DataBase; // Da vi ikke har adgang til en database, 
                                                       // simulerer vi med denne private liste....

        private ObservableCollection<Person> _publicListe; // Dette er objektet med elementer vi 
                                                           // "deler ud" til brugeren af vores class.
        public MainWindow()
        {
            InitializeComponent();

            DataBase = new ObservableCollection<Person>();
            DataBase.Add(new Person(0, "Svend", "Bendt", 1234));
            DataBase.Add(new Person(1, "Bein", "Stagge", -987654321));
            DataBase.Add(new Person(2, "Turt", "Khorsen", 0));
            DataBase.Add(new Person(3, "Gill", "Bates", int.MaxValue));

            _publicListe = new ObservableCollection<Person>();

            _publicListe = Get();

            this.DataContext = _publicListe;
        }

        private ObservableCollection<Person> Get()
        {
            _publicListe.Clear();     //Først tømmes den lokale kopi

            //Så løber vi alle elementerne igennem i databasen og overfører til lokal kopi
            foreach (Person p in DataBase)
            {
                _publicListe.Add(p);
            }

            return _publicListe;
        }

        private ObservableCollection<Person> Update(Person newPerson)
        {
            ObservableCollection<Person> data = Get();

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].ID == newPerson.ID)
                {
                    data[i] = newPerson;
                }
            }
            Commit();
            return data;
        }

        private ObservableCollection<Person> Insert(Person person)
        {
            ObservableCollection<Person> people = Get();
            people.Add(person);
            _publicListe = people;
            Commit();
            return people;
        }

        private ObservableCollection<Person> DeletePerson(Person Person)
        {
            ObservableCollection<Person> data = Get();

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].ID == Person.ID)
                {
                    data.RemoveAt(i);
                }
            }
            _publicListe = data;
            Commit();
            return data;
        }

        public void Commit()
        {
            DataBase = new ObservableCollection<Person>(_publicListe);
        }

        private void VisDataBtn_Click(object sender, RoutedEventArgs e)
        {
            string PersonData = person.Fornavn +
                " " +
                person.Efternavn +
                " har en formue på " +
                person.Formue +
                " Kr.";

            MessageBox.Show(PersonData);
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            person.Formue++;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeletePerson(person);
            Commit();
            
        }

        private void Get_Click(object sender, RoutedEventArgs e)
        {
        }

        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Person person = new Person(Index + 1, FornavnTxtbox.Text, EfternavnTxtbox.Text, int.Parse(FormueTxtbox.Text));
                Insert(person);
            }
            catch (FormatException)
            {
                Console.WriteLine("Format exception");
            }
            
        }
    }
}
