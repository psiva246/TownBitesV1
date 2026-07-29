using Microsoft.EntityFrameworkCore;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Application.Authentication.Interfaces;
using TownBites.Infrastructure.Data;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.Infrastructure.Services;

public class CustomerAddressService : ICustomerAddressService
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerAddressService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CustomerAddressResponse>> GetAllAsync(int userId)
    {
        return await _dbContext.CustomerAddresses
            .AsNoTracking()
            .Where(x => x.UserId == userId && x.IsActive)
            .OrderByDescending(x => x.IsDefault)
            .ThenBy(x => x.Name)
            .Select(x => new CustomerAddressResponse
            {
                Id = x.Id,
                Name = x.Name,
                ContactPerson = x.ContactPerson,
                PhoneNumber = x.PhoneNumber,
                AddressLine1 = x.AddressLine1,
                AddressLine2 = x.AddressLine2,
                Landmark = x.Landmark,
                City = x.City,
                State = x.State,
                Pincode = x.Pincode,
                IsDefault = x.IsDefault
            })
            .ToListAsync();
    }

    public async Task<CustomerAddressResponse> CreateAsync(int userId,CreateCustomerAddressRequest request)
    {
        if (request.IsDefault)
        {
            var existingDefaults = await _dbContext.CustomerAddresses
                .Where(x => x.UserId == userId && x.IsDefault && x.IsActive)
                .ToListAsync();

            foreach (var item in existingDefaults)
            {
                item.IsDefault = false;
            }
        }

        var entity = new CustomerAddress
        {
            UserId = userId,
            Name = request.Name,
            ContactPerson = request.ContactPerson,
            PhoneNumber = request.PhoneNumber,
            AddressLine1 = request.AddressLine1,
            AddressLine2 = request.AddressLine2,
            Landmark = request.Landmark,
            City = request.City,
            State = request.State,
            Pincode = request.Pincode,
            IsDefault = request.IsDefault,
            IsActive = true
        };

        _dbContext.CustomerAddresses.Add(entity);

        await _dbContext.SaveChangesAsync();

        return Map(entity);
    }

    public async Task<CustomerAddressResponse?> UpdateAsync(int userId, int id, UpdateCustomerAddressRequest request)
    {
        var entity = await _dbContext.CustomerAddresses
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.UserId == userId &&
                x.IsActive);

        if (entity == null)
            return null;

        if (request.IsDefault)
        {
            var existingDefaults = await _dbContext.CustomerAddresses
                .Where(x =>
                    x.UserId == userId &&
                    x.IsDefault &&
                    x.Id != id &&
                    x.IsActive)
                .ToListAsync();

            foreach (var item in existingDefaults)
            {
                item.IsDefault = false;
            }
        }

        entity.Name = request.Name;
        entity.ContactPerson = request.ContactPerson;
        entity.PhoneNumber = request.PhoneNumber;
        entity.AddressLine1 = request.AddressLine1;
        entity.AddressLine2 = request.AddressLine2;
        entity.Landmark = request.Landmark;
        entity.City = request.City;
        entity.State = request.State;
        entity.Pincode = request.Pincode;
        entity.IsDefault = request.IsDefault;

        await _dbContext.SaveChangesAsync();

        return Map(entity);
    }

    public async Task<bool> DeleteAsync(int userId, int id)
    {
        var entity = await _dbContext.CustomerAddresses
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.UserId == userId &&
                x.IsActive);

        if (entity == null)
            return false;

        entity.IsActive = false;

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> SetDefaultAsync(int userId, int id)
    {
        var address = await _dbContext.CustomerAddresses
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.UserId == userId &&
                x.IsActive);

        if (address == null)
            return false;

        var addresses = await _dbContext.CustomerAddresses
            .Where(x =>
                x.UserId == userId &&
                x.IsActive)
            .ToListAsync();

        foreach (var item in addresses)
        {
            item.IsDefault = false;
        }

        address.IsDefault = true;

        await _dbContext.SaveChangesAsync();

        return true;
    }

    private static CustomerAddressResponse Map(CustomerAddress entity)
    {
        return new CustomerAddressResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            ContactPerson = entity.ContactPerson,
            PhoneNumber = entity.PhoneNumber,
            AddressLine1 = entity.AddressLine1,
            AddressLine2 = entity.AddressLine2,
            Landmark = entity.Landmark,
            City = entity.City,
            State = entity.State,
            Pincode = entity.Pincode,
            IsDefault = entity.IsDefault
        };
    }
}