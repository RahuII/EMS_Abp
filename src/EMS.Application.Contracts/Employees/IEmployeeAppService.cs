using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EMS.Employees;

public interface IEmployeeAppService : ICrudAppService< //Defines CRUD methods
        EmployeeDto, //Used to show Employees
        Guid, //Primary key of the Employee entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateEmployeeDto>
{

}