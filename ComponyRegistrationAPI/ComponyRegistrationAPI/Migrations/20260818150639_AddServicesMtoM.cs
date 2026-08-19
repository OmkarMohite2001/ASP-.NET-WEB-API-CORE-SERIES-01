using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComponyRegistrationAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddServicesMtoM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponyService",
                columns: table => new
                {
                    ComponiesId = table.Column<int>(type: "int", nullable: false),
                    servicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponyService", x => new { x.ComponiesId, x.servicesId });
                    table.ForeignKey(
                        name: "FK_ComponyService_Componies_ComponiesId",
                        column: x => x.ComponiesId,
                        principalTable: "Componies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponyService_services_servicesId",
                        column: x => x.servicesId,
                        principalTable: "services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponyService_servicesId",
                table: "ComponyService",
                column: "servicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponyService");

            migrationBuilder.DropTable(
                name: "services");
        }
    }
}
