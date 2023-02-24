using WebbLabb2RestApi.DataAccess.Sql.Models;
using WebbLabb2RestApi.Shared.DTOs;

namespace WebbLabb2RestApi.DataAccess.Sql.Repositories;

public class CustomerRepository
{
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