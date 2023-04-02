using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EMS.Departments;

public class DepartmentDto : EntityDto<Guid>
{
    [Required]
    [StringLength(DepartmentConsts.MaxNameLength)]
    public string Name { get; set; }
    public string Description { get; set; }
}
