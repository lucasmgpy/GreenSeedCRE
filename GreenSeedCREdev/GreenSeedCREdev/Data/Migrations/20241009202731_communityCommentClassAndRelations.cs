using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenSeedCREdev.Data.Migrations
{
    /// <inheritdoc />
    public partial class communityCommentClassAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityPhotoUploads_AspNetUsers_UserId1",
                table: "CommunityPhotoUploads");

            migrationBuilder.DropIndex(
                name: "IX_CommunityPhotoUploads_UserId1",
                table: "CommunityPhotoUploads");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CommunityPhotoUploads");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CommunityPhotoUploads",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "CommunityPhotoComments",
                columns: table => new
                {
                    CommunityPhotoCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommunityPhotoUploadId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityPhotoComments", x => x.CommunityPhotoCommentId);
                    table.ForeignKey(
                        name: "FK_CommunityPhotoComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CommunityPhotoComments_CommunityPhotoUploads_CommunityPhotoUploadId",
                        column: x => x.CommunityPhotoUploadId,
                        principalTable: "CommunityPhotoUploads",
                        principalColumn: "CommunityPhotoUploadId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunityPhotoUploads_UserId",
                table: "CommunityPhotoUploads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityPhotoComments_CommunityPhotoUploadId",
                table: "CommunityPhotoComments",
                column: "CommunityPhotoUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityPhotoComments_UserId",
                table: "CommunityPhotoComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityPhotoUploads_AspNetUsers_UserId",
                table: "CommunityPhotoUploads",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityPhotoUploads_AspNetUsers_UserId",
                table: "CommunityPhotoUploads");

            migrationBuilder.DropTable(
                name: "CommunityPhotoComments");

            migrationBuilder.DropIndex(
                name: "IX_CommunityPhotoUploads_UserId",
                table: "CommunityPhotoUploads");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CommunityPhotoUploads",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "CommunityPhotoUploads",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommunityPhotoUploads_UserId1",
                table: "CommunityPhotoUploads",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityPhotoUploads_AspNetUsers_UserId1",
                table: "CommunityPhotoUploads",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
