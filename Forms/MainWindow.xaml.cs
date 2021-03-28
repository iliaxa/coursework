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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (MyDBContext objectMyDBContext = new MyDBContext())
            {
                if (objectMyDBContext.Database.Exists() == false)
                {
                    objectMyDBContext.Database.Create();
                    //User objectUser = new User();
                    //objectUser.Name = "user name";
                    //objectUser.Login = "user";
                    //objectUser.Password = "1111";
                    //objectMyDBContext.Users.Add(objectUser);
                    objectMyDBContext.SaveChanges();
                }
            }
            this.ShowAll();
        }
        #region Группа
        private void addGroupButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.GroupWindow g = new Forms.GroupWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        #endregion
        private void ShowAll()
        {
            try
            {
                using (MyDBContext objectMyDBContext = 
                    new MyDBContext())
                {
                  //  gropiesGrid.ItemsSource = DatabaseRequest.GetGroups(objectMyDBContext);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void clientGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
