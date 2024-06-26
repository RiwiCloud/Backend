using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos;
using Backend.Models;

namespace Backend.Services.UserRepositories
{
    public interface IUserRepository
    {
     Task<User> AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UserExistsAsync(string email);
        Task<User> RegisterUserAsync(UserRegistrationDto userRegistrationDto);
    }
}