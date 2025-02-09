﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class PhotoTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "Photos",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
            .Annotation("Sqlite:Autoincrement", true),
                IsMain = table.Column<bool>(type: "INTEGER", nullable: false),
                Url = table.Column<string>(type: "TEXT", nullable: false),
                PublicId = table.Column<string>(type: "TEXT", nullable: true),
                AppUserId = table.Column<int>(type: "INTEGER", nullable: false)
            },
    constraints: table =>
    {
        table.PrimaryKey("PK_Photos", x => x.Id);
        table.ForeignKey(
            name: "FK_Photos_Users_AppUserId",
            column: x => x.AppUserId,
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "Photos");

            migrationBuilder.DropTable(
            name: "Users");
        }
    }
}
