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
    /// Логика взаимодействия для AddOrderTypeWindow.xaml
    /// </summary>
    public partial class AddOrderTypeWindow : Window
    {
        public AddOrderTypeWindow()
        {
            InitializeComponent();
        }
        public AddOrderTypeWindow(int ID)
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
                    !String.IsNullOrWhiteSpace(DescrBox.Text) &&
                    !String.IsNullOrWhiteSpace(StatusBox.Text))
                {
                    Order_Type order_Type = new Order_Type
                    {
                        ID = db.Order_Types.Count() + 1,
                        Name = NameBox.Text,
                        Description = DescrBox.Text,
                        Status = StatusBox.Text
                    };
                    if (EditID == -1)
                    {
                        db.Order_Types.Add(order_Type);
                    }
                    else
                    {
                        var result = db.Order_Types.Find(EditID);
                        result.Name = NameBox.Text;
                        result.Status = StatusBox.Text;
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
                Order_Type EditOrder_Type = db.Order_Types.Find(EditID);
                if (EditID != -1)
                {
                    AddButton.Content = "Сохранить";
                    NameBox.Text = EditOrder_Type.Name;
                    StatusBox.Text = EditOrder_Type.Status;
                    DescrBox.Text = EditOrder_Type.Description;
                }
            }
        }
    }
}