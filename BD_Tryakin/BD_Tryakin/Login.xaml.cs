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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        GippoContext db = new GippoContext();

        public Login()
        {
            InitializeComponent();
            LoginButton.Click += Login_Click;
            RegistrationButton.Click += RedirectToRegistration;

            db.Users.Load();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Users a = db.Users.Find(LoginBox.Text);
            if (a != null && PasswordBox.Password == a.Password_User)
            {
                if (a.IsAdmin_User)
                {
                    MainWindow_Admin mw = new MainWindow_Admin();
                    mw.Show();
                    this.Close();
                }
                else
                {
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Логин или пароль введены неверно", "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void RedirectToRegistration(object sender, RoutedEventArgs e)
        {
            Registration a = new Registration();
            a.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
