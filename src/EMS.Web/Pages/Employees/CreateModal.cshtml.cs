using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EMS.Departments;
using EMS.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EMS.Web.Pages.Employees;

public class CreateModalModel : EMSPageModel
{
    [BindProperty]
    public CreateEmployeeViewModel Employee { get; set; }

    public List<SelectListItem> Departments { get; set; }

    private readonly IEmployeeAppService _employeeAppService;

    public CreateModalModel(
        IEmployeeAppService employeeAppService)
    {
        _employeeAppService = employeeAppService;
    }

    public async Task OnGetAsync()
    {
        Employee = new CreateEmployeeViewModel();

        var departmentLookup = await _employeeAppService.GetDepartmentLookupAsync();
        Departments = departmentLookup.Items
            .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _employeeAppService.CreateAsync(
            ObjectMapper.Map<CreateEmployeeViewModel, CreateUpdateEmployeeDto>(Employee)
            );
        return NoContent();
    }

    public class CreateEmployeeViewModel
    {
        [SelectItems(nameof(Departments))]
        [DisplayName("Department")]
        public Guid DepartmentId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}