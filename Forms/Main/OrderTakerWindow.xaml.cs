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
using WpfApplicationEntity.Forms;
using WpfApplicationEntity.Forms.Add;
using WpfApplicationEntity.API;
namespace WpfApplicationEntity.Forms.Main
{
    /// <summary>
    /// Логика взаимодействия для OrderTakerWindow.xaml
    /// </summary>
    public partial class OrderTakerWindow : Window
    {
        public OrderTakerWindow()
        {
            InitializeComponent();
        }
        readonly MyDBContext DBContext = new MyDBContext();
        private void ShowAll()
        {
            try
            {
                using (MyDBContext objectMyDBContext = new MyDBContext())
                {
                    clientGrid.ItemsSource = objectMyDBContext.Clients.ToList();
                    orderGrid.ItemsSource = GridsInfo.GetNewOrders(objectMyDBContext).ToList();
                    orderListGrid.ItemsSource = GridsInfo.GetNewOrderLists(objectMyDBContext).ToList();
                    orderTypeGrid.ItemsSource = objectMyDBContext.Order_Types.ToList();

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

        private void editClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (clientGrid.SelectedItem != null)
            {
                var edit = (Client)clientGrid.SelectedItem;
                AddClientWindow form = new AddClientWindow(DBContext.Clients.Find(edit.ID).ID);
                form.ShowDialog();
            }
            else MessageBox.Show("Не выбрано поле для редактирования", "Ошибка");
            this.ShowAll();
        }

        private void deleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (clientGrid.SelectedItem != null)
                {
                    var deleted = (Client)clientGrid.SelectedItem;
                    var list = (from item in DBContext.Clients.ToList()
                                where item.ID.CompareTo(deleted.ID) == 0
                                select item).ToList();
                    DBContext.Clients.Remove(list[0]);
                    DBContext.SaveChanges();
                    this.ShowAll();
                }
                else MessageBox.Show("Не выбрано поле для удаления", "Ошибка");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Нельзя удалить связанные записи", "Ошибка");
            }
        }

        private void addOrderButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrderWindow form = new AddOrderWindow();
            form.ShowDialog();
            this.ShowAll();
        }

        private void editOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (orderGrid.SelectedItem != null)
            {
                var edit = (GridsInfo.newOrder)orderGrid.SelectedItem;
                AddOrderWindow form = new AddOrderWindow(DBContext.Orders.Find(edit.ID).ID);
                form.ShowDialog();
            }
            else MessageBox.Show("Не выбрано поле для редактирования", "Ошибка");
            this.ShowAll();
        }

        private void deleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (orderGrid.SelectedItem != null)
                {
                    var deleted = (GridsInfo.newOrder)orderGrid.SelectedItem;
                    var list = (from item in DBContext.Orders.ToList()
                                where item.ID.CompareTo(deleted.ID) == 0
                                select item).ToList();
                    DBContext.Orders.Remove(list[0]);
                    DBContext.SaveChanges();
                    this.ShowAll();
                }
                else MessageBox.Show("Не выбрано поле для удаления", "Ошибка");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Нельзя удалить связанные записи", "Ошибка");
            }
        }

        private void addOrderTypeButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrderTypeWindow form = new AddOrderTypeWindow();
            form.ShowDialog();
            this.ShowAll();
        }

        private void editOrderTypetButton_Click(object sender, RoutedEventArgs e)
        {
            if (orderTypeGrid.SelectedItem != null)
            {
                var edit = (Order_Type)orderTypeGrid.SelectedItem;
                AddOrderTypeWindow form = new AddOrderTypeWindow(DBContext.Order_Types.Find(edit.ID).ID);
                form.ShowDialog();
            }
            else MessageBox.Show("Не выбрано поле для редактирования", "Ошибка");
            this.ShowAll();
        }

        private void deleteOrderTypeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (orderTypeGrid.SelectedItem != null)
                {
                    var deleted = (Order_Type)orderTypeGrid.SelectedItem;
                    var list = (from item in DBContext.Order_Types.ToList()
                                where item.ID.CompareTo(deleted.ID) == 0
                                select item).ToList();
                    DBContext.Order_Types.Remove(list[0]);
                    DBContext.SaveChanges();
                    this.ShowAll();
                }
                else MessageBox.Show("Не выбрано поле для удаления", "Ошибка");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Нельзя удалить связанные записи", "Ошибка");
            }
        }

        private void addOrderListButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrderListWindow form = new AddOrderListWindow();
            form.ShowDialog();
            this.ShowAll();
        }

        private void editOrderListButton_Click(object sender, RoutedEventArgs e)
        {
            if (orderListGrid.SelectedItem != null)
            {
                var edit = (GridsInfo.newOrderList)orderListGrid.SelectedItem;
                AddOrderListWindow form = new AddOrderListWindow(DBContext.Order_Lists.Find(edit.ID).ID);
                form.ShowDialog();
            }
            else MessageBox.Show("Не выбрано поле для редактирования", "Ошибка");
            this.ShowAll();
        }

        private void deleteOrderListButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (orderListGrid.SelectedItem != null)
                {
                    var deleted = (GridsInfo.newOrderList)orderListGrid.SelectedItem;
                    var list = (from item in DBContext.Order_Lists.ToList()
                                where item.ID.CompareTo(deleted.ID) == 0
                                select item).ToList();
                    DBContext.Order_Lists.Remove(list[0]);
                    DBContext.SaveChanges();
                    this.ShowAll();
                }
                else MessageBox.Show("Не выбрано поле для удаления", "Ошибка");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Нельзя удалить связанные записи", "Ошибка");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ShowAll();
        }

        private void createReportOrderListButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.ReportOrderList();
        }

        private void createReportOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.ReportOrder();
        }

        private void createReportOrderTypeButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.ReportOrderType();
        }

        private void createReportClientButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.ReportClient();
        }
    }
}
