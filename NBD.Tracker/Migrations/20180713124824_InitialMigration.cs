using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NBD.Tracker.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RootId = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(maxLength: 512, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    RecurrenceType = table.Column<int>(nullable: false),
                    RecurrenceValue = table.Column<long>(nullable: false),
                    Target = table.Column<int>(nullable: true),
                    Progress = table.Column<long>(nullable: false),
                    IsReached = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Goals_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PrivateId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 512, nullable: false),
                    RootId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trees_Goals_RootId",
                        column: x => x.RootId,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ParentId",
                table: "Goals",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Trees_RootId",
                table: "Trees",
                column: "RootId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trees");

            migrationBuilder.DropTable(
                name: "Goals");
        }
    }
}
