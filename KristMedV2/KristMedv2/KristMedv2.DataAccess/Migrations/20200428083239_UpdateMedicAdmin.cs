using Microsoft.EntityFrameworkCore.Migrations;

namespace KristMedv2.DataAccess.Migrations
{
    public partial class UpdateMedicAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Medics");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Medics");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Admins");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Medics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Medics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
