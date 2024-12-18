using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Data;
using SkillSwapAPI.Models;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.Helpers;
using System.Threading.Tasks;
using System.Linq;

namespace SkillSwapAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SkillSwapContext _context;

        public UserRepository(SkillSwapContext context)
        {
            _context = context;
        }

        // Obtener todos los usuarios con filtros y ordenación
        public async Task<List<UserDTO>> GetAllAsync(UserQueryParams queryParams)
        {
            IQueryable<User> users = _context.Users;

            // Filtro por nombre o correo electrónico
            if (!string.IsNullOrEmpty(queryParams.SearchTerm))
            {
                users = users.Where(u => u.Name.Contains(queryParams.SearchTerm) || u.Email.Contains(queryParams.SearchTerm));
            }

            // Ordenación
            if (queryParams.OrderBy == "name")
            {
                users = queryParams.AscendingOrder ? users.OrderBy(u => u.Name) : users.OrderByDescending(u => u.Name);
            }
            else if (queryParams.OrderBy == "email")
            {
                users = queryParams.AscendingOrder ? users.OrderBy(u => u.Email) : users.OrderByDescending(u => u.Email);
            }

            // Paginación
            if (queryParams.PageNumber > 0 && queryParams.PageSize > 0)
            {
                users = users.Skip((queryParams.PageNumber - 1) * queryParams.PageSize).Take(queryParams.PageSize);
            }

            // Ejecutar la consulta
            var result = await users.ToListAsync();

            // Convertir a DTO
            return result.Select(user => new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture
            }).ToList();
        }

        // Obtener un usuario por ID
        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture
            };
        }

        // Crear un nuevo usuario
        public async Task<UserDTO> AddAsync(CreateUserDTO createUserDTO)
        {
            var user = new User
            {
                Name = createUserDTO.Name,
                Email = createUserDTO.Email,
                ProfilePicture = createUserDTO.ProfilePicture
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture
            };
        }

        // Actualizar un usuario
        public async Task<UserDTO?> UpdateAsync(int id, UpdateUserDTO updateUserDTO)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.Name = updateUserDTO.Name;
            user.Email = updateUserDTO.Email;
            user.ProfilePicture = updateUserDTO.ProfilePicture;

            await _context.SaveChangesAsync();

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture
            };
        }

        // Eliminar un usuario
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Comprobar si el usuario existe
        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }
    }
}
