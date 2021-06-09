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
namespace WpfApplicationEntity.Forms.Add
{
    /// <summary>
    /// Логика взаимодействия для AddProductTypeWindow.xaml
    /// </summary>
    public partial class AddProductTypeWindow : Window
    {
        public AddProductTypeWindow()
        {
            InitializeComponent();
        }
        public AddProductTypeWindow(int ID)
        {
            InitializeComponent();
            this.EditID = ID;
        }
        private readonly int EditID = -1;
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {
                if (!String.IsNullOrWhiteSpace(NameBox.Text) &&
                    !String.IsNullOrWhiteSpace(DescrBox.Text))
                {
                    Product_Type product_Type = new Product_Type
                    {
                        ID = db.Dish_Types.Count() + 1,
                        Name = NameBox.Text,
                        Description = DescrBox.Text
                    };
                    if (EditID == -1)
                    {
                        db.Product_Types.Add(product_Type);
                    }
                    else
                    {
                        var result = db.Product_Types.Find(EditID);
                        result.Name = NameBox.Text;
                        result.Description = DescrBox.Text;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {
                Product_Type EditProduct_Type = db.Product_Types.Find(EditID);
                if (EditID != -1)
                {
                    AddButton.Content = "Сохранить";
                    NameBox.Text = EditProduct_Type.Name;
                    DescrBox.Text = EditProduct_Type.Description;
                }
            }
        }
    }
}
