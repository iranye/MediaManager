using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MediaManager.API.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Volumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Moniker = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volumes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "M3uFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TotalMegaBytes = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    VolumeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M3uFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_M3uFiles_Volumes_VolumeId",
                        column: x => x.VolumeId,
                        principalTable: "Volumes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "M3uFileEntry",
                columns: table => new
                {
                    M3uFileId = table.Column<int>(type: "integer", nullable: false),
                    FileEntryId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.InsertData(
                table: "FileEntries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "All of my love.mp3" },
                    { 2, "Rock and Roll All Nite.mp3" },
                    { 3, "Beat Box Extreme.mp3" },
                    { 4, "Lady in Red.mp3" },
                    { 5, "Back in the Saddle.mp3" }
                });

            migrationBuilder.InsertData(
                table: "Volumes",
                columns: new[] { "Id", "Created", "LastModified", "Moniker", "Title" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2023, 5, 12, 17, 30, 26, 838, DateTimeKind.Unspecified).AddTicks(7856), new TimeSpan(0, -4, 0, 0, 0)), null, "kgon-01", "KGON-01" },
                    { 2, new DateTimeOffset(new DateTime(2023, 5, 12, 17, 30, 26, 838, DateTimeKind.Unspecified).AddTicks(7897), new TimeSpan(0, -4, 0, 0, 0)), null, "mellow-01", "Mellow-01" }
                });

            migrationBuilder.InsertData(
                table: "M3uFiles",
                columns: new[] { "Id", "Created", "LastModified", "Title", "TotalMegaBytes", "VolumeId" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2023, 5, 12, 17, 30, 26, 840, DateTimeKind.Unspecified).AddTicks(1067), new TimeSpan(0, -4, 0, 0, 0)), null, "ShaNaNa.m3u", 42, 1 },
                    { 2, new DateTimeOffset(new DateTime(2023, 5, 12, 17, 30, 26, 840, DateTimeKind.Unspecified).AddTicks(1088), new TimeSpan(0, -4, 0, 0, 0)), null, "WakeAndBake.m3u", 42, 1 },
                    { 3, new DateTimeOffset(new DateTime(2023, 5, 12, 17, 30, 26, 840, DateTimeKind.Unspecified).AddTicks(1091), new TimeSpan(0, -4, 0, 0, 0)), null, "BravenHearts.m3u", 42, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_M3uFileEntry_M3uFileId",
                table: "M3uFileEntry",
                column: "M3uFileId");

            migrationBuilder.CreateIndex(
                name: "IX_M3uFiles_VolumeId",
                table: "M3uFiles",
                column: "VolumeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M3uFileEntry");

            migrationBuilder.DropTable(
                name: "FileEntries");

            migrationBuilder.DropTable(
                name: "M3uFiles");

            migrationBuilder.DropTable(
                name: "Volumes");
        }
    }
}
