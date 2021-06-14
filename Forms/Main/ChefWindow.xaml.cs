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
    /// Логика взаимодействия для ChefWindow.xaml
    /// </summary>
    public partial class ChefWindow : Window
    {
        public ChefWindow()
        {
            InitializeComponent();
        }
        private MyDBContext DBContext = new MyDBContext();
        private void ShowAll()
        {
            try
            {
                using (MyDBContext objectMyDBContext = new MyDBContext())
                {
                    productsGrid.ItemsSource = GridsInfo.GetNewProducts(objectMyDBContext).ToList();
                    batch_of_productsGrid.ItemsSource = GridsInfo.GetNewBatches(objectMyDBContext).ToList();
                    dishesGrid.ItemsSource = GridsInfo.GetNewDishes(objectMyDBContext).ToList();
                    dishTypesGrid.ItemsSource = objectMyDBContext.Dish_Types.ToList();
                    productsTypeGrid.ItemsSource = objectMyDBContext.Product_Types.ToList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            try
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
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Нельзя удалить связанные записи", "Ошибка");
            }
        }
        private void addBatchButton_Click(object sender, RoutedEventArgs e)
        {

            AddBatchWindow form = new AddBatchWindow();
            form.ShowDialog();
            this.ShowAll();
        }
        private void editBatch_of_productsButton_Click(object sender, RoutedEventArgs e)
        {
            if (batch_of_productsGrid.SelectedItem != null)
            {
                var edit = (GridsInfo.newBatch)batch_of_productsGrid.SelectedItem;
                AddBatchWindow form = new AddBatchWindow(DBContext.Batch_Of_Products.Find(edit.ID).ID);
                form.ShowDialog();
            }
            else MessageBox.Show("Не выбрано поле для редактирования", "Ошибка");
            this.ShowAll();
        }
        private void deleteBatch_of_productsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (batch_of_productsGrid.SelectedItem != null)
                {
                    var deleted = (GridsInfo.newBatch)batch_of_productsGrid.SelectedItem;
                    var list = (from item in DBContext.Batch_Of_Products.ToList()
                                where item.ID.CompareTo(deleted.ID) == 0
                                select item).ToList();
                    DBContext.Batch_Of_Products.Remove(list[0]);
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
        private void addProductTypeButton_Click(object sender, RoutedEventArgs e)
        {
            AddProductTypeWindow form = new AddProductTypeWindow();
            form.ShowDialog();
            this.ShowAll();
        }
        private void editProductTypeButton_Click(object sender, RoutedEventArgs e)
        {
            if (productsTypeGrid.SelectedItem != null)
            {
                var edit = (Product_Type)productsTypeGrid.SelectedItem;
                AddProductTypeWindow form = new AddProductTypeWindow(DBContext.Product_Types.Find(edit.ID).ID);
                form.ShowDialog();
            }
            else MessageBox.Show("Не выбрано поле для редактирования", "Ошибка");
            this.ShowAll();
        }
        private void deleteProductTypeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (productsTypeGrid.SelectedItem != null)
                {
                    var deleted = (Product_Type)productsTypeGrid.SelectedItem;
                    var list = (from item in DBContext.Product_Types.ToList()
                                where item.ID.CompareTo(deleted.ID) == 0
                                select item).ToList();
                    DBContext.Product_Types.Remove(list[0]);
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

        private void addDishButton_Click(object sender, RoutedEventArgs e)
        {
            AddDishWindow form = new AddDishWindow();
            form.ShowDialog();
            this.ShowAll();
        }

        private void editDishButton_Click(object sender, RoutedEventArgs e)
        {
            if (dishesGrid.SelectedItem != null)
            {
                var edit = (GridsInfo.newDish)dishesGrid.SelectedItem;
                AddDishWindow form = new AddDishWindow(DBContext.Dishes.Find(edit.ID).ID);
                form.ShowDialog();
            }
            else MessageBox.Show("Не выбрано поле для редактирования", "Ошибка");
            this.ShowAll();
        }
        private void deleteDishButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dishesGrid.SelectedItem != null)
                {
                    var deleted = (GridsInfo.newDish)dishesGrid.SelectedItem;
                    var list = (from item in DBContext.Dishes.ToList()
                                where item.ID.CompareTo(deleted.ID) == 0
                                select item).ToList();
                    DBContext.Dishes.Remove(list[0]);
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
        private void addDishTypeButton_Click(object sender, RoutedEventArgs e)
        {
            DishTypeWindow form = new DishTypeWindow();
            form.ShowDialog();
            this.ShowAll();
        }
        private void editDishTypeButton_Click(object sender, RoutedEventArgs e)
        {

            if (dishTypesGrid.SelectedItem != null)
            {
                var edit = (Dish_type)dishTypesGrid.SelectedItem;
                DishTypeWindow form = new DishTypeWindow(DBContext.Dish_Types.Find(edit.ID).ID);
                form.ShowDialog();
            }
            else MessageBox.Show("Не выбрано поле для редактирования", "Ошибка");
            this.ShowAll();
        }
        private void deleteDishTypeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dishTypesGrid.SelectedItem != null)
                {
                    var deleted = (Dish_type)dishTypesGrid.SelectedItem;
                    var list = (from item in DBContext.Dish_Types.ToList()
                                where item.ID.CompareTo(deleted.ID) == 0
                                select item).ToList();
                    DBContext.Dish_Types.Remove(list[0]);
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

        private void createReportProductButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.ReportProduct();
        }

        private void createReportBatchButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.ReportBatch();
        }

        private void createReportTypeProductButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.ReportProductType();
        }

        private void createReportDishButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.ReportDish();
        }

        private void createReportTypeDishButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.ReportDishType();
        }
    }
}
