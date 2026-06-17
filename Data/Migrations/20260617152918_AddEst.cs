using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Categorias",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Categorias");
        }
    }
}
