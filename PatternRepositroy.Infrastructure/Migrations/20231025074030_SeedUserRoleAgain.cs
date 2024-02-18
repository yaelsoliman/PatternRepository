using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatternRepositroy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserRoleAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO [dbo].[AspNetRoles] VALUES ('{Guid.NewGuid()}', 'User', 'USER', '{Guid.NewGuid()}')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM [dbo].[AspNetRoles]");

        }
    }
}
