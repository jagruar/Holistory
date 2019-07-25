using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Holistory.Infrastructure.Sql.Migrations
{
    public partial class use : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer");

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DropColumn(
                name: "UtcDateDeleted",
                table: "Answer");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Answer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "The Americas");

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Asia");

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Oceania");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Answer",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "UtcDateDeleted",
                table: "Answer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "North America");

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "South America");

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "South America");

            migrationBuilder.InsertData(
                table: "Region",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 7, "South America" },
                    { 8, "Southern Asia" },
                    { 9, "Northern Asia" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
