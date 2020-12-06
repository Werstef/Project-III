using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KristMedv2.DataAccess.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Medics_Code",
                table: "Medics");

            migrationBuilder.DropIndex(
                name: "IX_Medics_Username",
                table: "Medics");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Username",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Admins_Code",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_Username",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Medics",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Medics",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Admins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Admins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Medics",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Medics",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Clients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Admins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Admins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Medics_Code",
                table: "Medics",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medics_Username",
                table: "Medics",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Username",
                table: "Clients",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Code",
                table: "Admins",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Username",
                table: "Admins",
                column: "Username",
                unique: true);
        }
    }
}
