using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenSeedCREdev.Data.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "CommunityPhotoUploads",
                columns: table => new
                {
                    UploadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityPhotoUploads", x => x.UploadId);
                    table.ForeignKey(
                        name: "FK_CommunityPhotoUploads_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryCompanies",
                columns: table => new
                {
                    DeliveryCompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryCompanies", x => x.DeliveryCompanyId);
                });

            migrationBuilder.CreateTable(
                name: "PhotoChallenges",
                columns: table => new
                {
                    ChallengeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoChallenges", x => x.ChallengeId);
                    table.ForeignKey(
                        name: "FK_PhotoChallenges_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryCompanies_DeliveryCompanyId",
                        column: x => x.DeliveryCompanyId,
                        principalTable: "DeliveryCompanies",
                        principalColumn: "DeliveryCompanyId");
                });

            migrationBuilder.CreateTable(
                name: "ChallengeOptions",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    PhotoChallengeChallengeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeOptions", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_ChallengeOptions_PhotoChallenges_PhotoChallengeChallengeId",
                        column: x => x.PhotoChallengeChallengeId,
                        principalTable: "PhotoChallenges",
                        principalColumn: "ChallengeId");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PhotoChallengeParticipations",
                columns: table => new
                {
                    ParticipationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SelectedOptionId = table.Column<int>(type: "int", nullable: false),
                    ParticipationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhotoChallengeChallengeId = table.Column<int>(type: "int", nullable: true),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoChallengeParticipations", x => x.ParticipationId);
                    table.ForeignKey(
                        name: "FK_PhotoChallengeParticipations_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhotoChallengeParticipations_ChallengeOptions_SelectedOptionId",
                        column: x => x.SelectedOptionId,
                        principalTable: "ChallengeOptions",
                        principalColumn: "OptionId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PhotoChallengeParticipations_PhotoChallenges_PhotoChallengeChallengeId",
                        column: x => x.PhotoChallengeChallengeId,
                        principalTable: "PhotoChallenges",
                        principalColumn: "ChallengeId");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Todas as variedades de flores", "Flores" },
                    { 2, "Plantas para decoração de ambientes", "Plantas" },
                    { 3, "Fertilizantes para as plantas e flores", "Fertilizantes" },
                    { 4, "Vasos para plantas e flores", "Vasos" },
                    { 5, "Acessórios para jardinagem", "Acessórios" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "Rosa vermelha clássica", "Rosa", 5.99m, 100 },
                    { 2, 2, "Orquídea branca", "Orquídea", 15.99m, 50 },
                    { 3, 3, "Planta ornamental para ambientes internos", "Samambaia", 25.99m, 30 },
                    { 4, 2, "Planta do deserto", "Cacto", 4.99m, 30 },
                    { 5, 3, "Fertilizante para plantas", "Fertilizante NPK", 10.99m, 200 },
                    { 6, 3, "Fertilizante para flores", "Fertilizante JBL", 10.99m, 200 },
                    { 7, 4, "Vaso de cerâmica para plantas", "Vaso de cerâmica", 7.99m, 150 },
                    { 8, 4, "Vaso de plastico para plantas", "Vaso de PLASTICO", 3.99m, 150 },
                    { 9, 5, "Luvas para jardinagem", "Luvas de jardinagem", 3.99m, 100 },
                    { 10, 5, "Luvas para jardinagem", "Pinça", 8.99m, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeOptions_PhotoChallengeChallengeId",
                table: "ChallengeOptions",
                column: "PhotoChallengeChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityPhotoUploads_UserId1",
                table: "CommunityPhotoUploads",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryCompanyId",
                table: "Orders",
                column: "DeliveryCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoChallengeParticipations_PhotoChallengeChallengeId",
                table: "PhotoChallengeParticipations",
                column: "PhotoChallengeChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoChallengeParticipations_SelectedOptionId",
                table: "PhotoChallengeParticipations",
                column: "SelectedOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoChallengeParticipations_UserId1",
                table: "PhotoChallengeParticipations",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoChallenges_UserId1",
                table: "PhotoChallenges",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunityPhotoUploads");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "PhotoChallengeParticipations");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ChallengeOptions");

            migrationBuilder.DropTable(
                name: "DeliveryCompanies");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "PhotoChallenges");
        }
    }
}
