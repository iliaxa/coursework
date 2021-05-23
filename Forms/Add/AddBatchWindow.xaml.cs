using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApplicationEntity.API;
using System.Data.Entity.Migrations;
namespace WpfApplicationEntity.Forms
{
    /// <summary>
    /// Логика взаимодействия для AddBatchWindow.xaml
    /// </summary>
    public partial class AddBatchWindow : Window
    {
        public AddBatchWindow()
        {
            InitializeComponent();
        }
        public AddBatchWindow(int ID)
        {
            InitializeComponent();
            this.EditID = ID;
        }
        readonly private int EditID = -1;
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {

                if (!String.IsNullOrWhiteSpace(CountBox.Text) &&
                    DeliveryBox.SelectedDate != null &&
                    ProductCombo.SelectedIndex != -1)
                {

                    Batch_of_products batch = new Batch_of_products
                    {
                        ID = db.Batch_Of_Products.Count() + 1,
                        Count = Convert.ToInt32(CountBox.Text),
                        Delivery_Date = DeliveryBox.SelectedDate.Value,
                        Product = GetProduct(db.Products.ToList())
                    };
                    if (EditID == -1)
                    {
                        db.Batch_Of_Products.Add(batch);
                    }
                    else
                    {
                        var result = db.Batch_Of_Products.Find(EditID);
                        result.Count = Convert.ToInt32(CountBox.Text);
                        result.Delivery_Date = DeliveryBox.SelectedDate.Value;
                        result.Product = GetProduct(db.Products.ToList());
                    }
                }
                else MessageBox.Show("Заполнены не все поля");
                db.SaveChanges();
                this.Close();
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private Product GetProduct(List<Product> products)
        {
            foreach (var item in products)
            {
                if (item.Name == this.ProductCombo.SelectedItem.ToString())
                    return item;
            }
            return products[0];
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {
                Batch_of_products EditProduct = db.Batch_Of_Products.Find(this.EditID);
                var types = db.Products.ToList();
                List<string> productsList = new List<string>();
                foreach (var item in types)
                    productsList.Add(item.Name);
                ProductCombo.ItemsSource = productsList;
                if (EditID != -1)
                {
                    AddButton.Content = "Сохранить";
                    CountBox.Text = EditProduct.Count.ToString();
                    DeliveryBox.SelectedDate = EditProduct.Delivery_Date;
                    ProductCombo.SelectedItem = EditProduct.Product.Name;
                }
            }
        }
    }
}