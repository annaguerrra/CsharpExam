using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CsharpExam.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Trips_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Points_Trips_TripID",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Points_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripPoints",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PointID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPoints", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TripPoints_Points_PointID",
                        column: x => x.PointID,
                        principalTable: "Points",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TripPoints_Trips_TripID",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TripPoints_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Points_TripID",
                table: "Points",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_Points_UserID",
                table: "Points",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TripPoints_PointID",
                table: "TripPoints",
                column: "PointID");

            migrationBuilder.CreateIndex(
                name: "IX_TripPoints_TripID",
                table: "TripPoints",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_TripPoints_UserID",
                table: "TripPoints",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_UserID",
                table: "Trips",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripPoints");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
