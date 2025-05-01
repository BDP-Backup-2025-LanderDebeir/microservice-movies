using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Migrations.PostgreSQL
{
    /// <inheritdoc />
    public partial class AddBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    MovieEventId = table.Column<string>(type: "text", nullable: false),
                    BookingStatus = table.Column<int>(type: "integer", nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    StandardVisitors = table.Column<int>(type: "integer", nullable: false),
                    DiscountVisitors = table.Column<int>(type: "integer", nullable: false),
                    SeatNumbers = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_MovieEvents_MovieEventId",
                        column: x => x.MovieEventId,
                        principalTable: "MovieEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_MovieEventId",
                table: "Bookings",
                column: "MovieEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
