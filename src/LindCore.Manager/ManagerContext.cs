using LindCore.Domain.Entities;
using LindCore.Manager.Entities;
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

        public DbSet<WebDataSetting> WebDataSetting { get; set; }
        public DbSet<WebDataCtrl> WebDataCtrl { get; set; }
        public DbSet<WebManageUsers> WebManageUsers { get; set; }
        public DbSet<WebManageRoles> WebManageRoles { get; set; }
        public DbSet<WebManageMenus> WebManageMenus { get; set; }
        public DbSet<WebDepartments> WebDepartments { get; set; }
        public DbSet<WebConfirmRecord> WebConfirmRecord { get; set; }
        public DbSet<WebManageRoles_WebManageMenus_Authority_R> WebManageRoles_WebManageMenus_Authority_R { get; set; }
        public DbSet<WebLogger> WebLogger { get; set; }
        public DbSet<WebAuthorityCommands> WebAuthorityCommands { get; set; }
    }

}

