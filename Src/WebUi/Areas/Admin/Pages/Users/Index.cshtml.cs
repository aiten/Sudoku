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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Sudoku.Repository.Abstraction.Entities;
using Sudoku.Shared;

[Authorize(Roles = SudokuConst.Role_Admin)]
public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole>    _roleManager;
    private readonly ILogger<IndexModel>          _logger;

    public IList<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    public IList<IdentityRole>    Roles { get; set; } = new List<IdentityRole>();

    public IndexModel(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole>    roleManager,
        ILogger<IndexModel>          logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger      = logger;
    }

    public async Task OnGetAsync()
    {
        Users = await _userManager.Users.ToListAsync();
        Roles = await _roleManager.Roles.ToListAsync();
    }

    public async Task<string> GetUserRoles(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return string.Join(',', roles);
    }
}