using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManager.Migrations
{
    public partial class CreateGymUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GymUserId",
                table: "WorkoutPlan",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GymUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspNetUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlan_GymUserId",
                table: "WorkoutPlan",
                column: "GymUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlan_GymUser_GymUserId",
                table: "WorkoutPlan",
                column: "GymUserId",
                principalTable: "GymUser",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlan_GymUser_GymUserId",
                table: "WorkoutPlan");

            migrationBuilder.DropTable(
                name: "GymUser");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPlan_GymUserId",
                table: "WorkoutPlan");

            migrationBuilder.DropColumn(
                name: "GymUserId",
                table: "WorkoutPlan");
        }
    }
}
