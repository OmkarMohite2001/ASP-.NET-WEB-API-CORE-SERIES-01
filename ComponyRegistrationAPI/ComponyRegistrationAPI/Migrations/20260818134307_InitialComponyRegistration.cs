using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComponyRegistrationAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialComponyRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compony",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compony", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponyRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponyId = table.Column<int>(type: "int", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationAuthority = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponyRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponyRegistration_Compony_ComponyId",
                        column: x => x.ComponyId,
                        principalTable: "Compony",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponyRegistration_ComponyId",
                table: "ComponyRegistration",
                column: "ComponyId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponyRegistration");

            migrationBuilder.DropTable(
                name: "Compony");
        }
    }
}
