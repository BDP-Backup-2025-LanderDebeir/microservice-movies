using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Migrations.PostgreSQL
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Genres = table.Column<string>(type: "text", nullable: false),
                    Actors = table.Column<string>(type: "text", nullable: false),
                    AgeRating = table.Column<int>(type: "integer", nullable: false),
                    PosterUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieEvents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    MovieId = table.Column<string>(type: "text", nullable: false),
                    RoomId = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Visitors = table.Column<int>(type: "integer", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieEvents_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_MovieEvents_MovieId",
                table: "MovieEvents",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieEvents");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
