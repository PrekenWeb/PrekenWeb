using Microsoft.EntityFrameworkCore.Migrations;

namespace PrekenWeb.WebApi.Migrations
{
    public partial class AddVideoEnMedidatiesZoekopdracht : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Meditaties",
                table: "ZoekOpdracht",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VideoPreken",
                table: "ZoekOpdracht",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Meditaties",
                table: "ZoekOpdracht");

            migrationBuilder.DropColumn(
                name: "VideoPreken",
                table: "ZoekOpdracht");
        }
    }
}
