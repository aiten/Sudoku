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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Sudoku.Repository.Abstraction.Entities;
using Sudoku.Shared;

[Authorize(Roles = SudokuConst.Role_Admin)]
public class EditModel : PageModelBase
{
    public EditModel(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole>    roleManager,
        ILogger<EditModel>           logger) : base(userManager, roleManager, logger)
    {
    }

    [BindProperty]
    public IList<AppUserRole> Roles { get; set; } = new List<AppUserRole>();

    protected override async Task<bool> LoadData(string id)
    {
        if (!await base.LoadData(id))
        {
            return false;
        }

        Roles = await GetAllRolesAsync(AppUser);

        return true;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var origRoles = await GetAllRolesAsync(AppUser);

        var changed = origRoles.Join(Roles, r => r.RoleName, r => r.RoleName, (r, l) => (r, l))
            .Where(x => x.r.IsUserInRole != x.l.IsUserInRole)
            .ToList();

        var addRole    = changed.Where(x => x.r.IsUserInRole == false).Select(x => x.r.RoleName).ToList();
        var removeRole = changed.Where(x => x.r.IsUserInRole == true).Select(x => x.r.RoleName).ToList();

        var appUser = await _userManager.FindByIdAsync(AppUser.Id);

        await _userManager.AddToRolesAsync(appUser!, addRole);
        await _userManager.RemoveFromRolesAsync(appUser!, removeRole);

        return RedirectToPage("./Index");
    }
}