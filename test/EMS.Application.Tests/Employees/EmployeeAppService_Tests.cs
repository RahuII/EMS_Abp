using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;

namespace EMS.Employees;

public class EmployeeAppService_Tests : EMSApplicationTestBase
{
    private readonly IEmployeeAppService _employeeAppService;

    public EmployeeAppService_Tests()
    {
        _employeeAppService = GetRequiredService<IEmployeeAppService>();
    }

    [Fact]
    public async Task Should_Get_List_Of_Employees()
    {
        //Act
        var result = await _employeeAppService.GetListAsync(
            new PagedAndSortedResultRequestDto()
        );

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
        result.Items.ShouldContain(b => b.Name == "rahul");
    }
    [Fact]
    public async Task Should_Create_A_Valid_Employee()
    {
        //Act
        var result = await _employeeAppService.CreateAsync(
            new CreateUpdateEmployeeDto
            {
                Name = "New Name",
                DateOfBirth = new DateTime(2002, 3, 9),
                Email = "emp@gmail.com",
                Phone = "1234567890"
            }
        );

        //Assert
        result.Id.ShouldNotBe(Guid.Empty);
        result.Name.ShouldBe("New Name");
    }
    //[Fact]
    //public async Task Should_Not_Create_A_Employee_Without_Name()
    //{
    //    var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
    //    {
    //        await _employeeAppService.CreateAsync(
    //            new CreateUpdateEmployeeDto
    //            {
    //                Name = "",
    //                DateOfBirth = new DateTime(2007, 3, 9),
    //                Email = "emp1@gmail.com",
    //                Phone = "1234567890"
    //            }
    //        );
    //    });

    //    exception.ValidationErrors
    //        .ShouldContain(err => err.MemberNames.Any(mem => mem == "Name"));
    //}
}
