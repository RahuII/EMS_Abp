using EMS.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace EMS.Permissions;

public class EMSPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var employeeMSGroup = context.AddGroup(EMSPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(EMSPermissions.MyPermission1, L("Permission:MyPermission1"));
        employeeMSGroup.AddPermission(EMSPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        employeeMSGroup.AddPermission(EMSPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        var employeesPermission = employeeMSGroup.AddPermission(EMSPermissions.Employees.Default, L("Permission:Employees"));
        employeesPermission.AddChild(EMSPermissions.Employees.Create, L("Permission:Employees.Create"));
        employeesPermission.AddChild(EMSPermissions.Employees.Edit, L("Permission:Employees.Edit"));
        employeesPermission.AddChild(EMSPermissions.Employees.Delete, L("Permission:Employees.Delete"));

        var departmentsPermission = employeeMSGroup.AddPermission(EMSPermissions.Departments.Default, L("Permission:Departments"));
        departmentsPermission.AddChild(EMSPermissions.Departments.Create, L("Permission:Departments.Create"));
        departmentsPermission.AddChild(EMSPermissions.Departments.Edit, L("Permission:Departments.Edit"));
        departmentsPermission.AddChild(EMSPermissions.Departments.Delete, L("Permission:Departments.Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EMSResource>(name);
    }
}
