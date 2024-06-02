using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ousiaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tensileModulus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shearModulus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    volumeFraction = table.Column<double>(type: "float", nullable: false),
                    weightFraction = table.Column<double>(type: "float", nullable: false),
                    poissonRatio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fiberOrientationAngle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fiberOrientationMass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    angle = table.Column<double>(type: "float", nullable: false),
                    plyWeight = table.Column<double>(type: "float", nullable: false),
                    plyThickness = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plies", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plies");
        }
    }
}
