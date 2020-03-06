using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

namespace aflving_opgave
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Person> listen = new ObservableCollection<Person>();
        public MainWindow()
        {
            InitializeComponent();

            listBox.ItemsSource = listen;
            personlistgrid.DataContext = listen;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            List<Person> people = new List<Person>();
            string s = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                s = File.ReadAllText(openFileDialog.FileName);

            var list = s.Split(';');
            for (int i = 0; i < list.Length-4; i = i + 4)
            {
                listen.Add(new Person(list[i] , int.Parse(list[i + 1]) , int.Parse(list[i + 2]) , int.Parse(list[i + 3])));
            }
             int antal = listBox.Items.Count;
            statusbarlabel.Content = "antal items i listen "+antal + " sidste ændring i listen "+DateTime.Now;


        }
        public class Person : INotifyPropertyChanged
        {
            private int _age;
            private string _name;
            private int _score;
            private int _id;

            public Person(string name ,int id,int score,int age) 
            {
                Id = id;
                Name = name;
                Age = age;
                Score = score;
            }
            public string Name
            {
                get { return _name; }
                set { _name = value; NotifyPropertyChanged(nameof(Name)); }
            }
             public int Age
            {
                get { return _age; }
                set { _age = value; NotifyPropertyChanged(nameof(Age)); }
            }
            public int Score
            {
                get { return _score; }
                set { _score = value; NotifyPropertyChanged(nameof(Score)); }
            }
            public int Id
            {
                get { return _id; }
                set { _id = value; NotifyPropertyChanged(nameof(Id)); }

            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public string ListBoxToString
            {
                get
                {
                    return $"Name: {Name}- ID: {Id}";
                }
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Nameinfo.DataContext = listBox.SelectedItem;
            Ageinfo.DataContext = listBox.SelectedItem;
            Idinfo.DataContext = listBox.SelectedItem;
            Scoreinfo.DataContext = listBox.SelectedItem;
        }
    }
}
