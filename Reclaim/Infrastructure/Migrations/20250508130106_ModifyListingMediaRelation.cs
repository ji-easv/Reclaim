using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reclaim.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyListingMediaRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListingId1",
                table: "Media",
                type: "character varying(24)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_ListingId1",
                table: "Media",
                column: "ListingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Listings_ListingId1",
                table: "Media",
                column: "ListingId1",
                principalTable: "Listings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Listings_ListingId1",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_ListingId1",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "ListingId1",
                table: "Media");
        }
    }
}
