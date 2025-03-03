using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig221 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussion_Users_UserId",
                table: "Discussion");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionReply_Discussion_DiscussionId",
                table: "DiscussionReply");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionReply_Users_UserId",
                table: "DiscussionReply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscussionReply",
                table: "DiscussionReply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discussion",
                table: "Discussion");

            migrationBuilder.RenameTable(
                name: "DiscussionReply",
                newName: "DiscussionReplies");

            migrationBuilder.RenameTable(
                name: "Discussion",
                newName: "Discussions");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionReply_UserId",
                table: "DiscussionReplies",
                newName: "IX_DiscussionReplies_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionReply_DiscussionId",
                table: "DiscussionReplies",
                newName: "IX_DiscussionReplies_DiscussionId");

            migrationBuilder.RenameIndex(
                name: "IX_Discussion_UserId",
                table: "Discussions",
                newName: "IX_Discussions_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscussionReplies",
                table: "DiscussionReplies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discussions",
                table: "Discussions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionReplies_Discussions_DiscussionId",
                table: "DiscussionReplies",
                column: "DiscussionId",
                principalTable: "Discussions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionReplies_Users_UserId",
                table: "DiscussionReplies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_Users_UserId",
                table: "Discussions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionReplies_Discussions_DiscussionId",
                table: "DiscussionReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionReplies_Users_UserId",
                table: "DiscussionReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_Users_UserId",
                table: "Discussions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discussions",
                table: "Discussions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscussionReplies",
                table: "DiscussionReplies");

            migrationBuilder.RenameTable(
                name: "Discussions",
                newName: "Discussion");

            migrationBuilder.RenameTable(
                name: "DiscussionReplies",
                newName: "DiscussionReply");

            migrationBuilder.RenameIndex(
                name: "IX_Discussions_UserId",
                table: "Discussion",
                newName: "IX_Discussion_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionReplies_UserId",
                table: "DiscussionReply",
                newName: "IX_DiscussionReply_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionReplies_DiscussionId",
                table: "DiscussionReply",
                newName: "IX_DiscussionReply_DiscussionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discussion",
                table: "Discussion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscussionReply",
                table: "DiscussionReply",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussion_Users_UserId",
                table: "Discussion",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionReply_Discussion_DiscussionId",
                table: "DiscussionReply",
                column: "DiscussionId",
                principalTable: "Discussion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionReply_Users_UserId",
                table: "DiscussionReply",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
