using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Dtos;
using Backeng.Data;
using Backeng.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Services.UserRepositories
{
    public class UserRepository : IUserRepository
{
    private readonly BaseContext _context;
    private readonly IMapper _mapper;

    public UserRepository(BaseContext context)
    {
        _context = context;
    }

     public async Task<User> AddUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User> RegisterUserAsync(UserRegistrationDto userRegistrationDto)
    {
        if (await UserExistsAsync(userRegistrationDto.Email))
        {
            throw new Exception("User already exists");
        }

        var user = _mapper.Map<User>(userRegistrationDto);
        user.Password = user.Password; // Usar BCrypt para hashear la contraseña
        user.DateCreated = DateTime.Now;
        user.Status = "Active";

        return await AddUserAsync(user);
    }

        Task IUserRepository.RegisterUserAsync(UserRegistrationDto userRegistrationDto)
        {
            throw new NotImplementedException();
        }
    }

}