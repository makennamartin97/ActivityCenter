using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharpexam.Migrations
{
    public partial class csharpexam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "gatherings",
                columns: table => new
                {
                    gatheringID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date = table.Column<DateTime>(nullable: false),
                    time = table.Column<DateTime>(nullable: false),
                    duration = table.Column<int>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    userID = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gatherings", x => x.gatheringID);
                    table.ForeignKey(
                        name: "FK_gatherings_users_userID",
                        column: x => x.userID,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "associations",
                columns: table => new
                {
                    associationID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userID = table.Column<int>(nullable: false),
                    gatheringID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_associations", x => x.associationID);
                    table.ForeignKey(
                        name: "FK_associations_gatherings_gatheringID",
                        column: x => x.gatheringID,
                        principalTable: "gatherings",
                        principalColumn: "gatheringID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_associations_users_userID",
                        column: x => x.userID,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_associations_gatheringID",
                table: "associations",
                column: "gatheringID");

            migrationBuilder.CreateIndex(
                name: "IX_associations_userID",
                table: "associations",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_gatherings_userID",
                table: "gatherings",
                column: "userID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "associations");

            migrationBuilder.DropTable(
                name: "gatherings");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
