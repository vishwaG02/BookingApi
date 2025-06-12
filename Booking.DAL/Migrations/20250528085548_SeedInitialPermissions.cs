using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                Insert into Permission(Name,CreatedDate,IsActive)
                Values
                ('CreateUser',GETDATE(),1),
                ('EditUser',GETDATE(),1),
                ('GetUser',GETDATE(),1),
                ('CreateBooking',GETDATE(),1),
                ('EditBooking',GETDATE(),1),
                ('GetBooking',GETDATE(),1),
                ('CancelBooking',GETDATE(),1),
                ('DeleteUser',GETDATE(),1),
                ('Dashboard',GETDATE(),1)
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                Delete from Permission
                """);
        }
    }
}
