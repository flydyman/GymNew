using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeWork.Migrations
{
    public partial class Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainGroups",
                table: "TrainGroups",
                columns: new[] { "Id_Client", "Id_Training" });

            migrationBuilder.CreateIndex(
                name: "IX_TrainGroups_Id_Training",
                table: "TrainGroups",
                column: "Id_Training");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainGroups_Clients_Id_Client",
                table: "TrainGroups",
                column: "Id_Client",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainGroups_Trainings_Id_Training",
                table: "TrainGroups",
                column: "Id_Training",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainGroups_Clients_Id_Client",
                table: "TrainGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainGroups_Trainings_Id_Training",
                table: "TrainGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainGroups",
                table: "TrainGroups");

            migrationBuilder.DropIndex(
                name: "IX_TrainGroups_Id_Training",
                table: "TrainGroups");
        }
    }
}
