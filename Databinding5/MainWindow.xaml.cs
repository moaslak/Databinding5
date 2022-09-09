using Databinding5;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
        Person person; //TODO: make nullable
        private ObservableCollection<Person> publicListe; // Dette er objektet med elementer vi 
                                                           // "deler ud" til brugeren af vores class.
        DAL DAL = new DAL();
        public MainWindow()
        {
            
            InitializeComponent();
            
            publicListe = DAL.Get();
            this.DataContext = publicListe;
        }

        private ObservableCollection<Person> Insert(Person person)
        {
            ObservableCollection<Person> people = DAL.Get();
            people.Add(person);
            publicListe = people;
            DAL.Commit();
            return people;
        }

        private ObservableCollection<Person> DeletePerson(Person person)
        {
            ObservableCollection<Person> data = DAL.Get();

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].ID == person.ID)
                {
                    data.RemoveAt(i);
                }
            }
            publicListe = data;
            DAL.Commit();
            return data;
        }

        private void VisDataBtn_Click(object sender, RoutedEventArgs e)
        {
            string PersonData = FornavnTxtbox.Text +
                " " +
                EfternavnTxtbox.Text +
                " har en formue på " +
                FormueTxtbox.Text +
                " Kr.";
            person.Fornavn = FornavnTxtbox.Text;
            person.Efternavn = EfternavnTxtbox.Text;
            person.Formue = Convert.ToInt32(FormueTxtbox.Text);
            person.ID = Convert.ToInt32(IDTxtbox.Text);
            MessageBox.Show(PersonData);
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            person.Formue++;
            DAL.Commit();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeletePerson(person);
            DAL.Commit();
        }

        private void Get_Click(object sender, RoutedEventArgs e)
        {
            publicListe.Clear();
            publicListe = DAL.Get();
        }

        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Person person = new Person(Index + 1, FornavnTxtbox.Text, EfternavnTxtbox.Text, int.Parse(FormueTxtbox.Text));
                Insert(person);
                DAL.Commit();
            }
            catch (FormatException)
            {
                Console.WriteLine("Format exception");
            }
        }
    }
}
