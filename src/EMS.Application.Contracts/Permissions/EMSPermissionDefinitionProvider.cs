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

        var booksPermission = employeeMSGroup.AddPermission(EMSPermissions.Employees.Default, L("Permission:Books"));
        booksPermission.AddChild(EMSPermissions.Employees.Create, L("Permission:Employees.Create"));
        booksPermission.AddChild(EMSPermissions.Employees.Edit, L("Permission:Employees.Edit"));
        booksPermission.AddChild(EMSPermissions.Employees.Delete, L("Permission:Employees.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EMSResource>(name);
    }
}
