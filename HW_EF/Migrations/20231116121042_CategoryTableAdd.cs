using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HW_EF.Migrations
{
    /// <inheritdoc />
    public partial class CategoryTableAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Posts",
                newName: "CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId1",
                table: "Posts",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryId1",
                table: "Posts",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryId1",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CategoryId1",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Posts",
                newName: "Category");
        }
    }
}
