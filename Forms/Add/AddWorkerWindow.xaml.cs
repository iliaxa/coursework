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
    /// Логика взаимодействия для AddWorkerWindow.xaml
    /// </summary>
    public partial class AddWorkerWindow : Window
    {
        public AddWorkerWindow()
        {
            InitializeComponent();
        }
        public AddWorkerWindow(int ID)
        {
            InitializeComponent();
            this.EditID = ID;
        }
        private readonly int EditID = -1;
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {
                if (!String.IsNullOrWhiteSpace(SurNameBox.Text) &&
                    !String.IsNullOrWhiteSpace(NameBox.Text) &&
                    !String.IsNullOrWhiteSpace(LastNameBox.Text) &&
                    !String.IsNullOrWhiteSpace(AdressBox.Text) &&
                    !String.IsNullOrWhiteSpace(DriverBox.Text) &&
                    !String.IsNullOrWhiteSpace(LoginBox.Text) &&
                    !String.IsNullOrWhiteSpace(PassBox.Text) &&
                    (MaleBox.IsChecked.HasValue || FemaleBox.IsChecked.HasValue) &&
                    BirthdayBox.SelectedDate.HasValue && BirthdayBox.SelectedDate.Value < DateTime.Now)
                {
                    bool gender;
                    if (MaleBox.IsChecked.Value == true)
                        gender = true;
                    else gender = false;
                    Worker worker = new Worker
                    {
                        ID = db.Workers.Count() + 1,
                        Surname = SurNameBox.Text,
                        Name = NameBox.Text,
                        Lastname = LastNameBox.Text,
                        Adress = AdressBox.Text,
                        Driver_License = DriverBox.Text,
                        Phone_Number = PhoneBox.Text,
                        Login = LoginBox.Text,
                        Password = PassBox.Text,
                        Birthday = BirthdayBox.SelectedDate.Value,
                        Gender = gender,
                        Access_Level = GetAccessLevel(db.Levels.ToList())
                    };
                    if (EditID == -1)
                    {
                        db.Workers.Add(worker);
                    }
                    else
                    {
                        var result = db.Workers.Find(EditID);
                        result.Surname = SurNameBox.Text;
                        result.Name = NameBox.Text;
                        result.Lastname = LastNameBox.Text;
                        result.Adress = AdressBox.Text;
                        result.Driver_License = DriverBox.Text;
                        result.Phone_Number = PhoneBox.Text;
                        result.Login = LoginBox.Text;
                        result.Password = PassBox.Text;
                        result.Birthday = BirthdayBox.SelectedDate.Value;
                        result.Gender = gender;
                        result.Access_Level = GetAccessLevel(db.Levels.ToList());
                    }
                }
                else MessageBox.Show("Заполнены не все поля");
                    db.SaveChanges();
                    this.Close();
            }
        }
        private AccessLevel GetAccessLevel(List<AccessLevel> levels)
        {
            foreach (var item in levels)
            {
                if (item.Level == this.AccessCombo.SelectedItem.ToString())
                    return item;
            }
            return levels[0];
        }
        private void CancelButon_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {
                Worker EditWorker = db.Workers.Find(this.EditID);
                var types = db.Levels.ToList();
                List<string> typeList = new List<string>();
                foreach (var item in types)
                    typeList.Add(item.Level);
                AccessCombo.ItemsSource = typeList;
                if (EditID != -1)
                {
                    AddButton.Content = "Сохранить";
                    SurNameBox.Text = EditWorker.Surname;
                    NameBox.Text= EditWorker.Name;
                    LastNameBox.Text=EditWorker.Lastname;
                    AdressBox.Text=EditWorker.Adress;
                    DriverBox.Text=EditWorker.Driver_License;
                    PhoneBox.Text=EditWorker.Phone_Number;
                    LoginBox.Text=EditWorker.Login ;
                    PassBox.Text=EditWorker.Password ;
                    BirthdayBox.SelectedDate=EditWorker.Birthday;
                    bool gender= EditWorker.Gender;
                    if (gender)
                        MaleBox.IsChecked = true;
                    else FemaleBox.IsChecked = true;
                    AccessCombo.SelectedItem = EditWorker.Access_Level.Level;
                }
            }
        }
    }
}
