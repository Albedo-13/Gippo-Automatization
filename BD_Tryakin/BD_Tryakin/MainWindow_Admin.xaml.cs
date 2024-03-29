﻿using System;
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
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;
using BD_Tryakin.DB_Classes;
using System.Data;
using System.Windows.Forms;

namespace BD_Tryakin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow_User.xaml
    /// </summary>
    public partial class MainWindow_Admin : Window
    {
        GippoContext db = new GippoContext();

        public MainWindow_Admin()
        {
            InitializeComponent();
            Button_Add.Click += Button_Add_Click;
            Button_Save.Click += Button_Save_Click;
            Button_Delete.Click += Button_Delete_Click;
            Button_WriteOff.Click += Button_WriteOff_Click;
            Button_Report.Click += Button_Report_Click;
            Button_Refresh.Click += Button_Refresh_Click;

            db.Products.Load();
            Gippo_DG.ItemsSource = db.Products.Local.ToBindingList();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            AddElement el = new AddElement();
            el.Show();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void Button_Report_Click(object sender, RoutedEventArgs e)
        {
            ReportInvoice ri = new ReportInvoice();
            ri.Show();
        }

        private void Button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            MainWindow_Admin mw = new MainWindow_Admin();
            mw.Show();
            this.Close();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Gippo_DG.SelectedItems.Count > 0)
            {
                for (int i = 0; i < Gippo_DG.SelectedItems.Count; i++)
                {
                    Products foo = Gippo_DG.SelectedItems[i] as Products;
                    ProductTypes bar = Gippo_DG.SelectedItems[i] as ProductTypes;

                    if (foo != null)
                        db.Products.Remove(foo);
                    if (bar != null)
                        db.ProductTypes.Remove(bar);
                }
                db.SaveChanges();
            }
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
            MainWindow_Admin mw = new MainWindow_Admin();
            mw.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        // Разобраться в ресурсами
        private void Button_Click(object sender, RoutedEventArgs e)//Поиск по критериям
        {
            var included_FK_Tables = db.Products.Include(fk => fk.ProductTypes);

            var filtered = included_FK_Tables.Where(v => v.Name_Product.Contains(Name.Text)
            && v.ProductCount_Product.ToString().Contains(CountP.Text)
            && v.Price_Product.ToString().Contains(PriceP.Text));

            Gippo_DG.ItemsSource = filtered.ToList();
        }

        private void ButtonReference_Click(object sender, RoutedEventArgs e)
        {
            HelpNavigator navigator = HelpNavigator.Topic;
            Help.ShowHelp(null, @"Help.chm", navigator);
        }
    }
}
