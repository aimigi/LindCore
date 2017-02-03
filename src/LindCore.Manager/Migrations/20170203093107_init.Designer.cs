using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LindCore.Manager;

namespace LindCore.Manager.Migrations
{
    [DbContext(typeof(ManagerContext))]
    [Migration("20170203093107_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LindCore.Manager.UserTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<DateTime>("LastedTime");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("UserTest");
                });
        }
    }
}
