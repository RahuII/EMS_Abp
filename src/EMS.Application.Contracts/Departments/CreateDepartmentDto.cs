using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EMS.Departments;

public class CreateDepartmentDto
{
    [Required]
    [StringLength(DepartmentConsts.MaxNameLength)]
    public string Name { get; set; }

    public string Description { get; set; }
}
