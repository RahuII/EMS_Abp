using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EMS.Departments;

public interface IDepartmentAppService : IApplicationService
{
    /// <summary>
    /// This method is used to get the lookup data for the Department entity.
    /// </summary>
    /// <param name="id"> Id of the Department entity. </param>
    /// <returns> Object of type DepartmentDto that holds details of the Department entity. </returns>
    Task<DepartmentDto> GetAsync(Guid id);

    /// <summary>
    /// This method is used to get the list of Department entities.
    /// </summary>
    /// <param name="input"> Object of type GetDepartmentListDto that holds details of the Department entity. </param>
    /// <returns> List of DepartmentDto object that holds details of the Department entity. </returns>
    Task<PagedResultDto<DepartmentDto>> GetListAsync(GetDepartmentListDto input);

    /// <summary>
    /// This method is used to create a new Department entity.
    /// </summary>
    /// <param name="input"> Object of type CreateDepartmentDto that holds details of the Department entity. </param>
    /// <returns> Object of type DepartmentDto that holds details of the Department entity. </returns>
    Task<DepartmentDto> CreateAsync(CreateDepartmentDto input);

    /// <summary>
    /// This method is used to update an existing Department entity.
    /// </summary>
    /// This method is used to update an existing Department entity.
    /// <param name="id" > Id of the Department entity. </param>
    /// <param name="input"> Object of type UpdateDepartmentDto that holds details of the Department entity. </param>
    /// <returns> Object of type DepartmentDto that holds details of the Department entity. </returns>
    Task UpdateAsync(Guid id, UpdateDepartmentDto input);


    /// <summary>
    /// This method is used to delete an existing Department entity.
    /// </summary>
    /// <param name="id"> Id of the Department entity. </param>
    /// <returns> Object of type DepartmentDto that holds details of the Department entity. </returns>
    Task DeleteAsync(Guid id);
}
