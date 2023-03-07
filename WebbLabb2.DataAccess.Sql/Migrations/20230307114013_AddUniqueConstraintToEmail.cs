using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbLabb2.DataAccess.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_CustomerModel_Email",
                table: "CustomerModel",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_CustomerModel_Email",
                table: "CustomerModel");
        }
    }
}
