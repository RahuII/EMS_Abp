using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EMS.Employees;

public class DepartmentLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }
}
