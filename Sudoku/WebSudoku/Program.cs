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

using Framework.Tools;

using Sudoku.Repository;
using Sudoku.Repository.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddHttpClient("Azure", httpClient => { httpClient.BaseAddress = new Uri("https://sudokusolve.azurewebsites.net/api/"); });
builder.Services.AddHttpClient("local", httpClient => { httpClient.BaseAddress = new Uri("http://localhost:55149/api/"); });
builder.Services.AddHttpClient("pi", httpClient => { httpClient.BaseAddress = new Uri("https://ait.dyndns-home.com/sudokusolve/api/"); });

builder.Services
    .AddSudokuRepository(SqlServerDatabaseTools.OptionBuilder)
    .AddFrwTools();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

SudokuContext.InitializeDatabase(app.Services);

app.Run();