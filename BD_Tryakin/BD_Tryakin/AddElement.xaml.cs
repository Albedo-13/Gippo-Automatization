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
    /// Логика взаимодействия для AddElement.xaml
    /// </summary>
    public partial class AddElement : Window
    {
        Products newProduct = new Products();
        ProductTypes newType = new ProductTypes();
        PurchaseInvoices newPurchaseInvoice = new PurchaseInvoices();

        GippoContext db;

        public AddElement()
        {
            InitializeComponent();
            AddButton.Click += Button_Click;
            RefreshButton.Click += RefreshButton_Click;

            db = new GippoContext();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text != string.Empty 
                && Count.Text != string.Empty 
                && Description.Text != string.Empty 
                && Cost.Text != string.Empty
                && Type.Text != string.Empty
                && Convert.ToInt32(Count.Text) >= 0
                && Convert.ToInt32(Cost.Text) >= 0)
            {   // Высрал конечно мдааа
                AddButton.IsEnabled = true;
            }
            else
            {
                AddButton.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newProduct.Name_Product = Name.Text;
            newProduct.ProductCount_Product = Convert.ToInt32(Count.Text);
            newProduct.Description_Product = Description.Text;
            newProduct.Price_Product = Convert.ToInt32(Cost.Text);

            newType.Name_ProductType = Type.Text;

            newPurchaseInvoice.Name_PurchaseInvoice = $"{DateTime.Now.DayOfWeek}-{DateTime.Now.ToShortTimeString()}-{DateTime.Now.Second}-{newProduct.Name_Product}";
            newPurchaseInvoice.Date_PurchaseInvoice = DateTime.Now;
            newPurchaseInvoice.ProductCount_PurchaseInvoice = newProduct.ProductCount_Product;

            newProduct.ID_Invoice = null;
            newProduct.ID_PurchaseInvoice = null;
            newProduct.Login_User = null;

            db.Products.Add(newProduct);
            db.ProductTypes.Add(newType);
            db.PurchaseInvoices.Add(newPurchaseInvoice);
            db.SaveChanges();

            this.Close();
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
