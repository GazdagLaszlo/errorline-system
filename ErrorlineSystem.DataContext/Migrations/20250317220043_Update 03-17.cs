using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorlineSystem.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class Update0317 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AssignedId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ParentIssueId",
                table: "Issues");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Roles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AssignedIdId",
                table: "Issues",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Issues",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Issues",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentIssueIdId",
                table: "Issues",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssignedIdId",
                table: "Issues",
                column: "AssignedIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_CreatedById",
                table: "Issues",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ModifiedById",
                table: "Issues",
                column: "ModifiedById");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_AssignedIdId",
                table: "Issues",
                column: "AssignedIdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_CreatedById",
                table: "Issues",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_ModifiedById",
                table: "Issues",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Issues_ParentIssueIdId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_AssignedIdId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_CreatedById",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_ModifiedById",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssignedIdId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_CreatedById",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ModifiedById",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ParentIssueIdId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AssignedIdId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ParentIssueIdId",
                table: "Issues");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Roles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AssignedId",
                table: "Issues",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Issues",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Issues",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParentIssueId",
                table: "Issues",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");
        }
    }
}
