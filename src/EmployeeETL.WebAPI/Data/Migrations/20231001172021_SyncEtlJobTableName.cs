using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeETL.WebAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class SyncEtlJobTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs");

            migrationBuilder.RenameTable(
                name: "Jobs",
                newName: "EtlJobs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EtlJobs",
                table: "EtlJobs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EtlJobs",
                table: "EtlJobs");

            migrationBuilder.RenameTable(
                name: "EtlJobs",
                newName: "Jobs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs",
                column: "Id");
        }
    }
}
