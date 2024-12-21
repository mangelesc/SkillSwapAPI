using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Data;
using SkillSwapAPI.Models;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.Helpers;
using SkillSwapAPI.Mappers;  // Importar el namespace de los mappers
using System.Threading.Tasks;
using System.Linq;

namespace SkillSwapAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SkillSwapContext _context;

        public UserRepository(SkillSwapContext context)
        {
            _context = context;
        }

        public async Task<List<UserDTO>> GetAllAsync(UserQueryParams queryParams)
        {
            IQueryable<User> users = _context.Users;

            if (!string.IsNullOrEmpty(queryParams.SearchTerm))
            {
                users = users.Where(u => u.Name.Contains(queryParams.SearchTerm) || u.Email.Contains(queryParams.SearchTerm));
            }

            if (queryParams.OrderBy == "name")
            {
                users = queryParams.AscendingOrder ? users.OrderBy(u => u.Name) : users.OrderByDescending(u => u.Name);
            }
            else if (queryParams.OrderBy == "email")
            {
                users = queryParams.AscendingOrder ? users.OrderBy(u => u.Email) : users.OrderByDescending(u => u.Email);
            }

            if (queryParams.PageNumber > 0 && queryParams.PageSize > 0)
            {
                users = users.Skip((queryParams.PageNumber - 1) * queryParams.PageSize).Take(queryParams.PageSize);
            }

            var result = await users.ToListAsync();

            return result.Select(user => user.ToUserDTO()).ToList();
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user?.ToUserDTO();
        }

        public async Task<UserDTO> AddAsync(CreateUserDTO createUserDTO)
        {
            var user = createUserDTO.ToUser();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.ToUserDTO();
        }

        public async Task<UserDTO?> UpdateAsync(int id, UpdateUserDTO updateUserDTO)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user = updateUserDTO.ToUser(user);
            await _context.SaveChangesAsync();

            return user.ToUserDTO();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }
    }
}
