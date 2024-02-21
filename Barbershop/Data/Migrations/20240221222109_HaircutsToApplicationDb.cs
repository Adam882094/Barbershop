using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbershop.Data.Migrations
{
    public partial class HaircutsToApplicationDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Barbers_BarberId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Haircut_HaircutId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_HaircutId",
                table: "Appointment",
                newName: "IX_Appointment_HaircutId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_BarberId",
                table: "Appointment",
                newName: "IX_Appointment_BarberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Barbers_BarberId",
                table: "Appointment",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "BarberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Haircut_HaircutId",
                table: "Appointment",
                column: "HaircutId",
                principalTable: "Haircut",
                principalColumn: "HaircutId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Barbers_BarberId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Haircut_HaircutId",
                table: "Appointment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_HaircutId",
                table: "Appointments",
                newName: "IX_Appointments_HaircutId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_BarberId",
                table: "Appointments",
                newName: "IX_Appointments_BarberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Barbers_BarberId",
                table: "Appointments",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "BarberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Haircut_HaircutId",
                table: "Appointments",
                column: "HaircutId",
                principalTable: "Haircut",
                principalColumn: "HaircutId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
