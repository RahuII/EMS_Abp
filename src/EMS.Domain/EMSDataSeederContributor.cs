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

public class EMSDataSeederContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Employee, Guid> _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly DepartmentManager _departmentManager;
    public EMSDataSeederContributor(
        IRepository<Employee, Guid> employeeRepository,
        IDepartmentRepository departmentRepository,
        DepartmentManager departmentManager

        )
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _departmentManager = departmentManager;
    }
    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _employeeRepository.GetCountAsync() <= 0)
        {
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
        if (await _departmentRepository.GetCountAsync() <= 0)
        {
            await _departmentRepository.InsertAsync(
                await _departmentManager.CreateAsync(
                    "Computer",
                    "Computer science is the study of computation"
                )
            );

            await _departmentRepository.InsertAsync(
                await _departmentManager.CreateAsync(
                    "Civil",
                    "It is the branch with a lot of diversity right from Surveying"
                )
            );
        }

    }


}