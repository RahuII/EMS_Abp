using EMS.RegxConsts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EMS.Employees;

public class EmployeeDto : AuditedEntityDto<Guid>
{
    public Guid DepartmentId { get; set; }

    [Required]
    public string DepartmentName { get; set; }

    [Required]
    [RegularExpression(RegexConst.NameRegex)]
    public string Name { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [RegularExpression(RegexConst.EmailRegex)]
    public string Email { get; set; }
    public string Phone { get; set; }
}
