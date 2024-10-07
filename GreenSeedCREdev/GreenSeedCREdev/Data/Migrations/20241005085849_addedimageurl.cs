using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenSeedCREdev.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedimageurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChallengeOptions_PhotoChallenges_PhotoChallengeChallengeId",
                table: "ChallengeOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoChallengeParticipations_PhotoChallenges_PhotoChallengeChallengeId",
                table: "PhotoChallengeParticipations");

            migrationBuilder.DropIndex(
                name: "IX_ChallengeOptions_PhotoChallengeChallengeId",
                table: "ChallengeOptions");

            migrationBuilder.DropColumn(
                name: "PhotoChallengeChallengeId",
                table: "ChallengeOptions");

            migrationBuilder.RenameColumn(
                name: "ChallengeId",
                table: "PhotoChallenges",
                newName: "PhotoChallengeId");

            migrationBuilder.RenameColumn(
                name: "PhotoChallengeChallengeId",
                table: "PhotoChallengeParticipations",
                newName: "PhotoChallengeId");

            migrationBuilder.RenameColumn(
                name: "ParticipationId",
                table: "PhotoChallengeParticipations",
                newName: "PhotoChallengeParticipationId");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoChallengeParticipations_PhotoChallengeChallengeId",
                table: "PhotoChallengeParticipations",
                newName: "IX_PhotoChallengeParticipations_PhotoChallengeId");

            migrationBuilder.RenameColumn(
                name: "UploadId",
                table: "CommunityPhotoUploads",
                newName: "CommunityPhotoUploadId");

            migrationBuilder.RenameColumn(
                name: "ChallengeId",
                table: "ChallengeOptions",
                newName: "PhotoChallengeId");

            migrationBuilder.RenameColumn(
                name: "OptionId",
                table: "ChallengeOptions",
                newName: "ChallengeOptionId");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DeliveryCompanies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "OptionText",
                table: "ChallengeOptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://via.placeholder.com/150");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://via.placeholder.com/150");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://via.placeholder.com/150");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://via.placeholder.com/150");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://via.placeholder.com/150");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://via.placeholder.com/150");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                column: "ImageUrl",
                value: "https://via.placeholder.com/150");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                column: "ImageUrl",
                value: "https://via.placeholder.com/150");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                column: "ImageUrl",
                value: "https://via.placeholder.com/150");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                column: "ImageUrl",
                value: "https://via.placeholder.com/150");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeOptions_PhotoChallengeId",
                table: "ChallengeOptions",
                column: "PhotoChallengeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChallengeOptions_PhotoChallenges_PhotoChallengeId",
                table: "ChallengeOptions",
                column: "PhotoChallengeId",
                principalTable: "PhotoChallenges",
                principalColumn: "PhotoChallengeId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoChallengeParticipations_PhotoChallenges_PhotoChallengeId",
                table: "PhotoChallengeParticipations",
                column: "PhotoChallengeId",
                principalTable: "PhotoChallenges",
                principalColumn: "PhotoChallengeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChallengeOptions_PhotoChallenges_PhotoChallengeId",
                table: "ChallengeOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoChallengeParticipations_PhotoChallenges_PhotoChallengeId",
                table: "PhotoChallengeParticipations");

            migrationBuilder.DropIndex(
                name: "IX_ChallengeOptions_PhotoChallengeId",
                table: "ChallengeOptions");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "PhotoChallengeId",
                table: "PhotoChallenges",
                newName: "ChallengeId");

            migrationBuilder.RenameColumn(
                name: "PhotoChallengeId",
                table: "PhotoChallengeParticipations",
                newName: "PhotoChallengeChallengeId");

            migrationBuilder.RenameColumn(
                name: "PhotoChallengeParticipationId",
                table: "PhotoChallengeParticipations",
                newName: "ParticipationId");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoChallengeParticipations_PhotoChallengeId",
                table: "PhotoChallengeParticipations",
                newName: "IX_PhotoChallengeParticipations_PhotoChallengeChallengeId");

            migrationBuilder.RenameColumn(
                name: "CommunityPhotoUploadId",
                table: "CommunityPhotoUploads",
                newName: "UploadId");

            migrationBuilder.RenameColumn(
                name: "PhotoChallengeId",
                table: "ChallengeOptions",
                newName: "ChallengeId");

            migrationBuilder.RenameColumn(
                name: "ChallengeOptionId",
                table: "ChallengeOptions",
                newName: "OptionId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DeliveryCompanies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OptionText",
                table: "ChallengeOptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhotoChallengeChallengeId",
                table: "ChallengeOptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeOptions_PhotoChallengeChallengeId",
                table: "ChallengeOptions",
                column: "PhotoChallengeChallengeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChallengeOptions_PhotoChallenges_PhotoChallengeChallengeId",
                table: "ChallengeOptions",
                column: "PhotoChallengeChallengeId",
                principalTable: "PhotoChallenges",
                principalColumn: "ChallengeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoChallengeParticipations_PhotoChallenges_PhotoChallengeChallengeId",
                table: "PhotoChallengeParticipations",
                column: "PhotoChallengeChallengeId",
                principalTable: "PhotoChallenges",
                principalColumn: "ChallengeId");
        }
    }
}
