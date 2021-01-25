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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BD_Tryakin.DB_Classes;
using System.Data;
using System.Configuration;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BD_Tryakin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GippoContext db = new GippoContext();

        public MainWindow()
        {
            InitializeComponent();
            Button_Add.Click += Button_Add_Click;
            Button_Save.Click += Button_Save_Click;
            Button_Refresh.Click += Button_Refresh_Click;
            Button_Report.Click += Button_Report_Click;
            Button_WriteOff.Click += Button_WriteOff_Click;
            db.Products.Load();
            Gippo_DG.ItemsSource = db.Products.Local.ToBindingList();
        }

        private void Button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            AddElement el = new AddElement();
            el.Show();
        }

        private void Button_Report_Click(object sender, RoutedEventArgs e)
        {
            ReportInvoice ri = new ReportInvoice();
            ri.Show();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }
        private void Button_WriteOff_Click(object sender, RoutedEventArgs e)
        {
            if (Gippo_DG.SelectedItems.Count > 0)
            {
                Products a = (Products)Gippo_DG.SelectedItem;
                if (a.ProductCount_Product > 0)
                {
                    a.ProductCount_Product = a.ProductCount_Product - 1;

                    Invoices i = new Invoices();
                    i.Name_Invoice = $"{DateTime.Now.DayOfWeek}-{DateTime.Now.ToShortTimeString()}-{DateTime.Now.Second}-{a.Name_Product}";
                    i.Date_Invoice = DateTime.Now;
                    i.ProductCount_Invoice = 1;
                    db.Invoices.Add(i);

                    db.SaveChanges();
                }
            }
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string componentsInfoQuery = string.Empty;

            if (Name.Text == string.Empty && CountP.Text == string.Empty && PriceP.Text == string.Empty)
            {
                System.Windows.MessageBox.Show("Поля поиска не заполнены.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //Название
            if (Name.Text != string.Empty && CountP.Text == string.Empty && PriceP.Text == string.Empty)
            {
                componentsInfoQuery = "SELECT Id_Product, Name_Product, ProductCount_Product,Description_Product, Price_Product " +
                "FROM ProductTypes " +
                "JOIN Products ON ProductTypes.Id_ProductType = Products.Id_ProductType " +
                "WHERE Name_Product = '" + Name.Text + "'";
            }
            //Количество
            if (Name.Text == string.Empty && CountP.Text != string.Empty && PriceP.Text == string.Empty)
            {
                componentsInfoQuery = "SELECT Id_Product, Name_Product, ProductCount_Product,Description_Product, Price_Product " +
                "FROM ProductTypes " +
                "JOIN Products ON ProductTypes.Id_ProductType = Products.Id_ProductType " +
                "WHERE ProductCount_Product = '" + Convert.ToInt32(CountP.Text) + "'";
            }
            //Цена
            if (Name.Text == string.Empty && CountP.Text == string.Empty && PriceP.Text != string.Empty)
            {
                componentsInfoQuery = "SELECT Id_Product, Name_Product, ProductCount_Product, Description_Product, Price_Product " +
                "FROM ProductTypes " +
                "JOIN Products ON ProductTypes.Id_ProductType = Products.Id_ProductType " +
                "WHERE Price_Product = '" + PriceP.Text + "'";
            }
            //Название Количество 
            if (Name.Text != string.Empty && CountP.Text != string.Empty && PriceP.Text == string.Empty)
            {
                componentsInfoQuery = "SELECT Id_Product, Name_Product, ProductCount_Product, Description_Product, Price_Product " +
                "FROM ProductTypes " +
                "JOIN Products ON ProductTypes.Id_ProductType = Products.Id_ProductType " +
                "WHERE Name_Product = '" + Name.Text + "' AND ProductCount_Product = '" + Convert.ToInt32(CountP.Text) + "'";
            }
            //Название цена
            if (Name.Text != string.Empty && CountP.Text == string.Empty && PriceP.Text != string.Empty)
            {
                componentsInfoQuery = "SELECT Id_Product, Name_Product, ProductCount_Product, Description_Product, Price_Product " +
                "FROM ProductTypes " +
                "JOIN Products ON ProductTypes.Id_ProductType = Products.Id_ProductType " +
                "WHERE Name_Product = '" + Name.Text + "'AND Price_Product = '" + PriceP.Text + "'";
            }
            //Количество цена
            if (Name.Text == string.Empty && CountP.Text != string.Empty && PriceP.Text != string.Empty)
            {
                componentsInfoQuery = "SELECT Id_Product, Name_Product, ProductCount_Product, Description_Product, Price_Product " +
                "FROM ProductTypes " +
                "JOIN Products ON ProductTypes.Id_ProductType = Products.Id_ProductType " +
                "WHERE ProductCount_Product = '" + Convert.ToInt32(CountP.Text) + "'AND Price_Product = '" + PriceP.Text + "'";
            }
            //Название количество цена
            if (Name.Text != string.Empty && CountP.Text != string.Empty && PriceP.Text != string.Empty)
            {
                componentsInfoQuery = "SELECT Id_Product, Name_Product, ProductCount_Product, Description_Product, Price_Product " +
                "FROM ProductTypes " +
                "JOIN Products ON ProductTypes.Id_ProductType = Products.Id_ProductType " +
                "WHERE Name_Product  = '" + Name.Text + "'AND ProductCount_Product = '" + Convert.ToInt32(CountP.Text) + "'AND Price_Product  = '" + PriceP.Text + "'";
            }
            string connectionString = @"Data Source = (local)\SQLEXPRESS; Initial Catalog = GippoBase; Integrated Security = True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            DataTable table = new DataTable();
            using (SqlCommand cmd = new SqlCommand(componentsInfoQuery, connection))
            {
                using (IDataReader rdr = cmd.ExecuteReader())
                {
                    table.Load(rdr);
                }
            }
            Gippo_DG.ItemsSource = table.DefaultView;
            connection.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ButtonReference_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = HelpNavigator.Topic;
            Help.ShowHelp(null, @"Help.chm", navigator);
        }
    }
}
