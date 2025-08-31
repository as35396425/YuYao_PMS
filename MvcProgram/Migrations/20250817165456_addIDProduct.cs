using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcProgram.Migrations
{
    /// <inheritdoc />
    public partial class addIDProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Products",
                newName: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Products",
                newName: "ID");
        }
    }
}
