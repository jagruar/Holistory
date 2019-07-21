using Microsoft.EntityFrameworkCore.Migrations;

namespace Holistory.Infrastructure.Sql.Migrations
{
    public partial class usersnotaccountssss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Attempt",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Attempt",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
