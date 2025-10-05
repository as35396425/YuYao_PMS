using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcProgram.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderItemsExchangestoContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    exchangeID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReqestID = table.Column<int>(type: "INTEGER", nullable: true),
                    ResponseID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.exchangeID);
                    table.ForeignKey(
                        name: "FK_Exchanges_Products_ReqestID",
                        column: x => x.ReqestID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_Exchanges_Products_ResponseID",
                        column: x => x.ResponseID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    orderItemID = table.Column<Guid>(type: "TEXT", nullable: false),
                    exchangeID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.orderItemID);
                    table.ForeignKey(
                        name: "FK_OrderItems_Exchanges_exchangeID",
                        column: x => x.exchangeID,
                        principalTable: "Exchanges",
                        principalColumn: "exchangeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_ReqestID",
                table: "Exchanges",
                column: "ReqestID");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_ResponseID",
                table: "Exchanges",
                column: "ResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_exchangeID",
                table: "OrderItems",
                column: "exchangeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Exchanges");
        }
    }
}
