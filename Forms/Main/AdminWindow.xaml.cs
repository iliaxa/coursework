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
using WpfApplicationEntity.Forms.Add;
using WpfApplicationEntity.API;
namespace WpfApplicationEntity.Forms.Main
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : System.Windows.Window
    {
        public AdminWindow()
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
                    productsGrid.ItemsSource = GridsInfo.GetNewProducts(objectMyDBContext).ToList();
                    orderGrid.ItemsSource = GridsInfo.GetNewOrders(objectMyDBContext).ToList();
                    transportsGrid.ItemsSource = objectMyDBContext.Transports.ToList();
                    workersGrid.ItemsSource = GridsInfo.GetNewWorkers(objectMyDBContext).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void addWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            AddWorkerWindow form = new AddWorkerWindow();
            form.ShowDialog();
            this.ShowAll();
        }

        private void editWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            if (workersGrid.SelectedItem != null)
            {
                var edit = (GridsInfo.newWorker)workersGrid.SelectedItem;
                AddWorkerWindow form = new AddWorkerWindow(DBContext.Workers.Find(edit.ID).ID);
                form.ShowDialog();
            }
            else MessageBox.Show("Не выбрано поле для редактирования", "Ошибка");
            this.ShowAll();
        }

        private void deleteWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            if (workersGrid.SelectedItem != null)
            {
                var deleted = (GridsInfo.newWorker)workersGrid.SelectedItem;
                var list = (from item in DBContext.Workers.ToList()
                            where item.ID.CompareTo(deleted.ID) == 0
                            select item).ToList();
                DBContext.Workers.Remove(list[0]);
                DBContext.SaveChanges();
                this.ShowAll();
            }
            else MessageBox.Show("Не выбрано поле для удаления", "Ошибка");
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

        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow form = new AddProductWindow();
            form.ShowDialog();
            this.ShowAll();
        }

        private void editProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (productsGrid.SelectedItem != null)
            {
                var edit = (GridsInfo.newProduct)productsGrid.SelectedItem;
                AddProductWindow form = new AddProductWindow(DBContext.Products.Find(edit.ID).ID);
                form.ShowDialog();
            }
            else MessageBox.Show("Не выбрано поле для редактирования", "Ошибка");
            this.ShowAll();
        }

        private void deleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (productsGrid.SelectedItem != null)
            {
                var deleted = (GridsInfo.newProduct)productsGrid.SelectedItem;
                var list = (from item in DBContext.Products.ToList()
                            where item.ID.CompareTo(deleted.ID) == 0
                            select item).ToList();
                DBContext.Products.Remove(list[0]);
                DBContext.SaveChanges();
                this.ShowAll();
            }
            else MessageBox.Show("Не выбрано поле для удаления", "Ошибка");
        }

        private void addTransportButton_Click(object sender, RoutedEventArgs e)
        {
            AddTransportWindow form = new AddTransportWindow();
            form.ShowDialog();
            this.ShowAll();
        }

        private void editTransportButton_Click(object sender, RoutedEventArgs e)
        {
            if (transportsGrid.SelectedItem != null)
            {
                var edit = (Transport)transportsGrid.SelectedItem;
                AddTransportWindow form = new AddTransportWindow(DBContext.Orders.Find(edit.ID).ID);
                form.ShowDialog();
            }
            else MessageBox.Show("Не выбрано поле для редактирования", "Ошибка");
            this.ShowAll();
        }

        private void deleteTransportButton_Click(object sender, RoutedEventArgs e)
        {
            if (transportsGrid.SelectedItem != null)
            {
                var deleted = (Transport)transportsGrid.SelectedItem;
                var list = (from item in DBContext.Transports.ToList()
                            where item.ID.CompareTo(deleted.ID) == 0
                            select item).ToList();
                DBContext.Transports.Remove(list[0]);
                DBContext.SaveChanges();
                this.ShowAll();
            }
            else MessageBox.Show("Не выбрано поле для удаления", "Ошибка");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ShowAll();
        }
        private void createReportWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.ReportWorker();
        }
        private void createReportOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.ReportOrder();
        }

        private void createReportProductButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.ReportProduct();
        }

        private void createReportTransportButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.ReportTransport();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            DataBaseToFile.ExportDataBase();
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Импорт базы данных удалит существующую базу данных, вы точно хотите импортировать базу данных?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                DBContext.Database.Delete();
                DBContext.Database.Create();
                DataBaseToFile.ImportDataBase();
            }
        }
    }
}