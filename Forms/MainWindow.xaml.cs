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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplicationEntity.API;
using WpfApplicationEntity.Forms;
using WpfApplicationEntity.Forms.Add;

namespace WpfApplicationEntity
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        MyDBContext DBContext = new MyDBContext();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ShowAll();
        }
        private void ShowAll()
        {
            try
            {
                using (MyDBContext objectMyDBContext = new MyDBContext())
                {
                    productsGrid.ItemsSource = objectMyDBContext.Products.ToList();
                    batch_of_productsGrid.ItemsSource = objectMyDBContext.Batch_Of_Products.ToList();
                    clientGrid.ItemsSource = objectMyDBContext.Clients.ToList();
                    dishesGrid.ItemsSource = objectMyDBContext.Dishes.ToList();
                    dishTypesGrid.ItemsSource = objectMyDBContext.Dishes.ToList();
                    orderGrid.ItemsSource = objectMyDBContext.Orders.ToList();
                    orderListGrid.ItemsSource = objectMyDBContext.Order_Lists.ToList();
                    orderTypeGrid.ItemsSource = objectMyDBContext.Order_Types.ToList();
                    productsTypeGrid.ItemsSource = objectMyDBContext.Products.ToList();
                    transportsGrid.ItemsSource = objectMyDBContext.Transports.ToList();
                    workersGrid.ItemsSource = objectMyDBContext.Workers.ToList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void addClientButton_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow form = new AddClientWindow();
            form.ShowDialog();
            this.ShowAll();
        }

        private void addOrderButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrderWindow form = new AddOrderWindow();
            form.ShowDialog();
            this.ShowAll();
        }

        private void addBatchButton_Click(object sender, RoutedEventArgs e)
        {
            AddBatchWindow form = new AddBatchWindow();
            form.ShowDialog();
            this.ShowAll();

        }
        private void addProductTypeButton_Click(object sender, RoutedEventArgs e)
        {
            AddProductTypeWindow form = new AddProductTypeWindow();
            form.ShowDialog();
            this.ShowAll();

        }

        private void addOrderTypeButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrderTypeWindow form = new AddOrderTypeWindow();
            form.ShowDialog();
            this.ShowAll();
        }

        private void addOrderListButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrderListWindow form = new AddOrderListWindow();
            form.ShowDialog();
            this.ShowAll();

        }

        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow form = new AddProductWindow();
            form.ShowDialog();
            this.ShowAll();

        }

        private void addDishButton_Click(object sender, RoutedEventArgs e)
        {
            AddDishWindow form = new AddDishWindow();
            form.ShowDialog();
            this.ShowAll();

        }

        private void addTransportButton_Click(object sender, RoutedEventArgs e)
        {
            AddTransportWindow form = new AddTransportWindow();
            form.ShowDialog();
            this.ShowAll();

        }

        private void addDishTypeButton_Click(object sender, RoutedEventArgs e)
        {
            DishTypeWindow form = new DishTypeWindow();
            form.ShowDialog();
            this.ShowAll();

        }

        private void addWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            AddWorkerWindow form = new AddWorkerWindow();
            form.ShowDialog();
            this.ShowAll();
        }

        private void deleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (clientGrid.SelectedItem != null)
            {
                var deletedClient = (Client)clientGrid.SelectedItem;
                var clients = (from item in DBContext.Clients.ToList()
                                  where item.ID.CompareTo(deletedClient.ID) == 0
                                  select item).ToList();
                DBContext.Clients.Remove(clients[0]);
                DBContext.SaveChanges();
                this.ShowAll();
            }
            else MessageBox.Show("Не выбрано поле для удаления","Ошибка");
        }

        private void editClientButton_Click(object sender, RoutedEventArgs e)
        {
            this.ShowAll();
        }

        private void deleteWorkerButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
