using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class removeGuidColumnsFromBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserRoleMapping");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "UserRoleMapping");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "UserProfile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "UserRoleMapping",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "UserRoleMapping",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "UserRole",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "UserRole",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "UserProfile",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "UserProfile",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
