using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QrAppWebApi.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AttendanceDate",
                table: "Attendances",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AttendanceDate",
                table: "Attendances",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
