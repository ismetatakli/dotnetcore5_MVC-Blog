using Blog.Data.Concrete.EntityFramework.Mappings;
using Blog.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Concrete.EntityFramework.Contexts
{
    public class MyBlogContext:IdentityDbContext<User,Role,int>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString:
                @"Server=DESKTOP-4H9DS7M\SQLEXPRESS;
                Database=MyBlogDB;
                User Id=ismetatakli;
                Password=1234;
                Trusted_Connection=True;
                MultipleActiveResultSets=true");

            //optionsBuilder.UseSqlServer(connectionString:
            //    @"Server=DESKTOP-37FPUFR;
            //    Database=MyBlogDB;
            //    User Id=ismet;
            //    Password=1234;
            //    Trusted_Connection=True;
            //    MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}
