using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LSM.SsoService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_username",
                table: "user");

            migrationBuilder.DropColumn(
                name: "username",
                table: "user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_user_username",
                table: "user",
                column: "username",
                unique: true);
        }
    }
}
