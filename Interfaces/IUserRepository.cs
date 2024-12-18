using SkillSwapAPI.Models;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillSwapAPI.Interfaces
{
    public interface IUserRepository
    {
        // Obtener todos los usuarios con parámetros de búsqueda, ordenación y paginación
        Task<List<UserDTO>> GetAllAsync(UserQueryParams queryParams);

        // Obtener un usuario por ID
        Task<UserDTO?> GetByIdAsync(int id);

        // Crear un nuevo usuario
        Task<UserDTO> AddAsync(CreateUserDTO createUserDTO);

        // Actualizar un usuario existente
        Task<UserDTO?> UpdateAsync(int id, UpdateUserDTO updateUserDTO);

        // Eliminar un usuario
        Task<bool> DeleteAsync(int id);

        // Comprobar si el usuario existe
        Task<bool> UserExistsAsync(int id);
    }
}
