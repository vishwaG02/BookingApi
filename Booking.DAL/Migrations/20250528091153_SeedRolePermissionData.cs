using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolePermissionData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                
                declare @table table(
                 RoleId int,
                 PermissionId int,
                 CreatedDate datetime,
                 IsActive bit
                )
                INSERT INTO @table (RoleId, PermissionId, CreatedDate, IsActive)
                SELECT r.RoleId, p.Id, GETDATE(), 1
                FROM UserRole r
                JOIN Permission p ON 1 = 1
                WHERE r.RoleName = 'Admin';

                INSERT INTO @table (RoleId, PermissionId, CreatedDate, IsActive)
                SELECT r.RoleId, p.Id, GETDATE(), 1
                FROM UserRole r
                JOIN Permission p ON p.Name IN ('CreateBooking', 'EditBooking', 'GetBooking', 'Dashboard')
                WHERE r.RoleName = 'Customer';

                Merge into RolePermission as trg
                Using(
                Select * from @table
                ) as src
                on src.RoleId = trg.RoleId
                and src.PermissionId = trg.PermissionId
                and src.IsActive = trg.IsActive
                when not matched by target then Insert(RoleId,PermissionId,CreatedDate,IsActive)
                Values
                (src.RoleId,src.PermissionId,src.CreatedDate,src.IsActive);
                
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                
                Delete from RolePermission
                """);
        }
    }
}
