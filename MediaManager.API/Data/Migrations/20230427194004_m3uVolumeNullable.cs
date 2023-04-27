using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaManager.API.Migrations
{
    public partial class m3uVolumeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M3uFiles_Volumes_VolumeId",
                table: "M3uFiles");

            migrationBuilder.AlterColumn<int>(
                name: "VolumeId",
                table: "M3uFiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_M3uFiles_Volumes_VolumeId",
                table: "M3uFiles",
                column: "VolumeId",
                principalTable: "Volumes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M3uFiles_Volumes_VolumeId",
                table: "M3uFiles");

            migrationBuilder.AlterColumn<int>(
                name: "VolumeId",
                table: "M3uFiles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 18, 16, 3, 12, 579, DateTimeKind.Local).AddTicks(4004));

            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 18, 16, 3, 12, 579, DateTimeKind.Local).AddTicks(4008));

            migrationBuilder.UpdateData(
                table: "M3uFiles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 18, 16, 3, 12, 579, DateTimeKind.Local).AddTicks(4010));

            migrationBuilder.UpdateData(
                table: "Volumes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 18, 16, 3, 12, 579, DateTimeKind.Local).AddTicks(3882));

            migrationBuilder.UpdateData(
                table: "Volumes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 18, 16, 3, 12, 579, DateTimeKind.Local).AddTicks(3918));

            migrationBuilder.AddForeignKey(
                name: "FK_M3uFiles_Volumes_VolumeId",
                table: "M3uFiles",
                column: "VolumeId",
                principalTable: "Volumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
