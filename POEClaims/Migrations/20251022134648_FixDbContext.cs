using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POEClaims.Migrations
{
    /// <inheritdoc />
    public partial class FixDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClaimID",
                table: "Claims",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentPath",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Claims",
                newName: "ClaimID");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentPath",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
