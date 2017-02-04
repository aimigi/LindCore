using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LindCore.Manager;
using LindCore.Domain.Entities;

namespace LindCore.Manager.Migrations
{
    [DbContext(typeof(ManagerContext))]
    [Migration("20170204024137_init1")]
    partial class init1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LindCore.Domain.Entities.WebAuthorityCommands", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionName")
                        .IsRequired();

                    b.Property<DateTime>("AddTime");

                    b.Property<string>("ClassName");

                    b.Property<int>("Feature");

                    b.Property<long>("Flag");

                    b.Property<DateTime>("LastedTime");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("WebAuthorityCommands");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebConfirmRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<int>("AuditedStatus");

                    b.Property<int>("AuditedUserId");

                    b.Property<string>("AuditedUserName");

                    b.Property<string>("AuditedWorkFlow");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastedTime");

                    b.Property<int>("ToUserId");

                    b.Property<int>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("WebConfirmRecord");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebDataCtrl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<string>("DataCtrlApi")
                        .IsRequired();

                    b.Property<string>("DataCtrlField")
                        .IsRequired();

                    b.Property<string>("DataCtrlName")
                        .IsRequired();

                    b.Property<string>("DataCtrlType")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastedTime");

                    b.HasKey("Id");

                    b.ToTable("WebDataCtrl");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebDataSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<DateTime>("LastedTime");

                    b.Property<string>("ObjectIdArr")
                        .IsRequired();

                    b.Property<string>("ObjectNameArr");

                    b.Property<int?>("WebDataCtrlId");

                    b.Property<int>("WebDepartmentsId");

                    b.Property<int?>("WebManageRolesId");

                    b.HasKey("Id");

                    b.HasIndex("WebDataCtrlId");

                    b.HasIndex("WebManageRolesId");

                    b.ToTable("WebDataSetting");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebDepartments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<DateTime>("AddTime");

                    b.Property<int?>("FatherId");

                    b.Property<DateTime>("LastedTime");

                    b.Property<int>("Level");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Operator");

                    b.Property<int>("SortNumber");

                    b.HasKey("Id");

                    b.HasIndex("FatherId");

                    b.ToTable("WebDepartments");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebLogger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionName");

                    b.Property<DateTime>("AddTime");

                    b.Property<string>("Authority");

                    b.Property<string>("ControllerName");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastedTime");

                    b.Property<int>("OwnerId");

                    b.Property<string>("OwnerName");

                    b.Property<string>("RequestParams");

                    b.Property<int>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("WebLogger");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebManageMenus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<DateTime>("AddTime");

                    b.Property<long>("Authority");

                    b.Property<int?>("FatherId");

                    b.Property<bool>("IsDisplayMenuTree");

                    b.Property<DateTime>("LastedTime");

                    b.Property<int>("Level");

                    b.Property<string>("LinkUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Operator");

                    b.Property<int>("SortNumber");

                    b.HasKey("Id");

                    b.HasIndex("FatherId");

                    b.ToTable("WebManageMenus");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebManageRoles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<DateTime>("AddTime");

                    b.Property<DateTime>("LastedTime");

                    b.Property<string>("Operator");

                    b.Property<string>("RoleName")
                        .IsRequired();

                    b.Property<int>("SortNumber");

                    b.Property<int?>("WebDepartmentsId");

                    b.HasKey("Id");

                    b.HasIndex("WebDepartmentsId");

                    b.ToTable("WebManageRoles");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebManageRoles_WebManageMenus_Authority_R", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<long>("Authority");

                    b.Property<DateTime>("LastedTime");

                    b.Property<int?>("WebManageMenusId");

                    b.Property<int?>("WebManageRolesId");

                    b.HasKey("Id");

                    b.HasIndex("WebManageMenusId");

                    b.HasIndex("WebManageRolesId");

                    b.ToTable("WebManageRoles_WebManageMenus_Authority_R");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebManageUsers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime");

                    b.Property<string>("Description");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<DateTime>("LastedTime");

                    b.Property<string>("LoginName")
                        .IsRequired();

                    b.Property<string>("Mobile")
                        .IsRequired();

                    b.Property<string>("Operator");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("RealName")
                        .IsRequired();

                    b.Property<string>("ThridUserId");

                    b.Property<int?>("WebDepartmentsId");

                    b.Property<int?>("WebManageRolesId");

                    b.Property<int?>("WebSystemID");

                    b.HasKey("Id");

                    b.HasIndex("WebDepartmentsId");

                    b.HasIndex("WebManageRolesId");

                    b.ToTable("WebManageUsers");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebDataSetting", b =>
                {
                    b.HasOne("LindCore.Manager.Entities.WebDataCtrl", "WebDataCtrl")
                        .WithMany("WebDataSetting")
                        .HasForeignKey("WebDataCtrlId");

                    b.HasOne("LindCore.Manager.Entities.WebManageRoles", "WebManageRoles")
                        .WithMany("WebDataSetting")
                        .HasForeignKey("WebManageRolesId");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebDepartments", b =>
                {
                    b.HasOne("LindCore.Manager.Entities.WebDepartments", "Father")
                        .WithMany("Sons")
                        .HasForeignKey("FatherId");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebManageMenus", b =>
                {
                    b.HasOne("LindCore.Manager.Entities.WebManageMenus", "Father")
                        .WithMany("Sons")
                        .HasForeignKey("FatherId");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebManageRoles", b =>
                {
                    b.HasOne("LindCore.Manager.Entities.WebDepartments", "WebDepartments")
                        .WithMany("WebManageRoles")
                        .HasForeignKey("WebDepartmentsId");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebManageRoles_WebManageMenus_Authority_R", b =>
                {
                    b.HasOne("LindCore.Manager.Entities.WebManageMenus", "WebManageMenus")
                        .WithMany("WebManageRoles_WebManageMenus_Authority_R")
                        .HasForeignKey("WebManageMenusId");

                    b.HasOne("LindCore.Manager.Entities.WebManageRoles", "WebManageRoles")
                        .WithMany("WebManageRoles_WebManageMenus_Authority_R")
                        .HasForeignKey("WebManageRolesId");
                });

            modelBuilder.Entity("LindCore.Manager.Entities.WebManageUsers", b =>
                {
                    b.HasOne("LindCore.Manager.Entities.WebDepartments", "WebDepartments")
                        .WithMany("WebManageUsers")
                        .HasForeignKey("WebDepartmentsId");

                    b.HasOne("LindCore.Manager.Entities.WebManageRoles", "WebManageRoles")
                        .WithMany("WebManageUsers")
                        .HasForeignKey("WebManageRolesId");
                });
        }
    }
}
