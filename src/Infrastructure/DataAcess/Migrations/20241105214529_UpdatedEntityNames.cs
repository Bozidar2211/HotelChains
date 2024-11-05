using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntityNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Hotels",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "NumberOfEmloyees",
                table: "Hotels",
                newName: "NumberOfEmployees");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "HotelChains",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "HotelChains");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Hotels",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "NumberOfEmployees",
                table: "Hotels",
                newName: "NumberOfEmloyees");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Employees",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
