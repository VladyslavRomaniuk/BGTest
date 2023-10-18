using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BgLocal.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedAuthorsAndBooksTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, new DateOnly(1799, 6, 6), "Alexander", "Pushkin" },
                    { 2, new DateOnly(1828, 9, 9), "Lev", "Tolstoy" },
                    { 3, new DateOnly(1818, 11, 9), "Ivan", "Turgenev" },
                    { 4, new DateOnly(1809, 4, 1), "Nikolay", "Gogol" },
                    { 5, new DateOnly(1814, 10, 15), "Mikhail", "Lermontov" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Genre", "Title", "YearOfPublication" },
                values: new object[,]
                {
                    { 1, "Historical novel", "The Captain's Daughter", 1836 },
                    { 2, "Historical novel", "War and Peace", 1867 },
                    { 3, "Fiction ", "Mumu", 1854 },
                    { 4, "Picaresque", "Dead Souls", 1842 },
                    { 5, "Romance novel", "A Hero of Our Time", 1840 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
