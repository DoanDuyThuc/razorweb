using Microsoft.EntityFrameworkCore;

namespace razoWeb.models
{
    public class MyWebContext : DbContext
    {

        public MyWebContext(DbContextOptions<MyWebContext> options) : base(options) 
        { 
            //..
        }

        public DbSet<Article> article {set;get;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
        
    }
    
}