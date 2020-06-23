using Microsoft.EntityFrameworkCore;

namespace csharpexam.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> users {get;set;}
        public DbSet<Gathering> gatherings {get;set;}
        public DbSet<Association> associations {get;set;}
        
        // this is the variable we will use to connect to the MySQL table, Lizards
        //public DbSet<User> Users { get; set; }
    }
}