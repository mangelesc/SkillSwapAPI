using SkillSwapAPI.DTOs;
using SkillSwapAPI.Models;

namespace SkillSwapAPI.Helpers
{
    public static class UserMapper
    {
        // Convertir un User a UserDTO
        public static UserDTO ToUserDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture
            };
        }

        // Convertir CreateUserDTO a User
        public static User ToUserFromCreateDto(CreateUserDTO createUserDTO)
        {
            return new User
            {
                Name = createUserDTO.Name,
                Email = createUserDTO.Email,
                ProfilePicture = createUserDTO.ProfilePicture
            };
        }

        // Convertir UpdateUserDTO a User (para actualizaciones)
        public static void UpdateUserFromDto(User user, UpdateUserDTO updateUserDTO)
        {
            user.Name = updateUserDTO.Name;
            user.Email = updateUserDTO.Email;
            user.ProfilePicture = updateUserDTO.ProfilePicture;
        }
    }
}
