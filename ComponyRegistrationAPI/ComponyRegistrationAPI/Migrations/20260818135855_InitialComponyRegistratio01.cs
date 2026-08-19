using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComponyRegistrationAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialComponyRegistratio01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponyRegistration_Compony_ComponyId",
                table: "ComponyRegistration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponyRegistration",
                table: "ComponyRegistration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Compony",
                table: "Compony");

            migrationBuilder.RenameTable(
                name: "ComponyRegistration",
                newName: "ComponyRegistrations");

            migrationBuilder.RenameTable(
                name: "Compony",
                newName: "Componies");

            migrationBuilder.RenameIndex(
                name: "IX_ComponyRegistration_ComponyId",
                table: "ComponyRegistrations",
                newName: "IX_ComponyRegistrations_ComponyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponyRegistrations",
                table: "ComponyRegistrations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Componies",
                table: "Componies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponyRegistrations_Componies_ComponyId",
                table: "ComponyRegistrations",
                column: "ComponyId",
                principalTable: "Componies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponyRegistrations_Componies_ComponyId",
                table: "ComponyRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponyRegistrations",
                table: "ComponyRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Componies",
                table: "Componies");

            migrationBuilder.RenameTable(
                name: "ComponyRegistrations",
                newName: "ComponyRegistration");

            migrationBuilder.RenameTable(
                name: "Componies",
                newName: "Compony");

            migrationBuilder.RenameIndex(
                name: "IX_ComponyRegistrations_ComponyId",
                table: "ComponyRegistration",
                newName: "IX_ComponyRegistration_ComponyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponyRegistration",
                table: "ComponyRegistration",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Compony",
                table: "Compony",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponyRegistration_Compony_ComponyId",
                table: "ComponyRegistration",
                column: "ComponyId",
                principalTable: "Compony",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
