using EMS.Departments;
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
public class EMSDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Employee, Guid> _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly DepartmentManager _departmentManager;

    public EMSDataSeederContributor(
        IRepository<Employee, Guid> employeeRepository,
        IDepartmentRepository departmentRepository,
        DepartmentManager departmentManager)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _departmentManager = departmentManager;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _employeeRepository.GetCountAsync() > 0)
        {
            return;
        }

        var orwell = await _departmentRepository.InsertAsync(
            await _departmentManager.CreateAsync(
                "Computer",
                "Computer Science"
            )
        );

        var douglas = await _departmentRepository.InsertAsync(
            await _departmentManager.CreateAsync(
                "Civil",
                "Civil Engineering"
               )
        );

        await _employeeRepository.InsertAsync(
            new Employee
            {
                DepartmentId = orwell.Id, // SET THE DEPARTMENT
                Name = "Rahul",
                DateOfBirth = new DateTime(2002, 3, 9),
                Email = "rahul@gmail.com",
                Phone = "1234567890"
            },
            autoSave: true
        );

        await _employeeRepository.InsertAsync(
            new Employee
            {
                DepartmentId = orwell.Id, // SET THE DEPARTMENT
                Name = "Dev",
                DateOfBirth = new DateTime(2002, 3, 9),
                Email = "dev@gmail.com",
                Phone = "1276567890"
            },
            autoSave: true
        );
    }
}