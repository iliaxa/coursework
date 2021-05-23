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
using System.Data.Entity.Migrations;
using WpfApplicationEntity.API;
using System.Data.Entity;

namespace WpfApplicationEntity.Forms.Add
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public AddProductWindow()
        {
            InitializeComponent();
        }
        public AddProductWindow(int ID)
        {
            InitializeComponent();
            this.EditID = ID;
        }
        private int EditID = -1;
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {

                if (!String.IsNullOrWhiteSpace(NameBox.Text) &&
                    !String.IsNullOrWhiteSpace(StorageBox.Text) &&
                    TypeCombo.SelectedIndex != -1)
                {

                    Product product = new Product
                    {
                        ID = db.Products.Count() + 1,
                        Name = NameBox.Text,
                        Storage_life = StorageBox.Text,
                        Product_Type = GetProductType(db.Product_Types.ToList())
                    };
                    if (EditID == -1)
                    {
                        db.Products.Add(product);
                    }
                    else
                    {
                        var result = db.Products.Find(EditID);
                        result.Name = NameBox.Text;
                        result.Storage_life = StorageBox.Text;
                        result.Product_Type = GetProductType(db.Product_Types.ToList());
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
        private Product_Type GetProductType(List<Product_Type> types)
        {
            foreach (var item in types)
            {
                if (item.Name == this.TypeCombo.SelectedItem.ToString())
                    return item;
            }
            return types[0];
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {
                Product EditProduct = db.Products.Find(this.EditID);
                var types = db.Product_Types.ToList();
                List<string> typeList = new List<string>();
                foreach (var item in types)
                    typeList.Add(item.Name);
                TypeCombo.ItemsSource = typeList;
                if (EditID != -1)
                {
                    AddButton.Content = "Сохранить";
                    NameBox.Text = EditProduct.Name;
                    StorageBox.Text = EditProduct.Storage_life;
                    TypeCombo.SelectedItem = EditProduct.Product_Type.Name;
                }
            }
        }
    }
}
