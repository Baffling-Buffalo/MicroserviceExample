using Microsoft.EntityFrameworkCore.Migrations;

namespace Contact.API.Migrations
{
    public partial class ContactContextModelSnapshot_AddedOnDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__contact_l__f_con__37A5467C",
                table: "contact_list");

            migrationBuilder.DropForeignKey(
                name: "FK__contact_l__f_lis__38996AB5",
                table: "contact_list");

            migrationBuilder.AddForeignKey(
                name: "FK__contact_l__f_con__37A5467C",
                table: "contact_list",
                column: "f_contact",
                principalTable: "contact",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__contact_l__f_lis__38996AB5",
                table: "contact_list",
                column: "f_list",
                principalTable: "list",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__contact_l__f_con__37A5467C",
                table: "contact_list");

            migrationBuilder.DropForeignKey(
                name: "FK__contact_l__f_lis__38996AB5",
                table: "contact_list");

            migrationBuilder.AddForeignKey(
                name: "FK__contact_l__f_con__37A5467C",
                table: "contact_list",
                column: "f_contact",
                principalTable: "contact",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__contact_l__f_lis__38996AB5",
                table: "contact_list",
                column: "f_list",
                principalTable: "list",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
