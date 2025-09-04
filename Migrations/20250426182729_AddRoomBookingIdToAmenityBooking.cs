using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomBookingIdToAmenityBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "room_booking_id",
                schema: "core",
                table: "room_booking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "room_booking_id",
                schema: "core",
                table: "amenity_booking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_amenity_booking_room_booking_id",
                schema: "core",
                table: "amenity_booking",
                column: "room_booking_id");

            migrationBuilder.AddForeignKey(
                name: "amenity_booking_room_booking_id_fkey",
                schema: "core",
                table: "amenity_booking",
                column: "room_booking_id",
                principalSchema: "core",
                principalTable: "room_booking",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "amenity_booking_room_booking_id_fkey",
                schema: "core",
                table: "amenity_booking");

            migrationBuilder.DropIndex(
                name: "IX_amenity_booking_room_booking_id",
                schema: "core",
                table: "amenity_booking");

            migrationBuilder.DropColumn(
                name: "room_booking_id",
                schema: "core",
                table: "room_booking");

            migrationBuilder.DropColumn(
                name: "room_booking_id",
                schema: "core",
                table: "amenity_booking");
        }
    }
}
