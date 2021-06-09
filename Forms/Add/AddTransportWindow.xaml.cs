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
    /// Логика взаимодействия для AddTransportWindow.xaml
    /// </summary>
    public partial class AddTransportWindow : Window
    {
        public AddTransportWindow()
        {
            InitializeComponent();
        }
        public AddTransportWindow(int ID)
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
                    !String.IsNullOrWhiteSpace(NumberBox.Text))
                {
                    Transport transport = new Transport
                    {
                        ID = db.Transports.Count() + 1,
                        Name = NameBox.Text,
                        Number = NumberBox.Text
                    };
                    if (EditID == -1)
                    {
                        db.Transports.Add(transport);
                    }
                    else
                    {
                        var result = db.Transports.Find(EditID);
                        result.Name = NameBox.Text;
                        result.Number = NumberBox.Text;
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
                Transport EditTransport = db.Transports.Find(this.EditID);
                if (EditID != -1)
                {
                    AddButton.Content = "Сохранить";
                    NameBox.Text = EditTransport.Name;
                    NumberBox.Text = EditTransport.Number;
                }
            }
        }
    }
}
