using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Holistory.Infrastructure.Sql.Migrations
{
    public partial class usersnotaccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attempt_Account_AccountId",
                table: "Attempt");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Attempt_AccountId",
                table: "Attempt");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Attempt");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Attempt",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attempt_UserId",
                table: "Attempt",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attempt_AspNetUsers_UserId",
                table: "Attempt",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attempt_AspNetUsers_UserId",
                table: "Attempt");

            migrationBuilder.DropIndex(
                name: "IX_Attempt_UserId",
                table: "Attempt");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Attempt");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Attempt",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    UtcDateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attempt_AccountId",
                table: "Attempt",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attempt_Account_AccountId",
                table: "Attempt",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
