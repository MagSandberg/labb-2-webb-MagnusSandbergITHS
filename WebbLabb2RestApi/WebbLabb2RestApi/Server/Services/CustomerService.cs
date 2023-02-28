using WebbLabb2RestApi.DataAccess.Repositories;
using WebbLabb2RestApi.DataAccess.Sql.Repositories;
using WebbLabb2RestApi.Shared.DTOs;

namespace WebbLabb2RestApi.Server.Services;

public class CustomerService
{
    private readonly CustomerRepository _customerRepository;

    public CustomerService(CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task AddUser(CustomerDto customerDto)
    {
        await _customerRepository.AddUser(customerDto);
    }

    public async Task<CustomerDto[]> GetUsers()
    {
        return await _customerRepository.GetAllUsers();
    }

    public async Task<CustomerDto> GetUser(Guid id)
    {
        return await _customerRepository.GetUser(id);
    }

    public async Task<CustomerDto> GetUserByEmail(string email)
    {
        return await _customerRepository.GetUserByEmail(email);
    }

    public async Task UpdateUser(Guid id, CustomerDto dto)
    {
        await _customerRepository.UpdateUser(id, dto);
    }

    public async Task RemoveUser(Guid id)
    {
        await _customerRepository.RemoveUser(id);
    }
}