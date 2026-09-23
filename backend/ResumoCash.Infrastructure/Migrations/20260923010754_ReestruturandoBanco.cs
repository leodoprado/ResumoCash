using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumoCash.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReestruturandoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_completed",
                table: "transactions");

            migrationBuilder.AddColumn<string>(
                name: "process_status",
                table: "transactions",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "transactions",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "process_status",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "status",
                table: "transactions");

            migrationBuilder.AddColumn<bool>(
                name: "is_completed",
                table: "transactions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
