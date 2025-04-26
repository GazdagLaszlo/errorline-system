using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorlineSystem.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class IssueChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_AssignedIdId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssignedIdId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "AssignedIdId",
                table: "Issues");

            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "Issues",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssignedUserId",
                table: "Issues",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_AssignedUserId",
                table: "Issues",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_AssignedUserId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssignedUserId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Issues");

            migrationBuilder.AddColumn<int>(
                name: "AssignedIdId",
                table: "Issues",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssignedIdId",
                table: "Issues",
                column: "AssignedIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_AssignedIdId",
                table: "Issues",
                column: "AssignedIdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
