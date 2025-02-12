using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Data.Migrations
{
    public partial class RemoveDurationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Durations_DurationId",
                table: "Shows");

            migrationBuilder.DropTable(
                name: "Durations");

            migrationBuilder.DropIndex(
                name: "IX_Shows_DurationId",
                table: "Shows");

            migrationBuilder.RenameColumn(
                name: "DurationId",
                table: "Shows",
                newName: "DurationValue");

            migrationBuilder.AddColumn<int>(
                name: "DurationType",
                table: "Shows",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationType",
                table: "Shows");

            migrationBuilder.RenameColumn(
                name: "DurationValue",
                table: "Shows",
                newName: "DurationId");

            migrationBuilder.CreateTable(
                name: "Durations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Durations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shows_DurationId",
                table: "Shows",
                column: "DurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Durations_DurationId",
                table: "Shows",
                column: "DurationId",
                principalTable: "Durations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
