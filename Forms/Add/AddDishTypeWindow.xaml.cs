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
    /// Логика взаимодействия для DishTypeWindow.xaml
    /// </summary>
    public partial class DishTypeWindow : Window
    {
        public DishTypeWindow()
        {
            InitializeComponent();
        }
        public DishTypeWindow(int ID)
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
                    Dish_type dish_Type = new Dish_type
                    {
                        ID = db.Dish_Types.Count() + 1,
                        Name = NameBox.Text,
                        Description = DescrBox.Text
                    };
                    if (EditID == -1)
                    {
                        db.Dish_Types.Add(dish_Type);
                    }
                    else
                    {
                        var result = db.Dish_Types.Find(EditID);
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
                Dish_type EditType = db.Dish_Types.Find(this.EditID);
                if (EditID != -1)
                {
                    AddButton.Content = "Сохранить";
                    NameBox.Text = EditType.Name;
                    DescrBox.Text = EditType.Description;
                }
            }
        }
    }
}
