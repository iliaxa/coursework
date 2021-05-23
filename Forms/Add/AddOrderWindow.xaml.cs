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
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        public AddOrderWindow()
        {
            InitializeComponent();
        }
        public AddOrderWindow(int ID)
        {
            InitializeComponent();
            this.EditID = ID;
        }
        private readonly int EditID = -1;
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {
                if (!String.IsNullOrWhiteSpace(PlaceBox.Text) &&
                    DateBox.SelectedDate.HasValue &&
                    TimeBox.Value.HasValue &&
                    ClientCombo.SelectedIndex != -1 &&
                    WorkerCombo.SelectedIndex != -1 &&
                    TransportCombo.SelectedIndex != -1 &&
                    TypeCombo.SelectedIndex != -1
                    )
                {
                    Order order = new Order
                    {
                        ID = db.Orders.Count() + 1,
                        Place = PlaceBox.Text,
                        Date = DateBox.SelectedDate.Value,
                        Time = TimeBox.Value.Value,
                        Client = GetClient(db.Clients.ToList()),
                        Worker = GetWorker(db.Workers.ToList()),
                        Order_Type = GetType(db.Order_Types.ToList()),
                        Transport = GetTransport(db.Transports.ToList())
                    };
                    if (EditID == -1)
                    {
                        db.Orders.Add(order);
                    }
                    else
                    {
                        var result = db.Orders.Find(EditID);
                        result.Place = PlaceBox.Text;
                        result.Date = DateBox.SelectedDate.Value;
                        result.Time = TimeBox.Value.Value;
                        result.Client = GetClient(db.Clients.ToList());
                        result.Worker = GetWorker(db.Workers.ToList());
                        result.Order_Type = GetType(db.Order_Types.ToList());
                        result.Transport = GetTransport(db.Transports.ToList());
                    }
                }
                else MessageBox.Show("Заполнены не все поля");
                    db.SaveChanges();
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private Client GetClient(List<Client> clients)
        {
            string[] splittedClient = this.ClientCombo.SelectedItem.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in clients)
            {
                if (item.Surname == splittedClient[0] &&
                    item.Name == splittedClient[1] &&
                    item.Lastname == splittedClient[2])
                    return item;
            }
            return clients[0];
        }
        private Order_Type GetType(List<Order_Type> types)
        {
            foreach (var item in types)
            {
                if (item.Name == TypeCombo.SelectedItem.ToString())
                    return item;
            }
            return types[0];
        }
        private Worker GetWorker(List<Worker> workers)
        {
            string[] splittedClient = this.WorkerCombo.SelectedItem.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in workers)
            {
                if (item.Surname == splittedClient[0]
                    && item.Name == splittedClient[1]
                    && item.Lastname == splittedClient[2])
                    return item;
            }
            return workers[0];
        }
        private Transport GetTransport(List<Transport> transports)
        {
            foreach (var item in transports)
            {
                if (item.Name == TransportCombo.SelectedItem.ToString())
                    return item;
            }
            return transports[0];
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (MyDBContext db = new MyDBContext())
            {
                Order EditOrder = new Order();
                var clients = db.Clients.ToList();
                var types = db.Order_Types.ToList();
                var workers = db.Workers.ToList();
                var transports = db.Transports.ToList();
                List<string> clientList = new List<string>();
                List<string> typeList = new List<string>();
                List<string> workerList = new List<string>();
                List<string> transportList = new List<string>();
                foreach (var item in clients)
                    clientList.Add($"{item.Surname} {item.Name} {item.Lastname}");
                foreach (var item in types)
                    typeList.Add(item.Name);
                foreach (var item in workers)
                    workerList.Add($"{item.Surname} {item.Name} {item.Lastname}");
                foreach (var item in transports)
                    transportList.Add(item.Name);
                ClientCombo.ItemsSource = clientList;
                TypeCombo.ItemsSource = typeList;
                WorkerCombo.ItemsSource = workerList;
                TransportCombo.ItemsSource = transportList;
                if (EditID != -1)
                {
                    AddButton.Content = "Сохранить";
                    PlaceBox.Text= EditOrder.Place;
                    DateBox.SelectedDate = EditOrder.Date;
                    TimeBox.Value= EditOrder.Time;
                    ClientCombo.Text = $"{EditOrder.Client.Surname} {EditOrder.Client.Name} {EditOrder.Client.Lastname}";
                    WorkerCombo.Text = $"{EditOrder.Worker.Surname} {EditOrder.Worker.Name} {EditOrder.Worker.Lastname}";
                    TypeCombo.Text = EditOrder.Order_Type.Name;
                     TransportCombo.Text = EditOrder.Transport.Name;
                }
            }
        }
    }
}
