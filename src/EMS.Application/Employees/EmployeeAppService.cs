﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using EMS.Departments;
using EMS.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace EMS.Employees;

[Authorize(EMSPermissions.Employees.Default)]
public class EmployeeAppService :
       CrudAppService<
        Employee, //The Employee entity
        EmployeeDto, //Used to show Employees
        Guid, //Primary key of the book entity
        EmployeeFilterDto, //Used for paging/sorting
        CreateUpdateEmployeeDto>,
    IEmployeeAppService
{
    private readonly IDepartmentRepository _departmentRepository;

    public EmployeeAppService(
        IRepository<Employee, Guid> repository,
        IDepartmentRepository departmentRepository)
        : base(repository)
    {
        _departmentRepository = departmentRepository;
        GetPolicyName = EMSPermissions.Employees.Default;
        GetListPolicyName = EMSPermissions.Employees.Default;
        CreatePolicyName = EMSPermissions.Employees.Create;
        UpdatePolicyName = EMSPermissions.Employees.Edit;
        DeletePolicyName = EMSPermissions.Employees.Create;
    }

    public override async Task<EmployeeDto> GetAsync(Guid id)
    {
        //Get the IQueryable<Employee> from the repository
        var queryable = await Repository.GetQueryableAsync();

        //Prepare a query to join employees and departments
        var query = from employee in queryable
                    join department in await _departmentRepository.GetQueryableAsync() on employee.DepartmentId equals department.Id
                    where employee.Id == id
                    select new { employee, department };

        //Execute the query and get the employee with department
        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        if (queryResult == null)
        {
            throw new EntityNotFoundException(typeof(Employee), id);
        }

        var employeeDto = ObjectMapper.Map<Employee, EmployeeDto>(queryResult.employee);
        employeeDto.DepartmentName = queryResult.department.Name;
        return employeeDto;
    }

    public override async Task<PagedResultDto<EmployeeDto>> GetListAsync(EmployeeFilterDto input)
    {
        var queryable = await Repository.GetQueryableAsync();

        var query = from employee in queryable
                    join department in await _departmentRepository.GetQueryableAsync() on employee.DepartmentId equals department.Id
                    select new { employee, department };



        query = query
            .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
            x => x.employee.Name.ToLower().Contains(input.Filter.ToLower()) ||
            x.department.Name.ToLower().Contains(input.Filter.ToLower()))
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);



        var queryResult = await AsyncExecuter.ToListAsync(query);



        var employeeDtos = queryResult.Select(x =>
        {
            var employeeDto = ObjectMapper.Map<Employee, EmployeeDto>(x.employee);
            employeeDto.DepartmentName = x.department.Name;
            return employeeDto;
        }).ToList();



        var totalCount = await Repository.GetCountAsync();



        return new PagedResultDto<EmployeeDto>(totalCount, employeeDtos);
    }

    public async Task<ListResultDto<DepartmentLookupDto>> GetDepartmentLookupAsync()
    {
        var departments = await _departmentRepository.GetListAsync();

        return new ListResultDto<DepartmentLookupDto>(
            ObjectMapper.Map<List<Department>, List<DepartmentLookupDto>>(departments)
        );
    }

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"employee.{nameof(Employee.Name)}";
        }

        if (sorting.Contains("departmentName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "departmentName",
                "department.Name",
                StringComparison.OrdinalIgnoreCase
            );
        }

        return $"employee.{sorting}";
    }
}
