using System.Collections.Generic;
using System.Linq;
namespace WpfApplicationEntity.API
{
    public static class GridsInfo
    {
        public struct newBatch
        {
            public int ID { get; set; }
            public int Count { get; set; }
            public string Delivery_Date { get; set; }
            public string Product { get; set; }
            public newBatch(int Id, int count, string date, string product)
            {
                this.ID = Id;
                this.Count = count;
                this.Delivery_Date = date;
                this.Product = product;
            }
        }
        public struct newWorker
        {
            public int ID { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public string Lastname { get; set; }
            public string Adress { get; set; }
            public string Phone_Number { get; set; }
            public string Driver_License { get; set; }
            public bool Gender { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string Birthday { get; set; }
            public string Access_Level { get; set; }
            public newWorker(int iD, string surname, string name, string lastname, string adress, string phone_Number, string driver_License, bool gender, string login, string password, string birthday, string access_Level)
            {
                ID = iD;
                Surname = surname;
                Name = name;
                Lastname = lastname;
                Adress = adress;
                Phone_Number = phone_Number;
                Driver_License = driver_License;
                Gender = gender;
                Login = login;
                Password = password;
                Birthday = birthday;
                Access_Level = access_Level;
            }
        }
        public struct newProduct
        {
            public newProduct(int iD, string name, string storage_life, string product_Type)
            {
                ID = iD;
                Name = name;
                Storage_life = storage_life;
                Product_Type = product_Type;
            }

            public int ID { get; set; }
            public string Name { get; set; }
            public string Storage_life { get; set; }
            public string Product_Type { get; set; }
        }
        public struct newOrderList
        {
            public newOrderList(int iD, string count, string order, string dish)
            {
                ID = iD;
                Count = count;
                Order = order;
                Dish = dish;
            }

            public int ID { get; set; }
            public string Count { get; set; }
            public string Order { get; set; }
            public string Dish { get; set; }

        }
        public struct newOrder
        {
            public newOrder(int iD, string date, string time, string place, string transport, string worker, string client, string order_Type)
            {
                ID = iD;
                Date = date;
                Time = time;
                Place = place;
                Transport = transport;
                Worker = worker;
                Client = client;
                Order_Type = order_Type;
            }

            public int ID { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public string Place { get; set; }
            public string Transport { get; set; }
            public string Worker { get; set; }
            public string Client { get; set; }
            public string Order_Type { get; set; }
        }
        public struct newDish
        {
            public newDish(int iD, string name, double price, double weight, string composition, string dish_Type)
            {
                ID = iD;
                Name = name;
                Price = price;
                Weight = weight;
                Composition = composition;
                Dish_Type = dish_Type;
            }

            public int ID { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public double Weight { get; set; }
            public string Composition { get; set; }
            public string Dish_Type { get; set; }
        }
        public static IEnumerable<newBatch> GetNewBatches(MyDBContext objectMyDBContext)
        {
            return from batch in objectMyDBContext.Batch_Of_Products.ToList<Batch_of_products>()
                   from product in objectMyDBContext.Products.ToList<Product>()
                   where batch.Product.ID == product.ID
                   select (new newBatch(batch.ID, batch.Count, batch.Delivery_Date.ToShortDateString(), product.Name));
        }
        public static IEnumerable<newWorker> GetNewWorkers(MyDBContext objectMyDBContext)
        {
            return from worker in objectMyDBContext.Workers.ToList<Worker>()
                   from level in objectMyDBContext.Levels.ToList<AccessLevel>()
                   where worker.Access_Level.ID == level.ID
                   select (new newWorker(worker.ID, worker.Surname, worker.Name, worker.Lastname, worker.Adress, worker.Phone_Number, worker.Driver_License, worker.Gender, worker.Login, worker.Password, worker.Birthday.ToShortDateString(), level.Level));
        }
        public static IEnumerable<newProduct> GetNewProducts(MyDBContext objectMyDBContext)
        {
            return from type in objectMyDBContext.Product_Types.ToList<Product_Type>()
                   from product in objectMyDBContext.Products.ToList<Product>()
                   where product.Product_Type.ID == type.ID
                   select (new newProduct(product.ID, product.Name, product.Storage_life, type.Name));
        }
        public static IEnumerable<newOrderList> GetNewOrderLists(MyDBContext objectMyDBContext)
        {
            return from list in objectMyDBContext.Order_Lists.ToList<Order_list>()
                   from order in objectMyDBContext.Orders.ToList<Order>()
                   from dish in objectMyDBContext.Dishes.ToList<Dish>()
                   where list.Dish.ID == dish.ID && list.Order.ID == order.ID
                   select (new newOrderList(list.ID, list.Count, $"{order.Date.ToShortDateString()} {order.Time.ToShortTimeString()} {order.Place}", dish.Name));
        }
        public static IEnumerable<newOrder> GetNewOrders(MyDBContext objectMyDBContext)
        {
            return from order in objectMyDBContext.Orders.ToList<Order>()
                   from transport in objectMyDBContext.Transports.ToList<Transport>()
                   from client in objectMyDBContext.Clients.ToList<Client>()
                   from worker in objectMyDBContext.Workers.ToList<Worker>()
                   from type in objectMyDBContext.Order_Types.ToList<Order_Type>()
                   where order.Transport.ID == transport.ID && order.Client.ID == client.ID && order.Worker.ID == worker.ID && order.Order_Type.ID == type.ID
                   select (new newOrder(order.ID, order.Date.ToShortDateString(), order.Time.ToShortTimeString(), order.Place, $"{transport.Name}, {transport.Number}", $"{worker.Surname} {worker.Name} {worker.Lastname}",
                   $"{client.Surname} {client.Name} {client.Lastname}", type.Name));
        }
        public static IEnumerable<newDish> GetNewDishes(MyDBContext objectMyDBContext)
        {
            return from dish in objectMyDBContext.Dishes.ToList<Dish>()
                   from type in objectMyDBContext.Dish_Types.ToList<Dish_type>()
                   where dish.Dish_Type.ID == type.ID
                   select (new newDish(dish.ID, dish.Name, dish.Price, dish.Weight, dish.Composition, type.Name));
        }
    }
}
