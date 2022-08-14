using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Migrations
{
    public partial class CreateTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Label = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTask",
                columns: table => new
                {
                    MembersId = table.Column<int>(type: "int", nullable: false),
                    TasksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTask", x => new { x.MembersId, x.TasksId });
                    table.ForeignKey(
                        name: "FK_EmployeeTask_Employees_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTask_Tasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18a7bbf3-014b-4426-bb7d-5e8ad5dd6df0",
                column: "ConcurrencyStamp",
                value: "540af40e-430a-4f1c-890a-76485b9b0cf9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f73826e9-34c2-43cd-82aa-ec181350c358",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9bce918-462e-4296-bb3b-600794a36f1f", "AQAAAAEAACcQAAAAEDTyavfKIE8NhUkeu3PIWeSDJphbOWIRLWKwby6c/Hk/I6j/ghMNgVayQcpUCjtrtg==", "e39ee94a-e624-473a-b990-296ce46c2fbd" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTask_TasksId",
                table: "EmployeeTask",
                column: "TasksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTask");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18a7bbf3-014b-4426-bb7d-5e8ad5dd6df0",
                column: "ConcurrencyStamp",
                value: "0f750a79-5592-48ba-b8cb-635974ec0d3a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f73826e9-34c2-43cd-82aa-ec181350c358",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d4a443c-9461-4759-b7c3-2ae4d5a5e20b", "AQAAAAEAACcQAAAAEMhQBA+51W0bCuK1DLq31zOsaKptpls1VfLT/FmEberw0enEE3mcqd68c6hPKBlMwQ==", "9ba08d8f-5e25-4241-9760-b84510dea125" });
        }
    }
}
