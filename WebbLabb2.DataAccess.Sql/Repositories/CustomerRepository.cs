using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebbLabb2.DataAccess.Sql.Contexts;
using WebbLabb2.DataAccess.Sql.Models;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.DataAccess.Sql.Repositories;

public class CustomerRepository
{
    private readonly CustomerDbContext _customerDbContext;

    public CustomerRepository(CustomerDbContext customerDbContext)
    {
        _customerDbContext = customerDbContext;
    }

    public async Task<bool> AddCustomer(CustomerDto dto)
    {
        if (await _customerDbContext.CustomerModel.AnyAsync(c => c.Email == dto.Email))
            return false;

        _customerDbContext.CustomerModel.Add(ConvertToModel(dto));
        await _customerDbContext.SaveChangesAsync();

        return true;
    }
    public async Task<CustomerDto> GetCustomer(Guid id)
    {
        var user = await _customerDbContext.CustomerModel
            .FirstOrDefaultAsync(u => u.CustomerId.Equals(id));

        if (user == null) return null!;

        return ConvertToDto(user);
    }

    public async Task<CustomerDto> GetCustomerByEmail(string email)
    {
        var user = await _customerDbContext.CustomerModel
            .FirstOrDefaultAsync(u => u.Email.Equals(email));

        if (user != null) return ConvertToDto(user);

        user = new CustomerModel
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            Email = "Not found",
            CellNumber = string.Empty,
            StreetAddress = string.Empty,
            City = string.Empty,
            ZipCode = 0
        };
        return ConvertToDto(user);

    }

    public async Task<CustomerDto[]> GetAllCustomers()
    {
        var users = await _customerDbContext.CustomerModel.ToListAsync();

        return users.Select(ConvertToDto).ToArray();
    }

    public async Task<bool> UpdateCustomer(Guid id, CustomerDto dto)
    {
        var user = await _customerDbContext.CustomerModel
            .FirstOrDefaultAsync(c => c.CustomerId.Equals(id));

        if (user == null) return false;

        user.FirstName = dto.FirstName.IsNullOrEmpty() ? user.FirstName : dto.FirstName;
        user.LastName = dto.LastName.IsNullOrEmpty() ? user.LastName : dto.LastName;
        user.Email = user.Email;
        user.CellNumber = dto.CellNumber.IsNullOrEmpty() ? user.CellNumber : dto.CellNumber;
        user.StreetAddress = dto.StreetAddress.IsNullOrEmpty() ? user.StreetAddress : dto.StreetAddress;
        user.City = dto.City.IsNullOrEmpty() ? user.City : dto.City;
        user.ZipCode = dto.ZipCode;

        await _customerDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveCustomer(Guid id)
    {
        var user = await _customerDbContext.CustomerModel
            .FirstOrDefaultAsync(u => u.CustomerId.Equals(id));

        if (user == null) return false;

        _customerDbContext.CustomerModel.Remove(user!);
        await _customerDbContext.SaveChangesAsync();

        return true;
    }

    private CustomerModel ConvertToModel(CustomerDto dto)
    {
        return new CustomerModel()
        {
            CustomerId = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            CellNumber = dto.CellNumber,
            StreetAddress = dto.StreetAddress,
            City = dto.City,
            ZipCode = dto.ZipCode
        };
    }

    private CustomerDto ConvertToDto(CustomerModel dataModel)
    {
        return new CustomerDto()
        {
            Id = dataModel.CustomerId,
            FirstName = dataModel.FirstName,
            LastName = dataModel.LastName,
            Email = dataModel.Email,
            CellNumber = dataModel.CellNumber,
            StreetAddress = dataModel.StreetAddress,
            City = dataModel.City,
            ZipCode = dataModel.ZipCode
        };
    }
}