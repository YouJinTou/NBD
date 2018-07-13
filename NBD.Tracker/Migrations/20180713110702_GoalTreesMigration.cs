using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NBD.Tracker.Migrations
{
    public partial class GoalTreesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Goals",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Trees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    PublicHash = table.Column<string>(nullable: false),
                    PrivateHash = table.Column<string>(nullable: false),
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
                name: "IX_Trees_RootId",
                table: "Trees",
                column: "RootId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trees");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Goals",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 512);
        }
    }
}
