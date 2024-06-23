using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Management.Migrations
{
    /// <inheritdoc />
    public partial class TeacherId_To_Subject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "AppSubjects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppSubjects_TeacherId",
                table: "AppSubjects",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSubjects_AppTeachers_TeacherId",
                table: "AppSubjects",
                column: "TeacherId",
                principalTable: "AppTeachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSubjects_AppTeachers_TeacherId",
                table: "AppSubjects");

            migrationBuilder.DropIndex(
                name: "IX_AppSubjects_TeacherId",
                table: "AppSubjects");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "AppSubjects");
        }
    }
}
