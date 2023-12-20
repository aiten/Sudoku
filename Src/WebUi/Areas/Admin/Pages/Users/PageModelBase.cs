/*
  This file is part of Sudoku - A library to solve a sudoku.

  Copyright (c) Herbert Aitenbichler

  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
  to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
  and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/

namespace Sudoku.WebUi.Areas.Admin.Pages.Users;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Sudoku.Repository.Abstraction.Entities;

public class PageModelBase : PageModel
{
    protected readonly UserManager<ApplicationUser> _userManager;
    protected readonly RoleManager<IdentityRole>    _roleManager;
    private readonly   ILogger                      _logger;

    public PageModelBase(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole>    roleManager,
        ILogger                      logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger      = logger;
    }

    [BindProperty]
    public ApplicationUser AppUser { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        return await LoadData(id) ? Page() : NotFound();
    }

    protected virtual async Task<bool> LoadData(string id)
    {
        var appUser = await _userManager.FindByIdAsync(id);
        if (appUser == null)
        {
            return false;
        }

        AppUser = appUser;

        return true;
    }

    public async Task<string> GetUserRolesSummaryAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return string.Join(',', roles);
    }

    public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<IList<string>> GetRolesAsync()
    {
        return await _roleManager.Roles.Select(r => r.Name!).ToListAsync();
    }

    public async Task<bool> IsInRoleAsync(ApplicationUser user, string role)
    {
        return await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<IList<AppUserRole>> GetAllRolesAsync(ApplicationUser user)
    {
        var roles = (await GetRolesAsync())
            .Select(role =>
                new AppUserRole()
                {
                    RoleName = role,
                })
            .ToList();

        foreach (var role in roles)
        {
            role.IsUserInRole = await IsInRoleAsync(user, role.RoleName);
        }

        return roles;
    }
}