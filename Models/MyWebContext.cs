using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace razoWeb.models
{
    public class MyWebContext : IdentityDbContext<AppUser>
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

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if(tableName.StartsWith("AspNet")){
                    entityType.SetTableName(tableName.Substring(6));
                }
                
            }
        }
        
        
    }
    
}