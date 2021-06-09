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
    /// Логика взаимодействия для AddDishWindow.xaml
    /// </summary>
    public partial class AddDishWindow : Window
    {
        public AddDishWindow()
        {
            InitializeComponent();
        }
        public AddDishWindow(int ID)
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
                !String.IsNullOrWhiteSpace(PriceBox.Text) &&
                !String.IsNullOrWhiteSpace(WeightBox.Text) &&
                !String.IsNullOrWhiteSpace(CompositionBox.Text))
                {
                    Dish dish = new Dish
                    {
                        ID = db.Dishes.Count() + 1,
                        Name = NameBox.Text,
                        Price = Convert.ToDouble(PriceBox.Text),
                        Weight = Convert.ToDouble(WeightBox.Text),
                        Composition = CompositionBox.Text,
                        Dish_Type = GetDish_Type(db.Dish_Types.ToList())
                    };
                    if (EditID == -1)
                    {
                        db.Dishes.Add(dish);
                    }
                    else
                    {
                        var result = db.Dishes.Find(EditID);
                        result.Name = NameBox.Text;
                        result.Price = Convert.ToDouble(PriceBox.Text);
                        result.Weight = Convert.ToDouble(WeightBox.Text);
                        result.Composition = CompositionBox.Text;
                        result.Dish_Type = GetDish_Type(db.Dish_Types.ToList());

                    }
                }
                else MessageBox.Show("Заполнены не все поля");
                db.SaveChanges();
                this.Close();
            }
        }
        private Dish_type GetDish_Type(List<Dish_type> types)
        {
            foreach (var item in types)
            {
                if (item.Name == this.DishTypeCombo.SelectedItem.ToString())
                    return item;
            }
            return types[0];
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {
                Dish EditDish = db.Dishes.Find(this.EditID);
                var types = db.Dish_Types.ToList();
                List<string> typesList = new List<string>();
                foreach (var item in types)
                    typesList.Add(item.Name);
                DishTypeCombo.ItemsSource = typesList;
                if (EditID != -1)
                {
                    AddButton.Content = "Сохранить";
                    NameBox.Text= EditDish.Name;
                    PriceBox.Text=EditDish.Price.ToString();
                    WeightBox.Text=EditDish.Weight.ToString();
                    CompositionBox.Text=EditDish.Composition;
                    DishTypeCombo.SelectedItem = EditDish.Dish_Type.Name;
                }
            }
        }
    }
}
