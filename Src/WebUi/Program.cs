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

using System.Reflection;

using Framework.NLogTools;

using Microsoft.AspNetCore.Identity;

using Sudoku.Repository;
using Sudoku.Repository.Abstraction.Entities;
using Sudoku.Repository.Context;
using Sudoku.WebUi.Tools;

Framework.NLogTools.NLogConfigExtensions.ConfigureNLogLocation("SolveSudoku", Assembly.GetExecutingAssembly());
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();

builder.Services.AddHttpClient("Azure", httpClient => { httpClient.BaseAddress = new Uri("https://sudokusolve.azurewebsites.net/api/"); });
builder.Services.AddHttpClient("local", httpClient => { httpClient.BaseAddress = new Uri("http://localhost:5185/api/"); });
builder.Services.AddHttpClient("pi",    httpClient => { httpClient.BaseAddress = new Uri("https://ait.dyndns-home.com/sudokusolve/api/"); });
builder.Services.AddHttpClient("leocloud",    httpClient => { httpClient.BaseAddress = new Uri("https://student.cloud.htl-leonding.ac.at/h.aitenbichler/sudokusolveserver/api/"); });

builder.UseNLog();

builder.Host.UseSystemd();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services
    .AddSudokuRepository(SqlServerDatabaseTools.OptionBuilder)
    ;

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SudokuContext>()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    ;

builder.Services.AddTransient<AuthInitializer>();
var app = builder.Build();

var supportedCultures = new[] { "de", "en-US", "fr" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UsePathBase("/sudokusolver");

// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

SudokuContext.InitializeDatabase(app.Services);

using (var scope = app.Services.CreateScope())
{
    var authInitializer = scope.ServiceProvider.GetRequiredService<AuthInitializer>();
    await authInitializer.Initalize();
}

app.Run();