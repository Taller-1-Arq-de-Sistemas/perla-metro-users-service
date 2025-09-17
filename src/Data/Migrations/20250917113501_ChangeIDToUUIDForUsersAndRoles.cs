using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerlaMetroUsersService.src.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIDToUUIDForUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop FK and index that reference Users.RoleId before altering column types
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            // Explicit casts are required when converting text -> uuid
            // Roles.Id
            migrationBuilder.Sql("ALTER TABLE \"Roles\" ALTER COLUMN \"Id\" TYPE uuid USING \"Id\"::uuid;");

            // Users.Id
            migrationBuilder.Sql("ALTER TABLE \"Users\" ALTER COLUMN \"Id\" TYPE uuid USING \"Id\"::uuid;");

            // Users.RoleId
            migrationBuilder.Sql("ALTER TABLE \"Users\" ALTER COLUMN \"RoleId\" TYPE uuid USING \"RoleId\"::uuid;");

            // Recreate index and FK with new uuid types
            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop FK and index first
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            // Convert uuid back to text explicitly
            migrationBuilder.Sql("ALTER TABLE \"Users\" ALTER COLUMN \"RoleId\" TYPE text USING \"RoleId\"::text;");
            migrationBuilder.Sql("ALTER TABLE \"Users\" ALTER COLUMN \"Id\" TYPE text USING \"Id\"::text;");
            migrationBuilder.Sql("ALTER TABLE \"Roles\" ALTER COLUMN \"Id\" TYPE text USING \"Id\"::text;");

            // Recreate index and FK
            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
