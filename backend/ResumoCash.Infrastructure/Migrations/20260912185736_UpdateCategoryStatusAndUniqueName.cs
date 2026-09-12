using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumoCash.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoryStatusAndUniqueName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "categories");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "categories",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "categories");

            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
