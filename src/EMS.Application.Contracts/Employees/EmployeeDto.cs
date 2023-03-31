using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EMS.Employees;

public class EmployeeDto : AuditedEntityDto<Guid>
{
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; }

    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
