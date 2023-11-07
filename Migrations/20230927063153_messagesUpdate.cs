using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalR101.Migrations
{
    /// <inheritdoc />
    public partial class messagesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_UserID",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_UserID",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Message",
                newName: "RecieverId");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Message",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<bool>(
                name: "ReceiptInfo",
                table: "Message",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Message_RecieverId",
                table: "Message",
                column: "RecieverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_Id",
                table: "Message",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_RecieverId",
                table: "Message",
                column: "RecieverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_Id",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_RecieverId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_RecieverId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ReceiptInfo",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "RecieverId",
                table: "Message",
                newName: "UserName");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Message",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Message",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserID",
                table: "Message",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_UserID",
                table: "Message",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
