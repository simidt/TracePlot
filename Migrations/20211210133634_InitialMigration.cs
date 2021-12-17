using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TracePlot.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TraceRouteCollections",
                columns: table => new
                {
                    TraceRouteCollectionID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TargetHostname = table.Column<string>(type: "TEXT", nullable: true),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NumberOfLoops = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_TraceRouteCollections", x => x.TraceRouteCollectionID));

            migrationBuilder.CreateTable(
                name: "Hops",
                columns: table => new
                {
                    HopId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    HopNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    MedianReplyTime = table.Column<double>(type: "REAL", nullable: false),
                    AverageReplyTime = table.Column<double>(type: "REAL", nullable: false),
                    MinimumReplyTime = table.Column<long>(type: "INTEGER", nullable: false),
                    MaximumReplyTime = table.Column<long>(type: "INTEGER", nullable: false),
                    LowerQuartile = table.Column<double>(type: "REAL", nullable: false),
                    HigherQuartile = table.Column<double>(type: "REAL", nullable: false),
                    ParentID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TraceRouteCollectionID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hops", x => x.HopId);
                    table.ForeignKey(
                        name: "FK_Hops_TraceRouteCollections_TraceRouteCollectionID",
                        column: x => x.TraceRouteCollectionID,
                        principalTable: "TraceRouteCollections",
                        principalColumn: "TraceRouteCollectionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReplyTimes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Time = table.Column<long>(type: "INTEGER", nullable: false),
                    HopID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyTimes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReplyTimes_Hops_HopID",
                        column: x => x.HopID,
                        principalTable: "Hops",
                        principalColumn: "HopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hops_TraceRouteCollectionID",
                table: "Hops",
                column: "TraceRouteCollectionID");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyTimes_HopID",
                table: "ReplyTimes",
                column: "HopID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplyTimes");

            migrationBuilder.DropTable(
                name: "Hops");

            migrationBuilder.DropTable(
                name: "TraceRouteCollections");
        }
    }
}
