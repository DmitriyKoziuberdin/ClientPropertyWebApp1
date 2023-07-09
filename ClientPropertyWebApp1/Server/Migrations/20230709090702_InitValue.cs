using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClientPropertyWebApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Telephone = table.Column<string>(type: "text", nullable: true),
                    InitialSumOfUserProperty = table.Column<double>(type: "double precision", nullable: false),
                    CurrentSumOfUserProperty = table.Column<double>(type: "double precision", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameProperty = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TypeOfProperty = table.Column<string>(type: "text", nullable: true),
                    PurchaseDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    InitialValue = table.Column<double>(type: "double precision", nullable: false),
                    PriceLossPeriod = table.Column<string>(type: "text", nullable: true),
                    PriceLossSelectedPeriod = table.Column<double>(type: "double precision", nullable: false),
                    CurrentValue = table.Column<double>(type: "double precision", nullable: false),
                    DaysOfPropertyOwnership = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "CurrentSumOfUserProperty", "Email", "EmailConfirmed", "InitialSumOfUserProperty", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Telephone", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "Address", "b58a351c-1739-45d2-a050-206fd4b6878a", 100000000.0, null, false, 90000000.0, false, null, "User 1", null, null, null, null, false, "6b81e96d-f6a8-4c2b-8479-2aa5f2d7889e", "0957737020", false, null },
                    { 2, 0, "Address", "7b76e1d2-1d04-42e0-a27a-1da8ecc45006", 100000000.0, null, false, 90000000.0, false, null, "User 2", null, null, null, null, false, "adb8e518-8147-463c-a310-58875f3885bd", "0957737020", false, null }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "CurrentValue", "DaysOfPropertyOwnership", "InitialValue", "NameProperty", "PriceLossPeriod", "PriceLossSelectedPeriod", "PurchaseDate", "TypeOfProperty", "UserId" },
                values: new object[,]
                {
                    { 1, 1000.0, 10, 9000.0, "Property 1", "Year", 10.0, new DateTimeOffset(new DateTime(2023, 7, 9, 12, 7, 2, 234, DateTimeKind.Unspecified).AddTicks(3036), new TimeSpan(0, 3, 0, 0, 0)), "TypeOfProperty 1", 1 },
                    { 2, 10000000.0, 10, 9000000.0, "Property 2", "Year", 10.0, new DateTimeOffset(new DateTime(2023, 7, 9, 12, 7, 2, 234, DateTimeKind.Unspecified).AddTicks(3091), new TimeSpan(0, 3, 0, 0, 0)), "TypeOfProperty 2", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserId",
                table: "Properties",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
