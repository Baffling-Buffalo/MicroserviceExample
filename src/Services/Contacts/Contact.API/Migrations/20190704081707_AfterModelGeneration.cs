using Microsoft.EntityFrameworkCore.Migrations;

namespace Contact.API.Migrations
{
    public partial class AfterModelGeneration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__list__parent_id__47DBAE45",
                table: "list");

            migrationBuilder.DropIndex(
                name: "IX_list_parent_id",
                table: "list");

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "contact",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "contact",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
