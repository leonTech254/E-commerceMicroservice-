using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartService.Migrations
{
    /// <inheritdoc />
    public partial class InitMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    pricePerItem = table.Column<int>(type: "int", nullable: false),
                    totalprice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart");
        }
    }
}
