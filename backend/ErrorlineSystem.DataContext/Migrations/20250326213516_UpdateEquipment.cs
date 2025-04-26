using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorlineSystem.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Equipments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Equipments");
        }
    }
}
