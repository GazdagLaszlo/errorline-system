using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorlineSystem.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class Updateissue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Facilities_FacilityId",
                table: "Issues");

            migrationBuilder.AlterColumn<int>(
                name: "FacilityId",
                table: "Issues",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedId",
                table: "Issues",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "Issues",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Issues",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Issues",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InternalComment",
                table: "Issues",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IssueTypeId",
                table: "Issues",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Item",
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

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Issues",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ParentIssueId",
                table: "Issues",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Issues",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Facilities_FacilityId",
                table: "Issues",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Facilities_FacilityId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "AssignedId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "InternalComment",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "IssueTypeId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Item",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ParentIssueId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Issues");

            migrationBuilder.AlterColumn<int>(
                name: "FacilityId",
                table: "Issues",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Facilities_FacilityId",
                table: "Issues",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");
        }
    }
}
