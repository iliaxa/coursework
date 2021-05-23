using System;
using System.Text;
using System.Windows;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace WpfApplicationEntity.API
{
    static class DataBaseToFile
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DbConnectString"].ConnectionString;
        private static StreamWriter File;
        public static void ExportDataBase()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    File = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Catering.csv", false, Encoding.Unicode);
                    SelectTable("SELECT * FROM AccessLevels", connection);
                    SelectTable("SELECT * FROM Clients", connection);
                    SelectTable("SELECT * FROM Batch_of_products", connection);
                    SelectTable("SELECT * FROM Dish_type", connection);
                    SelectTable("SELECT * FROM Dishes", connection);
                    SelectTable("SELECT * FROM Order_list", connection);
                    SelectTable("SELECT * FROM Order_Type", connection);
                    SelectTable("SELECT * FROM Orders", connection);
                    SelectTable("SELECT * FROM Product_Type", connection);
                    SelectTable("SELECT * FROM Products", connection);
                    SelectTable("SELECT * FROM Transports", connection);
                    SelectTable("SELECT * FROM Workers", connection);
                    File.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    connection.Dispose();
                    MessageBox.Show("База данных успешно сохранена", "Успех");
                }
            }
        }
        private static void SelectTable(string query, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader SQLreader = command.ExecuteReader();
            if (SQLreader.HasRows)
            {
                File.WriteLine("#");
                while (SQLreader.Read())
                {
                    for (int i = 1; i < SQLreader.FieldCount; i++)
                        File.Write(SQLreader.GetValue(i).ToString() + ";");
                    File.WriteLine("$");
                }
            }
            connection.Close();
        }
        private static void InsertTable(string query, string[] values, int count, SqlConnection connection)
        {
            query += $" values (";
            for (int i = 0; i < count; i++)
            {
                query += $"'{values[i]}', ";
            }
            query = query.Substring(0, query.Length - 2);
            query += ")";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public static void ImportDataBase()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (StreamReader streamReader = new StreamReader("Catering.csv"))
                    {
                        string[] tables = streamReader.ReadToEnd().Replace(Environment.NewLine, "").Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                        if (tables.Length == 12)
                        {
                            for (int i = 0; i < tables[0].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[0].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into AccessLevels(Level)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 1, connection);
                            }
                            for (int i = 0; i < tables[1].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[1].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Clients(Surname,Name,Lastname,Phone_Number)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 4, connection);
                            }
                            for (int i = 0; i < tables[11].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[11].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Workers(Surname,Name,Lastname,Adress,Phone_Number,Driver_License,Gender,Login,Password,Birthday,Access_Level_ID)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 11, connection);
                            }
                            for (int i = 0; i < tables[3].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[3].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Dish_type(Name,Description)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 2, connection);
                            }
                            for (int i = 0; i < tables[6].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[6].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Order_Type(Name,Description,Status)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 3, connection);
                            }
                            for (int i = 0; i < tables[8].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[8].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Product_Type(Name,Description)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 2, connection);
                            }
                            for (int i = 0; i < tables[10].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[10].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Transports(Name,Number)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 2, connection);
                            }
                            for (int i = 0; i < tables[9].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[9].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Products(Name,Storage_life,Product_Type_ID)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 3, connection);
                            }
                            for (int i = 0; i < tables[2].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[2].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Batch_of_products(Count,Delivery_Date,Product_ID)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 3, connection);
                            }
                            for (int i = 0; i < tables[4].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[4].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Dishes(Name,Price,Weight,Composition,Dish_Type_ID)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 5, connection);
                            }
                            for (int i = 0; i < tables[7].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[7].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Orders(Date,Time,Place,Client_ID,Order_Type_ID,Transport_ID,Worker_ID)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 7, connection);
                            }
                            for (int i = 0; i < tables[5].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                            {
                                string[] values = tables[5].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                InsertTable("insert into Order_list(Count,Dish_ID,Order_ID)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 3, connection);
                            }
                        }
                        else MessageBox.Show("Количество таблиц не соответсвует таблицам базы данных", "Ошибка");
                        streamReader.Close();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}
