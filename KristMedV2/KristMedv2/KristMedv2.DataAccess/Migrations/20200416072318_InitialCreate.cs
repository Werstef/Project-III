using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KristMedv2.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PhoneNo = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Manufacturer = table.Column<string>(nullable: false),
                    UsageTimeAvailable = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Manufacturer = table.Column<string>(nullable: false),
                    QuantityStock = table.Column<int>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    MessageText = table.Column<string>(type: "varchar(5000)", nullable: false),
                    ClientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactMessages_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Diagnosis = table.Column<string>(type: "varchar(5000)", nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    MedicId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treatments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treatments_Medics_MedicId",
                        column: x => x.MedicId,
                        principalTable: "Medics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    AdminId = table.Column<Guid>(nullable: false),
                    TreatmentId = table.Column<Guid>(nullable: true),
                    MedicId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Medics_MedicId",
                        column: x => x.MedicId,
                        principalTable: "Medics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Equipment_Treatments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UsageTime = table.Column<int>(nullable: false),
                    EquipmentTypeId = table.Column<Guid>(nullable: true),
                    TreatmentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment_Treatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_Treatments_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Equipment_Treatments_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medication_Treatments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    QuantityUsed = table.Column<int>(nullable: false),
                    MedicationTypeId = table.Column<Guid>(nullable: true),
                    TreatmentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication_Treatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medication_Treatments_MedicationTypes_MedicationTypeId",
                        column: x => x.MedicationTypeId,
                        principalTable: "MedicationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medication_Treatments_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(type: "varchar(5000)", nullable: false),
                    TreatmentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AdminId",
                table: "Appointments",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClientId",
                table: "Appointments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MedicId",
                table: "Appointments",
                column: "MedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TreatmentId",
                table: "Appointments",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Username",
                table: "Clients",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactMessages_ClientId",
                table: "ContactMessages",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Treatments_EquipmentTypeId",
                table: "Equipment_Treatments",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Treatments_TreatmentId",
                table: "Equipment_Treatments",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_Treatments_MedicationTypeId",
                table: "Medication_Treatments",
                column: "MedicationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_Treatments_TreatmentId",
                table: "Medication_Treatments",
                column: "TreatmentId");

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
                name: "IX_Posts_TreatmentId",
                table: "Posts",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_ClientId",
                table: "Treatments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_MedicId",
                table: "Treatments",
                column: "MedicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "Equipment_Treatments");

            migrationBuilder.DropTable(
                name: "Medication_Treatments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "EquipmentTypes");

            migrationBuilder.DropTable(
                name: "MedicationTypes");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Medics");
        }
    }
}
