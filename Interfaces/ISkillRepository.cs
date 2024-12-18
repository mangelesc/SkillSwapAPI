using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillSwapAPI.Models;


namespace SkillSwapAPI.Interfaces
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAllAsync(); 
        Task<Skill?> GetByIdAsync(int id); 
        Task<Skill> CreateAsync(Skill Skill); 
        // Task<Skill?> UpdateAsync (int id, UpdateSkillRequestDto SkillDto); 
        // Task<Skill?> DeleteAsync (int id); 
        // Task<bool> SkillExist(int id);
    }
}