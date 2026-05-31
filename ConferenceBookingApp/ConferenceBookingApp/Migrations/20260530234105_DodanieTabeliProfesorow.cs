using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceBookingApp.Migrations
{
    /// <inheritdoc />
    public partial class DodanieTabeliProfesorow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ProfessorId",
                table: "Bookings",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Professors_ProfessorId",
                table: "Bookings",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Professors_ProfessorId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ProfessorId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Bookings");
        }
    }
}
