using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsightLogs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddErrorMetadataJsonToErrorLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ErrorMetadataJson",
                table: "ErrorLogs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReportedBy",
                table: "ErrorLogs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorMetadataJson",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "ReportedBy",
                table: "ErrorLogs");
        }
    }
}
