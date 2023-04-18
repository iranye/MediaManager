using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaManager.API.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Volumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Moniker = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volumes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "M3uFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalMegaBytes = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VolumeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M3uFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_M3uFiles_Volumes_VolumeId",
                        column: x => x.VolumeId,
                        principalTable: "Volumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    M3uId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileEntries_M3uFiles_M3uId",
                        column: x => x.M3uId,
                        principalTable: "M3uFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Volumes",
                columns: new[] { "Id", "Created", "LastModified", "Moniker", "Title" },
                values: new object[] { 1, new DateTime(2023, 4, 18, 16, 3, 12, 579, DateTimeKind.Local).AddTicks(3882), null, "kgon-01", "KGON-01" });

            migrationBuilder.InsertData(
                table: "Volumes",
                columns: new[] { "Id", "Created", "LastModified", "Moniker", "Title" },
                values: new object[] { 2, new DateTime(2023, 4, 18, 16, 3, 12, 579, DateTimeKind.Local).AddTicks(3918), null, "mellow-01", "Mellow-01" });

            migrationBuilder.InsertData(
                table: "M3uFiles",
                columns: new[] { "Id", "Created", "LastModified", "Title", "TotalMegaBytes", "VolumeId" },
                values: new object[] { 1, new DateTime(2023, 4, 18, 16, 3, 12, 579, DateTimeKind.Local).AddTicks(4004), null, "ShaNaNa.m3u", 42, 1 });

            migrationBuilder.InsertData(
                table: "M3uFiles",
                columns: new[] { "Id", "Created", "LastModified", "Title", "TotalMegaBytes", "VolumeId" },
                values: new object[] { 2, new DateTime(2023, 4, 18, 16, 3, 12, 579, DateTimeKind.Local).AddTicks(4008), null, "WakeAndBake.m3u", 42, 1 });

            migrationBuilder.InsertData(
                table: "M3uFiles",
                columns: new[] { "Id", "Created", "LastModified", "Title", "TotalMegaBytes", "VolumeId" },
                values: new object[] { 3, new DateTime(2023, 4, 18, 16, 3, 12, 579, DateTimeKind.Local).AddTicks(4010), null, "BravenHearts.m3u", 42, 2 });

            migrationBuilder.InsertData(
                table: "FileEntries",
                columns: new[] { "Id", "M3uId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "All of my love.mp3" },
                    { 2, 1, "Rock and Roll All Nite.mp3" },
                    { 3, 2, "Beat Box Extreme.mp3" },
                    { 4, 3, "Lady in Red.mp3" },
                    { 5, 3, "Back in the Saddle.mp3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileEntries_M3uId",
                table: "FileEntries",
                column: "M3uId");

            migrationBuilder.CreateIndex(
                name: "IX_M3uFiles_VolumeId",
                table: "M3uFiles",
                column: "VolumeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileEntries");

            migrationBuilder.DropTable(
                name: "M3uFiles");

            migrationBuilder.DropTable(
                name: "Volumes");
        }
    }
}
