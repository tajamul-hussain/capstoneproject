using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOS;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
         Task<AppUser>GetUserByUseremailAsync(string email);
        Task<AppUser> GetUserByUsernameAsync(string username);
       
        // Task<IEnumerable<MemberDto>> GetMembersAsync();
        // Task<MemberDto> GetMemberAsync(string username);
       

    }
}