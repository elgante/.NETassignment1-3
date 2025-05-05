using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class PostContext :DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    
    
/* Before genrating Post.db
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = Post.db");
       // optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);            
    }*/

//After generating Post.db
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Post.db");
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasKey(todo => todo.Id);
        /**You can also do some of the constraints in OnModelCreating(..). Here is an example
         * of limiting the Todo::Title to 50 characters:/
         */
        /*  modelBuilder.Entity<Todo>().Property(todo => todo.Title).HasMaxLength(50); */
        modelBuilder.Entity<User>().HasKey(user => user.Id);
    }
}