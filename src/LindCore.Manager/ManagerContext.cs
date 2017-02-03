using LindCore.Domain.Entities;
using LindCore.Manager.Models;
using Microsoft.EntityFrameworkCore;

namespace LindCore.Manager
{
    /// <summary>
    /// LindDb这个数据库的上下文对象
    /// </summary>
    public class ManagerContext : DbContext
    {
        public ManagerContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;database=tsingda_zzl_test1;user id=sa;password=zzl123;");

            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<UserTest> UserTest { get; set; }

    }

    public class UserTest : EntityInt
    {
        public string Name { get; set; }
    }
}

