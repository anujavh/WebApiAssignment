using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAssignemnt.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDeatails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDeatails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageDetails",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    senderId = table.Column<int>(type: "int", nullable: true),
                    receiverId = table.Column<int>(type: "int", nullable: false),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageDetails", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_MessageDetails_UserDeatails_receiverId",
                        column: x => x.receiverId,
                        principalTable: "UserDeatails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageDetails_UserDeatails_senderId",
                        column: x => x.senderId,
                        principalTable: "UserDeatails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageDetails_receiverId",
                table: "MessageDetails",
                column: "receiverId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageDetails_senderId",
                table: "MessageDetails",
                column: "senderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageDetails");

            migrationBuilder.DropTable(
                name: "UserDeatails");
        }
    }
}
