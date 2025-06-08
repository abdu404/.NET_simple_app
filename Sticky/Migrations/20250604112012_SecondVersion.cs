using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sticky.Migrations
{
    /// <inheritdoc />
    public partial class SecondVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
