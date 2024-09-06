using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LSM.SsoService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddResetPasswordTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reset_password_token",
                columns: table => new
                {
                    token_expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reset_password_token", x => new { x.token_expiry_date, x.user_id });
                    table.ForeignKey(
                        name: "FK_reset_password_token_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reset_password_token_user_id",
                table: "reset_password_token",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reset_password_token");
        }
    }
}
