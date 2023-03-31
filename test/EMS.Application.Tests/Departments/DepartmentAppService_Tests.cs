using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EMS.Departments;

public class DepartmentAppService_Tests : EMSApplicationTestBase
{
    private readonly IDepartmentAppService _departmentAppService;

    public DepartmentAppService_Tests()
    {
        _departmentAppService = GetRequiredService<IDepartmentAppService>();
    }

    [Fact]
    public async Task Should_Get_All_Department_Without_Any_Filter()
    {
        var result = await _departmentAppService.GetListAsync(new GetDepartmentListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
        result.Items.ShouldContain(department => department.Name == "Computer");
        result.Items.ShouldContain(department => department.Name == "Civil");
    }

    [Fact]
    public async Task Should_Get_Filtered_Authors()
    {
        var result = await _departmentAppService.GetListAsync(
            new GetDepartmentListDto { Filter = "Computer" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(department => department.Name == "Computer");
    }

    [Fact]
    public async Task Should_Create_A_New_Author()
    {
        var departmentDto = await _departmentAppService.CreateAsync(
            new CreateDepartmentDto
            {
                Name = "Edward Bellamy",
                Description = "Edward Bellamy was an American department..."
            }
        );

        departmentDto.Id.ShouldNotBe(Guid.Empty);
        departmentDto.Name.ShouldBe("Edward Bellamy");
    }

    [Fact]
    public async Task Should_Not_Allow_To_Create_Duplicate_Author()
    {
        await Assert.ThrowsAsync<DepartmentAlreadyExistsException>(async () =>
        {
            await _departmentAppService.CreateAsync(
                new CreateDepartmentDto
                {
                    Name = "Computer",
                    Description = "Computer science is the study of computation"
                }
            );
        });
    }

    //TODO: Test other methods...
}