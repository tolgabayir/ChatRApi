using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalR101.Migrations
{
    /// <inheritdoc />
    public partial class messageRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_RecieverId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_RecieverId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "RecieverId",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Message",
                newName: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReceiverId",
                table: "Message",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_ReceiverId",
                table: "Message",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_ReceiverId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ReceiverId",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Message",
                newName: "Text");

            migrationBuilder.AddColumn<string>(
                name: "RecieverId",
                table: "Message",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Message_RecieverId",
                table: "Message",
                column: "RecieverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_RecieverId",
                table: "Message",
                column: "RecieverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
