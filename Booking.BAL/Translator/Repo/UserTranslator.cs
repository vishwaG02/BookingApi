using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Booking.BAL.Constant;
using Booking.BAL.Translator.Interface;
using Booking.BAL.ViewModel.UserProfile;
using Booking.DAL.Entities;

namespace Booking.BAL.Translator.Repo
{
    public class UserTranslator : IUserTranslator
    {
        public UserProfile Translate(UserProfileViewModel model)
        {
            return new UserProfile
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedDate = DateTime.UtcNow,
                Email = model.Email,
                UserRoleMappings = new List<UserRoleMapping?>()
                {
                    new UserRoleMapping
                    {
                        RoleId = model.RoleId,
                        IsActive = true
                    }
                },
                IsActive = true
            };
        }

        public List<Claim> TranslateClaims(UserProfile user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            // Dynamically add permission claims
            if (user.UserRoleMappings != null)
            {
                foreach (var userRoleMapping in user.UserRoleMappings)
                {
                    // add Role Claim
                    claims.Add(new Claim(ClaimTypes.Role, userRoleMapping.UserRole.RoleName));

                    if (userRoleMapping.UserRole?.RolePermissions != null)
                    {
                        foreach (var rolePermission in userRoleMapping.UserRole.RolePermissions)
                        {
                            if (rolePermission.Permission != null && !string.IsNullOrEmpty(rolePermission.Permission.Name))
                            {
                                claims.Add(new Claim(GlobalConstant.PermissionClainName, rolePermission.Permission.Name));
                            }
                        }
                    }
                }
            }

            return claims;
        }
    }
}
