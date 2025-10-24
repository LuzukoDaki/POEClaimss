using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POEClaims.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Claims");
        }
    }
}
