using Microsoft.EntityFrameworkCore;
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

    public async Task AddCustomer(CustomerDto dto)
    {
        _customerDbContext.CustomerModel.Add(ConvertToModel(dto));
        await _customerDbContext.SaveChangesAsync();
    }

    public async Task<CustomerDto[]> GetAllUsers()
    {
        var users = await _customerDbContext.CustomerModel.ToListAsync();

        return users.Select(ConvertToDto).ToArray();
    }

    public async Task<CustomerDto> GetUser(Guid id)
    {
        var user = await _customerDbContext.CustomerModel
            .FirstOrDefaultAsync(u => u.CustomerId.Equals(id));

        return ConvertToDto(user);
    }

    public async Task<CustomerDto> GetUserByEmail(string email)
    {
        var user = await _customerDbContext.CustomerModel
            .FirstOrDefaultAsync(u => u.Email.Equals(email));

        return ConvertToDto(user);
    }

    public async Task UpdateUser(Guid id, CustomerDto dto)
    {
        var user = await _customerDbContext.CustomerModel
            .FirstOrDefaultAsync(c => c.CustomerId.Equals(id));

        user!.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.CellNumber = dto.CellNumber;
        user.StreetAddress = dto.StreetAddress;
        user.City = dto.City;
        user.ZipCode = dto.ZipCode;
        
        await _customerDbContext.SaveChangesAsync();
    }

    public async Task RemoveUser(Guid id)
    {
        var user = await _customerDbContext.CustomerModel
            .FirstOrDefaultAsync(u => u.CustomerId.Equals(id));

        _customerDbContext.CustomerModel.Remove(user);
        await _customerDbContext.SaveChangesAsync();
    }

    private CustomerModel ConvertToModel(CustomerDto dto)
    {
        return new CustomerModel()
        {
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