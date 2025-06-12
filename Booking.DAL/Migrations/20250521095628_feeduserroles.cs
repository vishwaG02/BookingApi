using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class feeduserroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                              
                insert into UserRole(RoleName,CreatedDate,IsActive)
                values
                ('Admin',GETDATE(),1),
                ('Customer',GETDATE(),1)
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""                              
                Delete from UserRole
                """);
        }
    }
}
