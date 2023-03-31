using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EMS.Employees;

public interface IEmployeeAppService : 
    ICrudAppService< //Defines CRUD methods
        EmployeeDto, //Used to show Employees
        Guid, //Primary key of the Employee entity
        EmployeeFilterDto, //Used to filter Employees
        CreateUpdateEmployeeDto>
{
    Task<ListResultDto<DepartmentLookupDto>> GetDepartmentLookupAsync();

}