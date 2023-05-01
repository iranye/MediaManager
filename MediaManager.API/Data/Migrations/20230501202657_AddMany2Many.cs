using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaManager.API.Migrations
{
    public partial class AddMany2Many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileEntries_M3uFiles_M3uId",
                table: "FileEntries");

            migrationBuilder.DropIndex(
                name: "IX_FileEntries_M3uId",
                table: "FileEntries");

            migrationBuilder.DropColumn(
                name: "M3uId",
                table: "FileEntries");

            migrationBuilder.CreateTable(
                name: "M3uFileEntry",
                columns: table => new
                {
                    M3uFileId = table.Column<int>(type: "int", nullable: false),
                    FileEntryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M3uFileEntry", x => new { x.FileEntryId, x.M3uFileId });
                    table.ForeignKey(
                        name: "FK_M3uFileEntry_FileEntries_FileEntryId",
                        column: x => x.FileEntryId,
                        principalTable: "FileEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M3uFileEntry_M3uFiles_M3uFileId",
                        column: x => x.M3uFileId,
                        principalTable: "M3uFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 5, 1, 16, 26, 56, 829, DateTimeKind.Local).AddTicks(1703));

            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 5, 1, 16, 26, 56, 829, DateTimeKind.Local).AddTicks(1717));

            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 5, 1, 16, 26, 56, 829, DateTimeKind.Local).AddTicks(1719));

            migrationBuilder.UpdateData(
                table: "Volumes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 5, 1, 16, 26, 56, 828, DateTimeKind.Local).AddTicks(847));

            migrationBuilder.UpdateData(
                table: "Volumes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 5, 1, 16, 26, 56, 828, DateTimeKind.Local).AddTicks(875));

            migrationBuilder.CreateIndex(
                name: "IX_M3uFileEntry_M3uFileId",
                table: "M3uFileEntry",
                column: "M3uFileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M3uFileEntry");

            migrationBuilder.AddColumn<int>(
                name: "M3uId",
                table: "FileEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "FileEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "M3uId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "FileEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "M3uId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "FileEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "M3uId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "FileEntries",
                keyColumn: "Id",
                keyValue: 4,
                column: "M3uId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "FileEntries",
                keyColumn: "Id",
                keyValue: 5,
                column: "M3uId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 27, 15, 40, 4, 715, DateTimeKind.Local).AddTicks(2363));

            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 27, 15, 40, 4, 715, DateTimeKind.Local).AddTicks(2366));

            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 27, 15, 40, 4, 715, DateTimeKind.Local).AddTicks(2368));

            migrationBuilder.UpdateData(
                table: "Volumes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 27, 15, 40, 4, 715, DateTimeKind.Local).AddTicks(2257));

            migrationBuilder.UpdateData(
                table: "Volumes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 27, 15, 40, 4, 715, DateTimeKind.Local).AddTicks(2285));

            migrationBuilder.CreateIndex(
                name: "IX_FileEntries_M3uId",
                table: "FileEntries",
                column: "M3uId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileEntries_M3uFiles_M3uId",
                table: "FileEntries",
                column: "M3uId",
                principalTable: "M3uFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
