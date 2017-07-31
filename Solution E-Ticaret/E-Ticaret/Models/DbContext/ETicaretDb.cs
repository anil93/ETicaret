namespace E_Ticaret.Models.DbContext
{
    using E_Ticaret.Models.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class ETicaretDb : DbContext
    {
        // Your context has been configured to use a 'ETicaretDb' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'E_Ticaret.Models.DbContext.ETicaretDb' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ETicaretDb' 
        // connection string in the application configuration file.
        public ETicaretDb()
            : base("name=ETicaretDb")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<LogIn> LogIn { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}