using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClassLibrary1
{
    public class TodoDbContext : DbContext
    {

        public IDbSet<TodoItem> _items { get; set; }
        public TodoDbContext(String connection): base(connection)
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TodoItem>().HasKey(s => s.Id);
            modelBuilder.Entity<TodoItem>().Property(s => s.DateCreated).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(s => s.IsCompleted).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(s => s.Text).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(s => s.UserId).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(s => s.DateCompleted).IsOptional();  

        }

    }
}
