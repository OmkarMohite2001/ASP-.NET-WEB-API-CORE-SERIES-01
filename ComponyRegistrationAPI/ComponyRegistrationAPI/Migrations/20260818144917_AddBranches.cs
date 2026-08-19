using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComponyRegistrationAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBranches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponyBranches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponyBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponyBranches_Componies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Componies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponyBranches_CompanyId",
                table: "ComponyBranches",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponyBranches");
        }
    }
}
