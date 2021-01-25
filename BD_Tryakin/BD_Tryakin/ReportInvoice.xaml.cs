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
using System.IO;

namespace BD_Tryakin
{
    /// <summary>
    /// Логика взаимодействия для ReportInvoice.xaml
    /// </summary>
    public partial class ReportInvoice : Window
    {
        GippoContext db = new GippoContext();

        public ReportInvoice()
        {
            InitializeComponent();
            ReportButton.Click += ReportButton_Click;

            db.Invoices.Load();
            db.PurchaseInvoices.Load();

            PurchaseInvoices_LV.ItemsSource = db.PurchaseInvoices.Local.ToBindingList();
            Invoices_LV.ItemsSource = db.Invoices.Local.ToBindingList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dg = PurchaseInvoices_LV;
            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            var result = (string)Clipboard.GetData(DataFormats.Text);
            dynamic wordApp = null;
            try
            {
                var sw = new StreamWriter("Приходная_Накладная.doc");
                sw.WriteLine(result);
                sw.Close();
                Type wordType = Type.GetTypeFromProgID("Word.Application");
                wordApp = Activator.CreateInstance(wordType);
                wordApp.Documents.Add(System.AppDomain.CurrentDomain.BaseDirectory + "Приходная_Накладная.doc");
                wordApp.ActiveDocument.Range.ConvertToTable(1, dg.Items.Count, dg.Columns.Count);
                wordApp.Visible = true;
            }
            catch (Exception ex)
            {
                if (wordApp != null)
                {
                    wordApp.Quit();
                }
            }

            this.Close();
        }

        private void ReportButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dg = Invoices_LV;
            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            var result = (string)Clipboard.GetData(DataFormats.Text);
            dynamic wordApp = null;
            try
            {
                var sw = new StreamWriter("Выходная_Накладная.doc");
                sw.WriteLine(result);
                sw.Close();
                Type wordType = Type.GetTypeFromProgID("Word.Application");
                wordApp = Activator.CreateInstance(wordType);
                wordApp.Documents.Add(System.AppDomain.CurrentDomain.BaseDirectory + "Выходная_Накладная.doc");
                wordApp.ActiveDocument.Range.ConvertToTable(1, dg.Items.Count, dg.Columns.Count);
                wordApp.Visible = true;
            }
            catch (Exception ex)
            {
                if (wordApp != null)
                {
                    wordApp.Quit();
                }
            }

            this.Close();
        }
    }
}
