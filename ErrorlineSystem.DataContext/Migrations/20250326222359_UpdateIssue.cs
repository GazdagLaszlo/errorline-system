using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorlineSystem.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Issues_ParentIssueIdId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ParentIssueIdId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ParentIssueIdId",
                table: "Issues");

            migrationBuilder.AddColumn<int>(
                name: "ParentIssueId",
                table: "Issues",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ParentIssueId",
                table: "Issues",
                column: "ParentIssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Issues_ParentIssueId",
                table: "Issues",
                column: "ParentIssueId",
                principalTable: "Issues",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Issues_ParentIssueId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ParentIssueId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ParentIssueId",
                table: "Issues");

            migrationBuilder.AddColumn<int>(
                name: "ParentIssueIdId",
                table: "Issues",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ParentIssueIdId",
                table: "Issues",
                column: "ParentIssueIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Issues_ParentIssueIdId",
                table: "Issues",
                column: "ParentIssueIdId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
