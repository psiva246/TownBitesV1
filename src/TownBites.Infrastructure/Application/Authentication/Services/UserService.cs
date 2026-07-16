using Microsoft.EntityFrameworkCore;
using TownBites.Domain.Entities;
using TownBites.Shared.Enums;
using TownBites.Infrastructure.Application.Authentication.Interfaces;
using TownBites.Infrastructure.Data;

namespace TownBites.Infrastructure.Application.Authentication.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPasswordHasherService _passwordHasher;

    public UserService(
        ApplicationDbContext dbContext,
        IPasswordHasherService passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(x =>
                x.PhoneNumber == phoneNumber &&
                !x.IsDeleted);
    }

    public async Task<bool> PhoneNumberExistsAsync(string phoneNumber)
    {
        return await _dbContext.Users
            .AnyAsync(x =>
                x.PhoneNumber == phoneNumber &&
                !x.IsDeleted);
    }

    public async Task<User> RegisterCustomerAsync(
        string name,
        string phoneNumber,
        string password)
    {
        if (await PhoneNumberExistsAsync(phoneNumber))
        {
            throw new InvalidOperationException(
                "Phone number is already registered.");
        }

        var user = new User
        {
            Name = name,
            PhoneNumber = phoneNumber,
            PasswordHash = _passwordHasher.HashPassword(password),
            Role = UserRole.Customer,
            IsActive = true
        };

        _dbContext.Users.Add(user);

        await _dbContext.SaveChangesAsync();

        return user;
    }
}