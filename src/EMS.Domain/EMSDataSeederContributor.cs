using EMS.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace EMS;

public class EMSDataSeederContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Employee, Guid> _employeeRepository;
    public EMSDataSeederContributor(IRepository<Employee, Guid> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _employeeRepository.GetCountAsync() > 0)
        {
            return;
        }
        await _employeeRepository.InsertAsync(
                new Employee
                {
                    Name = "rahul",
                    DateOfBirth = new DateTime(2002, 6, 8),
                    Email = "rahul@gmail.com",
                    Phone = "1234567890"
                },
                autoSave: true
            );
        await _employeeRepository.InsertAsync(
                new Employee
                {
                    Name = "adi",
                    DateOfBirth = new DateTime(1999, 6, 8),
                    Email = "adi@gmail.com",
                    Phone = "0987654321"
                },
                autoSave: true
            );
    }


}