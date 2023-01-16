using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManager.Migrations
{
    public partial class AddWorkoutPlanUsersRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlan_GymUser_GymUserId",
                table: "WorkoutPlan");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPlan_GymUserId",
                table: "WorkoutPlan");

            migrationBuilder.DropColumn(
                name: "GymUserId",
                table: "WorkoutPlan");

            migrationBuilder.CreateTable(
                name: "GymUserWorkoutPlan",
                columns: table => new
                {
                    GymUsersId = table.Column<int>(type: "int", nullable: false),
                    WorkoutPlansId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymUserWorkoutPlan", x => new { x.GymUsersId, x.WorkoutPlansId });
                    table.ForeignKey(
                        name: "FK_GymUserWorkoutPlan_GymUser_GymUsersId",
                        column: x => x.GymUsersId,
                        principalTable: "GymUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GymUserWorkoutPlan_WorkoutPlan_WorkoutPlansId",
                        column: x => x.WorkoutPlansId,
                        principalTable: "WorkoutPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GymUserWorkoutPlan_WorkoutPlansId",
                table: "GymUserWorkoutPlan",
                column: "WorkoutPlansId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymUserWorkoutPlan");

            migrationBuilder.AddColumn<int>(
                name: "GymUserId",
                table: "WorkoutPlan",
                type: "int",
                nullable: true);

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
    }
}
