using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaManager.API.Migrations
{
    public partial class updateseed01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTimeOffset(new DateTime(2023, 5, 17, 9, 25, 38, 950, DateTimeKind.Unspecified).AddTicks(9920), new TimeSpan(0, -4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTimeOffset(new DateTime(2023, 5, 17, 9, 25, 38, 950, DateTimeKind.Unspecified).AddTicks(9945), new TimeSpan(0, -4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTimeOffset(new DateTime(2023, 5, 17, 9, 25, 38, 950, DateTimeKind.Unspecified).AddTicks(9948), new TimeSpan(0, -4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Volumes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTimeOffset(new DateTime(2023, 5, 17, 9, 25, 38, 949, DateTimeKind.Unspecified).AddTicks(8566), new TimeSpan(0, -4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Volumes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTimeOffset(new DateTime(2023, 5, 17, 9, 25, 38, 949, DateTimeKind.Unspecified).AddTicks(8612), new TimeSpan(0, -4, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Volumes",
                columns: new[] { "Id", "Created", "LastModified", "Moniker", "Title" },
                values: new object[] { 3, new DateTimeOffset(new DateTime(2023, 5, 17, 9, 25, 38, 949, DateTimeKind.Unspecified).AddTicks(8615), new TimeSpan(0, -4, 0, 0, 0)), null, "kgon-02", "KGON-02" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Volumes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTimeOffset(new DateTime(2023, 5, 12, 17, 30, 26, 840, DateTimeKind.Unspecified).AddTicks(1067), new TimeSpan(0, -4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTimeOffset(new DateTime(2023, 5, 12, 17, 30, 26, 840, DateTimeKind.Unspecified).AddTicks(1088), new TimeSpan(0, -4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTimeOffset(new DateTime(2023, 5, 12, 17, 30, 26, 840, DateTimeKind.Unspecified).AddTicks(1091), new TimeSpan(0, -4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Volumes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTimeOffset(new DateTime(2023, 5, 12, 17, 30, 26, 838, DateTimeKind.Unspecified).AddTicks(7856), new TimeSpan(0, -4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Volumes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTimeOffset(new DateTime(2023, 5, 12, 17, 30, 26, 838, DateTimeKind.Unspecified).AddTicks(7897), new TimeSpan(0, -4, 0, 0, 0)));
        }
    }
}
