using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LindCore.Manager.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebDataSetting_WebDataCtrl_WebDataCtrlId",
                table: "WebDataSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_WebDataSetting_WebManageRoles_WebManageRolesId",
                table: "WebDataSetting");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "WebManageRoles_WebManageMenus_Authority_R");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "WebManageRoles_WebManageMenus_Authority_R");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "WebManageRoles");

            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "WebManageMenus");

            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "WebDepartments");

            migrationBuilder.AlterColumn<int>(
                name: "WebManageRolesId",
                table: "WebDataSetting",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WebDataCtrlId",
                table: "WebDataSetting",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WebDataSetting_WebDataCtrl_WebDataCtrlId",
                table: "WebDataSetting",
                column: "WebDataCtrlId",
                principalTable: "WebDataCtrl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WebDataSetting_WebManageRoles_WebManageRolesId",
                table: "WebDataSetting",
                column: "WebManageRolesId",
                principalTable: "WebManageRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebDataSetting_WebDataCtrl_WebDataCtrlId",
                table: "WebDataSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_WebDataSetting_WebManageRoles_WebManageRolesId",
                table: "WebDataSetting");

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "WebManageRoles_WebManageMenus_Authority_R",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "WebManageRoles_WebManageMenus_Authority_R",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "WebManageRoles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentID",
                table: "WebManageMenus",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentID",
                table: "WebDepartments",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WebManageRolesId",
                table: "WebDataSetting",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "WebDataCtrlId",
                table: "WebDataSetting",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_WebDataSetting_WebDataCtrl_WebDataCtrlId",
                table: "WebDataSetting",
                column: "WebDataCtrlId",
                principalTable: "WebDataCtrl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WebDataSetting_WebManageRoles_WebManageRolesId",
                table: "WebDataSetting",
                column: "WebManageRolesId",
                principalTable: "WebManageRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
