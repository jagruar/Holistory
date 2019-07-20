using Microsoft.EntityFrameworkCore.Migrations;

namespace Holistory.Infrastructure.Sql.Migrations
{
    public partial class inits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTopic_Account_AccountId1",
                table: "AccountTopic");

            migrationBuilder.DropIndex(
                name: "IX_AccountTopic_AccountId1",
                table: "AccountTopic");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "AccountTopic");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId1",
                table: "AccountTopic",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountTopic_AccountId1",
                table: "AccountTopic",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTopic_Account_AccountId1",
                table: "AccountTopic",
                column: "AccountId1",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
