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
using WpfApplicationEntity.Forms.Main;
namespace WpfApplicationEntity
{
    /// <summary>
    /// Логика взаимодействия для AutorizationWindow.xaml
    /// </summary>
    public partial class AutorizationWindow : Window
    {
        public AutorizationWindow()
        {
            InitializeComponent();
        }
        private void AutorizationButton_Click(object sender, RoutedEventArgs e)
        {
            TryAutorization();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {
                if (db.Database.Exists() == false)
                {
                    db.Database.Create();
                    List<AccessLevel> ListLevels = new List<AccessLevel>();
                    AccessLevel Level = new AccessLevel
                    {
                        ID = 0,
                        Level = "Администратор"
                    };
                    db.Levels.Add(Level);
                    ListLevels.Add(Level);
                    Level = new AccessLevel
                    {
                        ID = 1,
                        Level = "Шеф-повар"
                    };
                    db.Levels.Add(Level);
                    ListLevels.Add(Level);
                    Level = new AccessLevel
                    {
                        ID = 2,
                        Level = "Приемщик заказов"
                    };
                    db.Levels.Add(Level);
                    ListLevels.Add(Level);
                    Worker admin = new Worker()
                    {
                        ID = 0,
                        Name = "admin",
                        Surname = "admin",
                        Lastname = "admin",
                        Birthday = DateTime.Now,
                        Phone_Number = "0",
                        Login = "admin",
                        Password = "passs",
                        Gender = true,
                        Driver_License = "present",
                        Adress = "0",
                        Access_Level = ListLevels[0]
                    };
                    db.Workers.Add(admin);
                    db.SaveChanges();
                    MessageBox.Show("Создана новая база двнных, логин и пароль нового пользователя: admin passs","Ошибка",MessageBoxButton.OK,MessageBoxImage.Information);
                }
            }
        }

        private void TryAutorization()
        {
            using (MyDBContext db = new MyDBContext())
            {
                if (!String.IsNullOrWhiteSpace(LoginBox.Text) && !String.IsNullOrWhiteSpace(PasswordBox.Text))
                {
                    var users = (from user in db.Workers.ToList()
                                 where user.Login.CompareTo(LoginBox.Text) == 0 && user.Password.CompareTo(PasswordBox.Text) == 0
                                 select user).ToList();
                    if (users.Count() > 0 && db.Workers.Count() > 0)
                    {
                        if (users[0].Access_Level.Level == "Администратор")
                        {
                            AdminWindow form = new AdminWindow();
                            form.Show();
                            this.Close();
                        }
                        if (users[0].Access_Level.Level == "Шеф-повар")
                        {
                            ChefWindow form = new ChefWindow();
                            form.Show();
                            this.Close();
                        }
                        if (users[0].Access_Level.Level == "Приемщик заказов")
                        {
                            OrderTakerWindow form = new OrderTakerWindow();
                            form.Show();
                            this.Close();
                        }
                    }
                    else MessageBox.Show("Пользователь с такими данным не найден", "Ошибка");
                }
                else MessageBox.Show("Не заполнены все поля", "Ошибка");
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                TryAutorization();
        }
    }
}
