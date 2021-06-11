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
    /// Логика взаимодействия для AddOrderListWindow.xaml
    /// </summary>
    public partial class AddOrderListWindow : Window
    {
        public AddOrderListWindow()
        {
            InitializeComponent();
        }
        public AddOrderListWindow(int ID)
        {
            InitializeComponent();
            this.EditID = ID;
        }
        private readonly int EditID = -1;
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {
                if (!String.IsNullOrWhiteSpace(CountBox.Text))
                {
                    Order_list order_List = new Order_list
                    {
                        ID = db.Order_Lists.Count() + 1,
                        Count = CountBox.Text,
                        Order = GetOrder(db.Orders.ToList()),
                        Dish = GetDish(db.Dishes.ToList())
                    };
                    if (EditID == -1)
                    {
                        db.Order_Lists.Add(order_List);
                    }
                    else
                    {
                        var result = db.Order_Lists.Find(EditID);
                        result.Count = CountBox.Text;
                        result.Dish = GetDish(db.Dishes.ToList());
                        result.Order = GetOrder(db.Orders.ToList());
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
        private Order GetOrder(List<Order> orders)
        {
            string[] splittedOrder = this.OrderCombo.SelectedItem.ToString().Split(new char[] {' '},StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in orders)
            {
                if (item.Date.ToShortDateString() == splittedOrder[0]
                    && item.Place == splittedOrder[1]
                    && item.Time.ToShortTimeString() == splittedOrder[2])
                        return item;
            }
            return orders[0];
        }
        private Dish GetDish(List<Dish> dishes)
        {
            foreach (var item in dishes)
                if (item.Name == this.DishCombo.SelectedItem.ToString())
                    return item;
            return dishes[0];
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {
                Order_list EditOrder_List = db.Order_Lists.Find(EditID);
                var orders = db.Orders.ToList();
                var dishes = db.Dishes.ToList();
                List<string> orderList = new List<string>();
                List<string> dishList = new List<string>();
                foreach (var item in orders)
                    orderList.Add($"{item.Date.ToShortDateString()} {item.Place} {item.Time.ToShortTimeString()}");
                foreach (var item in dishes)
                    dishList.Add(item.Name);
                OrderCombo.ItemsSource = orderList;
                DishCombo.ItemsSource = dishList;
                if (EditID != -1)
                {
                    AddButton.Content = "Сохранить";
                    CountBox.Text = EditOrder_List.Count;
                    DishCombo.SelectedItem = EditOrder_List.Dish.Name;
                    OrderCombo.SelectedItem = $"{EditOrder_List.Order.Date} {EditOrder_List.Order.Place} {EditOrder_List.Order.Time}";
                }
            }
        }
    }
}
