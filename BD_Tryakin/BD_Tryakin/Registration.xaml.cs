using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using BD_Tryakin.DB_Classes;
using System.Data.Entity;

namespace BD_Tryakin
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        GippoContext db = new GippoContext();

        public Registration()
        {
            InitializeComponent();
            RegistrationButton.Click += RegistrateUser;
            LoginButton.Click += RedirectToLogin;

            db.Users.Load();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void RegistrateUser(object sender, RoutedEventArgs e)
        {
            Users foo = db.Users.Find(LoginBox.Text);
            if (LoginBox.Text != string.Empty && foo == null)
            {
                if (PasswordBox.Password != string.Empty && NameBox.Text != string.Empty 
                    && SurnameBox.Text != string.Empty && AddressBox.Text != string.Empty)
                {
                    Users a = new Users(LoginBox.Text, PasswordBox.Password, NameBox.Text, SurnameBox.Text, AddressBox.Text);
                    db.Users.Add(a);
                    db.SaveChanges();
                    MessageBox.Show("Ваш аккаунт успешно создан");
                    Login c = new Login();
                    c.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены");
                }
            }
            else
            {
                MessageBox.Show("Этот логин занят, введите другой");
            }
        }

        private void RedirectToLogin(object sender, RoutedEventArgs e)
        {
            Login a = new Login();
            a.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
