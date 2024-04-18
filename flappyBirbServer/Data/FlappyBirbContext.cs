using flappyBirb_server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace flappyBirb_server.Data
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ajouter deux utilisateurs
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount" },
                values: new object[,]
                {
                    { "1", "user1", "USER1", "user1@example.com", "USER1@EXAMPLE.COM", true, "AQAAAAEAACcQAAAAEKJXKjAN7x3nFVnpGZ57OdD5SScKFdZ1pJX4DhuhOS5qJ/mjJG/Mm7E77L2U3BTujw==", "N34BXKXJFSE5LXSO3O22VTJVLH4RXHXG", "f4eb7b17-0864-48f5-af65-76f4a9e41a7a", false, false, false, 0 },
                    { "2", "user2", "USER2", "user2@example.com", "USER2@EXAMPLE.COM", true, "AQAAAAEAACcQAAAAEKJXKjAN7x3nFVnpGZ57OdD5SScKFdZ1pJX4DhuhOS5qJ/mjJG/Mm7E77L2U3BTujw==", "N34BXKXJFSE5LXSO3O22VTJVLH4RXHXG", "f4eb7b17-0864-48f5-af65-76f4a9e41a7a", false, false, false, 0 }
                });

            // Ajouter quatre scores (deux scores par utilisateur)
            migrationBuilder.InsertData(
                table: "Score",
                columns: new[] { "Id", "BirbUserId", "Date", "ScoreValue", "TimeInSeconds", "IsPublic" },
                values: new object[,]
                {
                    { 1, "1", new DateTime(2024, 4, 1), 100, 30, true },
                    { 2, "1", new DateTime(2024, 4, 2), 150, 40, false },
                    { 3, "2", new DateTime(2024, 4, 3), 200, 50, true },
                    { 4, "2", new DateTime(2024, 4, 4), 250, 60, false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Score",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Score",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Score",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Score",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
