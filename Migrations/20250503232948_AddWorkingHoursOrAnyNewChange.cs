using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalonBooking.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkingHoursOrAnyNewChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpenTime",
                table: "WorkingHours",
                newName: "WorkStart");

            migrationBuilder.RenameColumn(
                name: "CloseTime",
                table: "WorkingHours",
                newName: "WorkEnd");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Businesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Businesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Businesses");

            migrationBuilder.RenameColumn(
                name: "WorkStart",
                table: "WorkingHours",
                newName: "OpenTime");

            migrationBuilder.RenameColumn(
                name: "WorkEnd",
                table: "WorkingHours",
                newName: "CloseTime");
        }
    }
}
