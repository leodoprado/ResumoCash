using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumoCash.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_categories_category_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "IX_transactions_category_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "IX_categories_user_id",
                table: "categories");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_categories_user_id_id",
                table: "categories",
                columns: new[] { "user_id", "id" });

            migrationBuilder.CreateIndex(
                name: "IX_transactions_user_id_category_id",
                table: "transactions",
                columns: new[] { "user_id", "category_id" });

            migrationBuilder.CreateIndex(
                name: "IX_transactions_user_id_competence_month",
                table: "transactions",
                columns: new[] { "user_id", "competence_month" });

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_categories_user_id_category_id",
                table: "transactions",
                columns: new[] { "user_id", "category_id" },
                principalTable: "categories",
                principalColumns: new[] { "user_id", "id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_users_user_id",
                table: "transactions",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_categories_user_id_category_id",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_users_user_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "IX_transactions_user_id_category_id",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "IX_transactions_user_id_competence_month",
                table: "transactions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_categories_user_id_id",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "transactions");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_category_id",
                table: "transactions",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_categories_user_id",
                table: "categories",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_categories_category_id",
                table: "transactions",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
