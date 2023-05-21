using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessageBroker.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMessageKeyNameToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Messages",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Messages",
                newName: "Key");
        }
    }
}
