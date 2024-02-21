using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbershop.Data.Migrations
{
    public partial class AddedHaircuts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BarberName",
                table: "Barbers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "HaircutId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Haircut",
                columns: table => new
                {
                    HaircutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Haircut", x => x.HaircutId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_HaircutId",
                table: "Appointments",
                column: "HaircutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Haircut_HaircutId",
                table: "Appointments",
                column: "HaircutId",
                principalTable: "Haircut",
                principalColumn: "HaircutId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Haircut_HaircutId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "Haircut");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_HaircutId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "HaircutId",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "BarberName",
                table: "Barbers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
