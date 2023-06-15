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


namespace Sudoku.WebUi.Tools;

using Framework.Repository.Abstraction;

using Microsoft.AspNetCore.Identity;

using Sudoku.Repository.Abstraction.Entities;
using Sudoku.Shared;

public class AuthInitializer
{
    private UserManager<ApplicationUser> UserManager { get; set; }
    private RoleManager<IdentityRole>    RoleManager { get; set; }

    public AuthInitializer(
        IUnitOfWork                  unitOfWork,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole>    roleManager)
    {
        RoleManager = roleManager;
        UserManager = userManager;
    }

    public async Task Initalize()
    {
        if (RoleManager.Roles.Any(x => x.Name == SudokuConst.Role_Admin)) return;

        await RoleManager.CreateAsync(new IdentityRole(SudokuConst.Role_Admin));
        await RoleManager.CreateAsync(new IdentityRole(SudokuConst.Role_User));
        await RoleManager.CreateAsync(new IdentityRole(SudokuConst.Role_Guest));

        await UserManager.CreateAsync(new ApplicationUser
        {
            UserName       = "admin@htl.at",
            Email          = "admin@htl.at",
            EmailConfirmed = true
        }, "Admin123*");


        var user = await UserManager.FindByEmailAsync("admin@htl.at");
        await UserManager.AddToRoleAsync(user!, SudokuConst.Role_Admin);
    }
}