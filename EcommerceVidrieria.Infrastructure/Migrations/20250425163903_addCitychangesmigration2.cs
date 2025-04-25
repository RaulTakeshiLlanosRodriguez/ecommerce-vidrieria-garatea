using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceVidrieria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCitychangesmigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_City_CityId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_City_CityId",
                table: "Orders",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_City_CityId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_City_CityId",
                table: "Orders",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
