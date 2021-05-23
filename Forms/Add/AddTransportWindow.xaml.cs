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
        readonly MyDBContext DBContext = new MyDBContext();
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(NameBox.Text) &&
                !String.IsNullOrWhiteSpace(NumberBox.Text))
            {
                Transport transport = new Transport
                {
                    ID = DBContext.Transports.Count() + 1,
                    Name = NameBox.Text,
                    Number = NumberBox.Text
                };
                DBContext.Transports.Add(transport);
                DBContext.SaveChanges();
            }
            else MessageBox.Show("Заполнены не все поля");
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
