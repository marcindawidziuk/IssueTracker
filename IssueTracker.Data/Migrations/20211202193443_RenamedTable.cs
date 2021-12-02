using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Data.Migrations
{
    public partial class RenamedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExercisePlanMappings_Issues_IssueId",
                table: "ExercisePlanMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_ExercisePlanMappings_Labels_LabelId",
                table: "ExercisePlanMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExercisePlanMappings",
                table: "ExercisePlanMappings");

            migrationBuilder.RenameTable(
                name: "ExercisePlanMappings",
                newName: "LabelIssueMappings");

            migrationBuilder.RenameIndex(
                name: "IX_ExercisePlanMappings_LabelId",
                table: "LabelIssueMappings",
                newName: "IX_LabelIssueMappings_LabelId");

            migrationBuilder.RenameIndex(
                name: "IX_ExercisePlanMappings_IssueId",
                table: "LabelIssueMappings",
                newName: "IX_LabelIssueMappings_IssueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabelIssueMappings",
                table: "LabelIssueMappings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LabelIssueMappings_Issues_IssueId",
                table: "LabelIssueMappings",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LabelIssueMappings_Labels_LabelId",
                table: "LabelIssueMappings",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabelIssueMappings_Issues_IssueId",
                table: "LabelIssueMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_LabelIssueMappings_Labels_LabelId",
                table: "LabelIssueMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabelIssueMappings",
                table: "LabelIssueMappings");

            migrationBuilder.RenameTable(
                name: "LabelIssueMappings",
                newName: "ExercisePlanMappings");

            migrationBuilder.RenameIndex(
                name: "IX_LabelIssueMappings_LabelId",
                table: "ExercisePlanMappings",
                newName: "IX_ExercisePlanMappings_LabelId");

            migrationBuilder.RenameIndex(
                name: "IX_LabelIssueMappings_IssueId",
                table: "ExercisePlanMappings",
                newName: "IX_ExercisePlanMappings_IssueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExercisePlanMappings",
                table: "ExercisePlanMappings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExercisePlanMappings_Issues_IssueId",
                table: "ExercisePlanMappings",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExercisePlanMappings_Labels_LabelId",
                table: "ExercisePlanMappings",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
