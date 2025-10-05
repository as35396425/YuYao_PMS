using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcProgram.Migrations
{
    /// <inheritdoc />
    public partial class addDbSettoContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReqestID = table.Column<string>(type: "TEXT", nullable: true),
                    ResponseID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_ReqestID",
                        column: x => x.ReqestID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_ResponseID",
                        column: x => x.ResponseID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReqestID",
                table: "Orders",
                column: "ReqestID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ResponseID",
                table: "Orders",
                column: "ResponseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
