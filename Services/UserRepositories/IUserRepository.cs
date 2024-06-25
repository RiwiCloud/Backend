using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos;
using Backeng.Models;

namespace Backend.Services.UserRepositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task RegisterUserAsync(UserRegistrationDto userRegistrationDto);
        Task<bool> UserExistsAsync(string email);
    }
}