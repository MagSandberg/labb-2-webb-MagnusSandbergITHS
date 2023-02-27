
using System;
using WebbLabb2RestApi.DataAccess.Sql.Contexts;
using WebbLabb2RestApi.DataAccess.Sql.Models;
using WebbLabb2RestApi.Shared.DTOs;

namespace WebbLabb2RestApi.DataAccess.Sql.Repositories;

public class CustomerRepository
{
    private readonly CustomerDbContext _customerDbContext;

    public CustomerRepository(CustomerDbContext storage)
    {
        _customerDbContext = storage;
    }

    public async Task AddUser(CustomerDto dto)
    {
        _customerDbContext.CustomerModel.Add(ConvertToModel(dto));
        await _customerDbContext.SaveChangesAsync();
    }

    //public Person GetPerson(Guid id)
    //{
    //    return _customerDbContext.People.FirstOrDefault(p => p.Id.Equals(id));
    //}

    //public void UpdateName(Guid id, string name)
    //{
    //    var person = _customerDbContext.People.FirstOrDefault(p => p.Id.Equals(id));
    //    person.Name = name;
    //    _customerDbContext.SaveChanges();
    //}

    //public void UpdateAge(Guid id, int age)
    //{
    //    var person = _customerDbContext.People.FirstOrDefault(p => p.Id.Equals(id));
    //    person.Age = age;
    //    _customerDbContext.SaveChanges();
    //}

    //public void DeletePerson(Guid id)
    //{
    //    var person = _customerDbContext.People.FirstOrDefault(p => p.Id.Equals(id));
    //    _customerDbContext.People.Remove(person);
    //    _customerDbContext.SaveChanges();
    //}

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