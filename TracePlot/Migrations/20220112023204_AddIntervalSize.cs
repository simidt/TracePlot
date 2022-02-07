using Microsoft.EntityFrameworkCore.Migrations;

namespace TracePlot.Migrations
{
    public partial class AddIntervalSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntervalSize",
                table: "TraceRouteCollections",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntervalSize",
                table: "TraceRouteCollections");
        }
    }
}
