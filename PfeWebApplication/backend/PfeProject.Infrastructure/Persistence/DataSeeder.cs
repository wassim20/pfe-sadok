using PfeProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using BCrypt.Net;

namespace PfeProject.Infrastructure.Persistence
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // ✅ Créer les rôles si non existants
            if (!await _context.Roles.AnyAsync())
            {
                _context.Roles.AddRange(
                    new Role { Name = "Admin", State = true },
                    new Role { Name = "User", State = true }
                );
                await _context.SaveChangesAsync();
            }

            // ✅ Créer l’utilisateur admin par défaut
            var adminEmail = "sadokkerkeni@gmail.com";
            if (!await _context.Users.AnyAsync(u => u.Email == adminEmail))
            {
                var admin = new User
                {
                    FirstName = "Admin",
                    LastName = "System",
                    Matricule = "ADM001",
                    Email = adminEmail,
                    Password = BCrypt.Net.BCrypt.HashPassword("11099536"),
                    CreationDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    State = true
                };

                _context.Users.Add(admin);
                await _context.SaveChangesAsync();

                var adminRole = await _context.Roles.FirstAsync(r => r.Name == "Admin");

                _context.UserRoles.Add(new UserRole
                {
                    UserId = admin.Id,
                    RoleId = adminRole.Id,
                    IsActive = true,
                    AssignmentDate = DateTime.UtcNow,
                    Note = "Admin créé automatiquement",
                    AssignedById = null
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
