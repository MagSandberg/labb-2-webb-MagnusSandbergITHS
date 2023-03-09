using WebbLabb2.DataAccess.Sql.Repositories;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Services;

public class CustomerService
{
    private readonly CustomerRepository _customerRepository;

    public CustomerService(CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<bool> AddCustomer(CustomerDto customerDto)
    {
        return await _customerRepository.AddCustomer(customerDto);
    }
    public async Task<CustomerDto> GetCustomer(Guid id)
    {
        return await _customerRepository.GetCustomer(id);
    }

    public async Task<CustomerDto[]> GetCustomers()
    {
        return await _customerRepository.GetAllCustomers();
    }

    public async Task<CustomerDto> GetCustomerByEmail(string email)
    {
        return await _customerRepository.GetCustomerByEmail(email);
    }

    public async Task UpdateCustomer(Guid id, CustomerDto dto)
    {
        await _customerRepository.UpdateCustomer(id, dto);
    }

    public async Task RemoveCustomer(Guid id)
    {
        await _customerRepository.RemoveCustomer(id);
    }
}