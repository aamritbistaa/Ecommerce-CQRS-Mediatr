using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserStatusandOTPtoCredential : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserStatus",
                table: "Vendors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OTP",
                table: "UsersCredentials",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserStatus",
                table: "UsersCredentials",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserStatus",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "UsersCredentials");

            migrationBuilder.DropColumn(
                name: "UserStatus",
                table: "UsersCredentials");
        }
    }
}
