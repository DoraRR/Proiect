using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManager.Migrations
{
    public partial class CreateTrainersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "WorkoutPlan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Trainer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AspNetUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlan_TrainerId",
                table: "WorkoutPlan",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlan_Trainer_TrainerId",
                table: "WorkoutPlan",
                column: "TrainerId",
                principalTable: "Trainer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlan_Trainer_TrainerId",
                table: "WorkoutPlan");

            migrationBuilder.DropTable(
                name: "Trainer");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPlan_TrainerId",
                table: "WorkoutPlan");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "WorkoutPlan");
        }
    }
}
