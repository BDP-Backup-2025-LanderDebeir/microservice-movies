using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Migrations.PostgreSQL
{
    /// <inheritdoc />
    public partial class AddSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Actors", "AgeRating", "Description", "Duration", "Genres", "PosterUrl", "Title", "Year" },
                values: new object[,]
                {
                    { "ebfb9308-6c61-4608-af77-394448808e9b", "Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss", 16, "A computer hacker learns from mysterious rebels about the true nature of his reality", 136, "Sci-fi", "https://www.imdb.com/title/tt0133093/", "The Matrix", 1999 },
                    { "fb258d1a-10a2-4bf9-85cd-ca83585d1ee5", "Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss", 16, "The human city of Zion defends itself against the massive invasion of the machines as Neo fights to end the war at another front while also opposing the rogue Agent Smith.", 138, "Sci-fi", "https://www.imdb.com/title/tt0234215/", "The Matrix Reloaded", 2003 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[,]
                {
                    { "45bfe58a-c9ba-44b6-911e-c1387f6e1ace", 200, "Room 2" },
                    { "f38145ab-9f1e-4778-90f4-b911fb5e15a7", 100, "Room 1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: "ebfb9308-6c61-4608-af77-394448808e9b");

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: "fb258d1a-10a2-4bf9-85cd-ca83585d1ee5");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: "45bfe58a-c9ba-44b6-911e-c1387f6e1ace");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: "f38145ab-9f1e-4778-90f4-b911fb5e15a7");
        }
    }
}
