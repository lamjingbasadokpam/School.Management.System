using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Management.Localization;
using School.Management.MultiTenancy;
using School.Management.Permissions;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace School.Management.Blazor.Menus;

public class ManagementMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public ManagementMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<ManagementResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                ManagementMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home"
            )
        );
        var managementMenu = new ApplicationMenuItem(
      "Management",
      l["Menu:Management"],
      icon: "fas fa-school"
         );

        context.Menu.AddItem(managementMenu);

        //CHECK the PERMISSION
        if (await context.IsGrantedAsync(ManagementPermissions.Subjects.Default))
        {
            managementMenu.AddItem(new ApplicationMenuItem(
                "Management.Subjects",
                l["Menu:Subjects"],
                url: "/subject", icon: "fas fa-book"
            ));
        }
        if (await context.IsGrantedAsync(ManagementPermissions.Students.Default))
        {
            managementMenu.AddItem(new ApplicationMenuItem(
                "Management.Students",
                l["Menu:Students"],
                url: "/student", icon: "fas fa-user"
            ));
        }
        if (await context.IsGrantedAsync(ManagementPermissions.Teachers.Default))
        {
            managementMenu.AddItem(new ApplicationMenuItem(
                "Management.Teachers",
                l["Menu:Teachers"],
                url: "/teacher", icon: "fas fa-user"
            ));
        }
        
       

        var administration = context.Menu.GetAdministration();

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        //return Task.CompletedTask;
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{authServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null).RequireAuthenticated());

        return Task.CompletedTask;
    }
}
