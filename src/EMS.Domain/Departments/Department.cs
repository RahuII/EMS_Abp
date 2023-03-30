using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMS.Departments;

public class Department : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string Description { get;  set; }
    private Department()
    {
        /* This constructor is for deserialization / ORM purpose */
    }
    internal Department(Guid id, [NotNull] string name, [CanBeNull] string description = null) : base(id)
    {
        Name = name;
        Description = description;
    }
    internal Department ChangeName(string name)
    {
        SetName(name);
        return this;
    }
    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name, nameof(name),
            maxLength: DepartmentConsts.MaxNameLength
         );
    }
}
