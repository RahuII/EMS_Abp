using EMS.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EMS.Employees;

public class EmployeeAppService : CrudAppService<
        Employee, //The Employee entity
        EmployeeDto, //Used to show employees
        Guid, //Primary key of the employee entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateEmployeeDto>, //Used to create/update a book
    IEmployeeAppService
{
    public EmployeeAppService(IRepository<Employee, Guid> repository)
    : base(repository)
    {
        GetPolicyName = EMSPermissions.Employees.Default;
        CreatePolicyName = EMSPermissions.Employees.Create;
        UpdatePolicyName = EMSPermissions.Employees.Edit;
        DeletePolicyName = EMSPermissions.Employees.Delete;
    }
}