using System.Data.Entity;
namespace WpfApplicationEntity.API
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base("DbConnectString") { }
        public DbSet<AccessLevel> Levels { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Batch_of_products> Batch_Of_Products{ get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Dish_products> Dish_Products { get; set; }
        public DbSet<Dish_type> Dish_Types{ get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<Order_list> Order_Lists{ get; set; }
        public DbSet<Order_Type> Order_Types{ get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Product_Type> Product_Types{ get; set; }
        public DbSet<Transport> Transports{ get; set; }
        public DbSet<Worker> Workers{ get; set; }
    }
}
