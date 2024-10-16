using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenSeed.Data.Migrations
{
    /// <inheritdoc />
    public partial class comunidade1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ImagePosts");

            migrationBuilder.CreateTable(
                name: "CommunityPhotoUploads",
                columns: table => new
                {
                    CommunityPhotoUploadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityPhotoUploads", x => x.CommunityPhotoUploadId);
                    table.ForeignKey(
                        name: "FK_CommunityPhotoUploads_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

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
                name: "IX_CommunityPhotoComments_CommunityPhotoUploadId",
                table: "CommunityPhotoComments",
                column: "CommunityPhotoUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityPhotoComments_UserId",
                table: "CommunityPhotoComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityPhotoUploads_UserId",
                table: "CommunityPhotoUploads",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunityPhotoComments");

            migrationBuilder.DropTable(
                name: "CommunityPhotoUploads");

            migrationBuilder.CreateTable(
                name: "ImagePosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagePosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagePosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Comments_ImagePosts_ImagePostId",
                        column: x => x.ImagePostId,
                        principalTable: "ImagePosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ImagePostId",
                table: "Comments",
                column: "ImagePostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagePosts_UserId",
                table: "ImagePosts",
                column: "UserId");
        }
    }
}
