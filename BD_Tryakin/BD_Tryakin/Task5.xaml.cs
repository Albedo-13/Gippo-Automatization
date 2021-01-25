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
using System.Data.Entity;
using BD_Tryakin.DB_Classes;

namespace BD_Tryakin
{
    /// <summary>
    /// Логика взаимодействия для Task5.xaml
    /// </summary>
    public partial class Task5 : Window
    {
        GippoContext db = new GippoContext();
        List<Products> picked = new List<Products>() { };

        public Task5()
        {
            InitializeComponent();
            Task5a.Click += Task5a_Click;
            Back.Click += Back_Click;

            db.Products.Load();
        }

        private void Task5a_Click(object sender, RoutedEventArgs e)
        {
            picked = db.Products.Local.Where(v => v.ProductCount_Product > v.Price_Product).ToList();
            Gippo_DG.ItemsSource = picked;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow_Admin mw = new MainWindow_Admin();
            mw.Show();
            this.Close();
        }
    }
}
