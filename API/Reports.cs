using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
namespace WpfApplicationEntity.API
{
    static class Reports
    {
        public static void ReportWorker()
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "Times New Roman";
            workSheet.Cells[1, 1] = "Фамилия";
            workSheet.Cells[1, 2] = "Имя";
            workSheet.Cells[1, 3] = "Отчество";
            workSheet.Cells[1, 4] = "Адрес";
            workSheet.Cells[1, 5] = "Номер телефона";
            workSheet.Cells[1, 6] = "Пол";
            workSheet.Cells[1, 7] = "Логин";
            workSheet.Cells[1, 8] = "Пароль";
            workSheet.Cells[1, 9] = "Дата рождения";
            workSheet.Cells[1, 10] = "Должность";
            int i = 2;
            using (MyDBContext db = new MyDBContext())
            {
                foreach (Worker item in db.Workers.ToList())
                {
                    workSheet.Cells[i, 1] = item.Surname;
                    workSheet.Cells[i, 2] = item.Name;
                    workSheet.Cells[i, 3] = item.Lastname;
                    workSheet.Cells[i, 4] = item.Adress;
                    workSheet.Cells[i, 5] = item.Phone_Number;
                    if (item.Gender)
                        workSheet.Cells[i, 6] = "Мужской";
                    else
                        workSheet.Cells[i, 6] = "Женский";
                    workSheet.Cells[i, 7] = item.Login;
                    workSheet.Cells[i, 8] = item.Password;
                    workSheet.Cells[i, 9] = item.Birthday.ToShortDateString();
                    workSheet.Cells[i, 10] = item.Access_Level.Level;
                    i++;
                }
            }
            string path = Environment.CurrentDirectory + "\\WorkersReport.xls";
            workSheet.SaveAs(path);
            exApp.Quit();
        }
        public static void ReportOrder()
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "Times New Roman";
            workSheet.Cells[1, 1] = "Дата";
            workSheet.Cells[1, 2] = "Время";
            workSheet.Cells[1, 3] = "Место";
            workSheet.Cells[1, 4] = "Транспорт";
            workSheet.Cells[1, 5] = "Работник";
            workSheet.Cells[1, 6] = "Клиент";
            workSheet.Cells[1, 7] = "Тип заказа";
            int i = 2;
            using (MyDBContext db = new MyDBContext())
            {
                foreach (Order item in db.Orders.ToList())
                {
                    workSheet.Cells[i, 1] = item.Date.ToShortDateString();
                    workSheet.Cells[i, 2] = item.Time.ToShortTimeString();
                    workSheet.Cells[i, 3] = item.Place;
                    workSheet.Cells[i, 4] = item.Transport.Name;
                    workSheet.Cells[i, 5] = $"{item.Worker.Surname} {item.Worker.Name} {item.Worker.Lastname}";
                    workSheet.Cells[i, 6] = $"{item.Client.Surname} {item.Client.Name} {item.Client.Lastname}";
                    workSheet.Cells[i, 7] = item.Order_Type.Name;
                    i++;
                }
            }
            string path = Environment.CurrentDirectory + "\\OrdersReport.xls";
            workSheet.SaveAs(path);
            exApp.Quit();
        }
        public static void ReportProduct()
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "Times New Roman";
            workSheet.Cells[1, 1] = "Название";
            workSheet.Cells[1, 2] = "Срок хранения";
            workSheet.Cells[1, 3] = "Тип продукта";
            int i = 2;
            using (MyDBContext db = new MyDBContext())
            {
                foreach (Product item in db.Products.ToList())
                {
                    workSheet.Cells[i, 1] = item.Name;
                    workSheet.Cells[i, 2] = item.Storage_life;
                    workSheet.Cells[i, 3] = item.Product_Type.Name;
                    i++;
                }
            }
            string path = Environment.CurrentDirectory + "\\ProductsReport.xls";
            workSheet.SaveAs(path);
            exApp.Quit();
        }
        public static void ReportTransport()
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "Times New Roman";
            workSheet.Cells[1, 1] = "Название";
            workSheet.Cells[1, 2] = "Номер";
            int i = 2;
            using (MyDBContext db = new MyDBContext())
            {
                foreach (Transport item in db.Transports.ToList())
                {
                    workSheet.Cells[i, 1] = item.Name;
                    workSheet.Cells[i, 2] = item.Number;
                    i++;
                }
            }
            string path = Environment.CurrentDirectory + "\\TransportReport.xls";
            workSheet.SaveAs(path);
            exApp.Quit();
        }
        public static void ReportBatch()
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "Times New Roman";
            workSheet.Cells[1, 1] = "Количество";
            workSheet.Cells[1, 2] = "Дата";
            workSheet.Cells[1, 3] = "Продукт";
            int i = 2;
            using (MyDBContext db = new MyDBContext())
            {
                foreach (Batch_of_products item in db.Batch_Of_Products.ToList())
                {
                    workSheet.Cells[i, 1] = item.Count;
                    workSheet.Cells[i, 2] = item.Delivery_Date;
                    workSheet.Cells[i, 3] = $"{item.Product.Name}, {item.Product.Product_Type.Name}";
                    i++;
                }
            }
            string path = Environment.CurrentDirectory + "\\BatchesReport.xls";
            workSheet.SaveAs(path);
            exApp.Quit();
        }
        public static void ReportProductType()
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "Times New Roman";
            workSheet.Cells[1, 1] = "Название";
            workSheet.Cells[1, 2] = "Описание";
            int i = 2;
            using (MyDBContext db = new MyDBContext())
            {
                foreach (Product_Type item in db.Product_Types.ToList())
                {
                    workSheet.Cells[i, 1] = item.Name;
                    workSheet.Cells[i, 2] = item.Description;
                    i++;
                }
            }
            string path = Environment.CurrentDirectory + "\\ProductTypesReport.xls";
            workSheet.SaveAs(path);
            exApp.Quit();
        }
        public static void ReportDishType()
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "Times New Roman";
            workSheet.Cells[1, 1] = "Название";
            workSheet.Cells[1, 2] = "Описание";
            int i = 2;
            using (MyDBContext db = new MyDBContext())
            {
                foreach (Dish_type item in db.Dish_Types.ToList())
                {
                    workSheet.Cells[i, 1] = item.Name;
                    workSheet.Cells[i, 2] = item.Description;
                    i++;
                }
            }
            string path = Environment.CurrentDirectory + "\\DishTypesReport.xls";
            workSheet.SaveAs(path);
            exApp.Quit();
        }
        public static void ReportDish()
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "Times New Roman";
            workSheet.Cells[1, 1] = "Название";
            workSheet.Cells[1, 2] = "Цена";
            workSheet.Cells[1, 3] = "Вес";
            workSheet.Cells[1, 4] = "Состав";
            workSheet.Cells[1, 5] = "Тип блюда";
            int i = 2;
            using (MyDBContext db = new MyDBContext())
            {
                foreach (Dish item in db.Dishes.ToList())
                {
                    workSheet.Cells[i, 1] = item.Name;
                    workSheet.Cells[i, 2] = item.Price;
                    workSheet.Cells[i, 3] = item.Weight;
                    workSheet.Cells[i, 4] = item.Composition;
                    workSheet.Cells[i, 5] = $"{item.Dish_Type.Name}, {item.Dish_Type.Description}";
                    i++;
                }
            }
            string path = Environment.CurrentDirectory + "\\DishesReport.xls";
            workSheet.SaveAs(path);
            exApp.Quit();
        }
        public static void ReportOrderList()
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "Times New Roman";
            workSheet.Cells[1, 1] = "Фамилия";
            workSheet.Cells[1, 2] = "Имя";
            workSheet.Cells[1, 3] = "Отчество";
            int i = 2;
            using (MyDBContext db = new MyDBContext())
            {
                foreach (Order_list item in db.Order_Lists.ToList())
                {
                    workSheet.Cells[i, 1] = item.Count;
                    workSheet.Cells[i, 2] = item.Dish.Name;
                    workSheet.Cells[i, 3] = $"{item.Order.Date}, {item.Order.Time}, {item.Order.Place}";
                    i++;
                }
            }
            string path = Environment.CurrentDirectory + "\\OrderListsReport.xls";
            workSheet.SaveAs(path);
            exApp.Quit();
        }
        public static void ReportClient()
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "Times New Roman";
            workSheet.Cells[1, 1] = "Фамилия";
            workSheet.Cells[1, 2] = "Имя";
            workSheet.Cells[1, 3] = "Отчество";
            workSheet.Cells[1, 4] = "Номер телефона";
            int i = 2;
            using (MyDBContext db = new MyDBContext())
            {
                foreach (Client item in db.Clients.ToList())
                {
                    workSheet.Cells[i, 1] = item.Surname;
                    workSheet.Cells[i, 2] = item.Name;
                    workSheet.Cells[i, 3] = item.Lastname;
                    workSheet.Cells[i, 4] = item.Phone_Number;
                    i++;
                }
            }
            string path = Environment.CurrentDirectory + "\\ClientsReport.xls";
            workSheet.SaveAs(path);
            exApp.Quit();
        }
        public static void ReportOrderType()
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "Times New Roman";
            workSheet.Cells[1, 1] = "Название";
            workSheet.Cells[1, 2] = "Описание";
            workSheet.Cells[1, 3] = "Статус";
            int i = 2;
            using (MyDBContext db = new MyDBContext())
            {
                foreach (Order_Type item in db.Order_Types.ToList())
                {
                    workSheet.Cells[i, 1] = item.Name;
                    workSheet.Cells[i, 2] = item.Description;
                    workSheet.Cells[i, 3] = item.Status;
                    i++;
                }
            }
            string path = Environment.CurrentDirectory + "\\OrderTypesReport.xls";
            workSheet.SaveAs(path);
            exApp.Quit();
        }
    }
}
