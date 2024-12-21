using SkillSwapAPI.DTOs;
using SkillSwapAPI.Models;

namespace SkillSwapAPI.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture
            };
        }

        public static User ToUser(this CreateUserDTO createUserDTO)
        {
            return new User
            {
                Name = createUserDTO.Name,
                Email = createUserDTO.Email,
                ProfilePicture = createUserDTO.ProfilePicture
            };
        }

        public static User ToUser(this UpdateUserDTO updateUserDTO, User user)
        {
            user.Name = updateUserDTO.Name;
            user.Email = updateUserDTO.Email;
            user.ProfilePicture = updateUserDTO.ProfilePicture;
            return user;
        }
    }
}
