using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reclaim.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixListingOrderRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Orders_Id",
                table: "Listings");

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "Listings",
                type: "character varying(24)",
                maxLength: 24,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_OrderId",
                table: "Listings",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Orders_OrderId",
                table: "Listings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Orders_OrderId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_OrderId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Listings");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Orders_Id",
                table: "Listings",
                column: "Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
