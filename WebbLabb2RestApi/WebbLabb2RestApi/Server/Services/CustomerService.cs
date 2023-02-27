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
}