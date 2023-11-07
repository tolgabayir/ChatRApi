using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalR101.Migrations
{
    /// <inheritdoc />
    public partial class unreadedMessagesColumnAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnReadedMessages",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnReadedMessages",
                table: "AspNetUsers");
        }
    }
}
