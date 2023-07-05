using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFlattSampleApp.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Organization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organization_OrganizationId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organization",
                table: "Organization");

            migrationBuilder.RenameTable(
                name: "Organization",
                newName: "Organizations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations");

            migrationBuilder.RenameTable(
                name: "Organizations",
                newName: "Organization");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organization",
                table: "Organization",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organization_OrganizationId",
                table: "Users",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
