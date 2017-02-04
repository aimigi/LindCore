using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LindCore.Manager.Migrations
{
    public partial class init0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebAuthorityCommands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActionName = table.Column<string>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false),
                    ClassName = table.Column<string>(nullable: true),
                    Feature = table.Column<int>(nullable: false),
                    Flag = table.Column<long>(nullable: false),
                    LastedTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebAuthorityCommands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebConfirmRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddTime = table.Column<DateTime>(nullable: false),
                    AuditedStatus = table.Column<int>(nullable: false),
                    AuditedUserId = table.Column<int>(nullable: false),
                    AuditedUserName = table.Column<string>(nullable: true),
                    AuditedWorkFlow = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LastedTime = table.Column<DateTime>(nullable: false),
                    ToUserId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebConfirmRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebDataCtrl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddTime = table.Column<DateTime>(nullable: false),
                    DataCtrlApi = table.Column<string>(nullable: false),
                    DataCtrlField = table.Column<string>(nullable: false),
                    DataCtrlName = table.Column<string>(nullable: false),
                    DataCtrlType = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LastedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebDataCtrl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    About = table.Column<string>(nullable: true),
                    AddTime = table.Column<DateTime>(nullable: false),
                    FatherId = table.Column<int>(nullable: true),
                    LastedTime = table.Column<DateTime>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Operator = table.Column<string>(nullable: true),
                    ParentID = table.Column<int>(nullable: true),
                    SortNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebDepartments_WebDepartments_FatherId",
                        column: x => x.FatherId,
                        principalTable: "WebDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WebLogger",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActionName = table.Column<string>(nullable: true),
                    AddTime = table.Column<DateTime>(nullable: false),
                    Authority = table.Column<string>(nullable: true),
                    ControllerName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LastedTime = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    OwnerName = table.Column<string>(nullable: true),
                    RequestParams = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebLogger", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebManageMenus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    About = table.Column<string>(nullable: true),
                    AddTime = table.Column<DateTime>(nullable: false),
                    Authority = table.Column<long>(nullable: false),
                    FatherId = table.Column<int>(nullable: true),
                    IsDisplayMenuTree = table.Column<bool>(nullable: false),
                    LastedTime = table.Column<DateTime>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    LinkUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Operator = table.Column<string>(nullable: true),
                    ParentID = table.Column<int>(nullable: true),
                    SortNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebManageMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebManageMenus_WebManageMenus_FatherId",
                        column: x => x.FatherId,
                        principalTable: "WebManageMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WebManageRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    About = table.Column<string>(nullable: true),
                    AddTime = table.Column<DateTime>(nullable: false),
                    DepartmentID = table.Column<int>(nullable: false),
                    LastedTime = table.Column<DateTime>(nullable: false),
                    Operator = table.Column<string>(nullable: true),
                    RoleName = table.Column<string>(nullable: false),
                    SortNumber = table.Column<int>(nullable: false),
                    WebDepartmentsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebManageRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebManageRoles_WebDepartments_WebDepartmentsId",
                        column: x => x.WebDepartmentsId,
                        principalTable: "WebDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WebDataSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddTime = table.Column<DateTime>(nullable: false),
                    LastedTime = table.Column<DateTime>(nullable: false),
                    ObjectIdArr = table.Column<string>(nullable: false),
                    ObjectNameArr = table.Column<string>(nullable: true),
                    WebDataCtrlId = table.Column<int>(nullable: false),
                    WebDepartmentsId = table.Column<int>(nullable: false),
                    WebManageRolesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebDataSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebDataSetting_WebDataCtrl_WebDataCtrlId",
                        column: x => x.WebDataCtrlId,
                        principalTable: "WebDataCtrl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebDataSetting_WebManageRoles_WebManageRolesId",
                        column: x => x.WebManageRolesId,
                        principalTable: "WebManageRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebManageRoles_WebManageMenus_Authority_R",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddTime = table.Column<DateTime>(nullable: false),
                    Authority = table.Column<long>(nullable: false),
                    LastedTime = table.Column<DateTime>(nullable: false),
                    MenuId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    WebManageMenusId = table.Column<int>(nullable: true),
                    WebManageRolesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebManageRoles_WebManageMenus_Authority_R", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebManageRoles_WebManageMenus_Authority_R_WebManageMenus_WebManageMenusId",
                        column: x => x.WebManageMenusId,
                        principalTable: "WebManageMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WebManageRoles_WebManageMenus_Authority_R_WebManageRoles_WebManageRolesId",
                        column: x => x.WebManageRolesId,
                        principalTable: "WebManageRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WebManageUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    LastedTime = table.Column<DateTime>(nullable: false),
                    LoginName = table.Column<string>(nullable: false),
                    Mobile = table.Column<string>(nullable: false),
                    Operator = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    RealName = table.Column<string>(nullable: false),
                    ThridUserId = table.Column<string>(nullable: true),
                    WebDepartmentsId = table.Column<int>(nullable: true),
                    WebManageRolesId = table.Column<int>(nullable: true),
                    WebSystemID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebManageUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebManageUsers_WebDepartments_WebDepartmentsId",
                        column: x => x.WebDepartmentsId,
                        principalTable: "WebDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WebManageUsers_WebManageRoles_WebManageRolesId",
                        column: x => x.WebManageRolesId,
                        principalTable: "WebManageRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WebDataSetting_WebDataCtrlId",
                table: "WebDataSetting",
                column: "WebDataCtrlId");

            migrationBuilder.CreateIndex(
                name: "IX_WebDataSetting_WebManageRolesId",
                table: "WebDataSetting",
                column: "WebManageRolesId");

            migrationBuilder.CreateIndex(
                name: "IX_WebDepartments_FatherId",
                table: "WebDepartments",
                column: "FatherId");

            migrationBuilder.CreateIndex(
                name: "IX_WebManageMenus_FatherId",
                table: "WebManageMenus",
                column: "FatherId");

            migrationBuilder.CreateIndex(
                name: "IX_WebManageRoles_WebDepartmentsId",
                table: "WebManageRoles",
                column: "WebDepartmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_WebManageRoles_WebManageMenus_Authority_R_WebManageMenusId",
                table: "WebManageRoles_WebManageMenus_Authority_R",
                column: "WebManageMenusId");

            migrationBuilder.CreateIndex(
                name: "IX_WebManageRoles_WebManageMenus_Authority_R_WebManageRolesId",
                table: "WebManageRoles_WebManageMenus_Authority_R",
                column: "WebManageRolesId");

            migrationBuilder.CreateIndex(
                name: "IX_WebManageUsers_WebDepartmentsId",
                table: "WebManageUsers",
                column: "WebDepartmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_WebManageUsers_WebManageRolesId",
                table: "WebManageUsers",
                column: "WebManageRolesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebAuthorityCommands");

            migrationBuilder.DropTable(
                name: "WebConfirmRecord");

            migrationBuilder.DropTable(
                name: "WebDataSetting");

            migrationBuilder.DropTable(
                name: "WebLogger");

            migrationBuilder.DropTable(
                name: "WebManageRoles_WebManageMenus_Authority_R");

            migrationBuilder.DropTable(
                name: "WebManageUsers");

            migrationBuilder.DropTable(
                name: "WebDataCtrl");

            migrationBuilder.DropTable(
                name: "WebManageMenus");

            migrationBuilder.DropTable(
                name: "WebManageRoles");

            migrationBuilder.DropTable(
                name: "WebDepartments");
        }
    }
}
