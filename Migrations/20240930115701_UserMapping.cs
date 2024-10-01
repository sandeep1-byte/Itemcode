using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Item_Code_management_System.Migrations
{
    /// <inheritdoc />
    public partial class UserMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemCodeMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCodeId = table.Column<int>(type: "int", nullable: true),
                    UserItemCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCodeMappings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCodeMappings");
        }
    }
}
