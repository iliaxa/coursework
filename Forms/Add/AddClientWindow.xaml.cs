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
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
        }
        public AddClientWindow(int ID)
        {
            InitializeComponent();
            this.EditID = ID;
        }
        private readonly int EditID = -1;
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {
                if (!String.IsNullOrWhiteSpace(SurnameBox.Text) &&
                    !String.IsNullOrWhiteSpace(NameBox.Text) &&
                    !String.IsNullOrWhiteSpace(LastNameBox.Text) &&
                    !String.IsNullOrWhiteSpace(PhoneBox.Text))
                {
                    Client client = new Client
                    {
                        ID = db.Clients.Count() + 1,
                        Surname = SurnameBox.Text,
                        Name = NameBox.Text,
                        Lastname = LastNameBox.Text,
                        Phone_Number = PhoneBox.Text
                    };
                    if (EditID == -1)
                    {
                    db.Clients.Add(client);
                    }
                    else
                    {
                        var result = db.Clients.Find(EditID);
                        result.Surname = SurnameBox.Text;
                        result.Name = NameBox.Text;
                        result.Lastname = LastNameBox.Text;
                        result.Phone_Number = PhoneBox.Text;
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
                Client EditClient = db.Clients.Find(EditID);
                if (EditID != -1)
                {
                    SurnameBox.Text = EditClient.Surname;
                    NameBox.Text = EditClient.Name;
                    LastNameBox.Text = EditClient.Lastname;
                    PhoneBox.Text = EditClient.Phone_Number;
                }
            }
        }
    }
}
