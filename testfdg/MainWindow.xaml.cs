using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace testfdg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<User> users;
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isAscending;

        public MainWindow()
        {
            InitializeComponent();

            Users = new ObservableCollection<User>();

            Users.Add(new User { Name = "Dave", Password = "1DavePwd" });
            Users.Add(new User { Name = "Steve", Password = "2StevePwd" });
            Users.Add(new User { Name = "Lisa", Password = "3LisaPwd" });

            uxList.ItemsSource = Users;
            head.Click += Test_Click;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged();
            }
        }



        private void Test_Click(object sender, RoutedEventArgs e)
        {
            //temp = isAscending ? new ObservableCollection<User>(temp.OrderBy(x => x.Name)) :
            //                     new ObservableCollection<User>(temp.OrderByDescending(x => x.Name));
            if (!isAscending)
            {
                var sortedUsers = Users.OrderBy(x => x.Name).ToList();
                Users = new ObservableCollection<User>(sortedUsers);
                uxList.ItemsSource = null;
                uxList.ItemsSource = Users;
                isAscending = true;
            }
            else
            {                    
                var sortedUsers = Users.OrderByDescending(x => x.Name).ToList();
                Users = new ObservableCollection<User>(sortedUsers);
                uxList.ItemsSource = null;
                uxList.ItemsSource = Users;
                isAscending = false;
            }

        }
    }

    public class User : INotifyPropertyChanged
    {
        private string name;
        protected virtual void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public string Name
        {
            get => name; set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string Password { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
