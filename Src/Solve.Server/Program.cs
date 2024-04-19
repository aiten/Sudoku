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

using Sudoku.Solve;
using Sudoku.Solve.Server.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

app.UsePathBase("/sudokusolve");

// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add CORS to support Single Page Apps (SPAs)
app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

const string SudokuTag = "Sudoku";

app.MapGet("/api/sudoku", (string[] sudoku) =>
    {
        var s = sudoku.ToArray().CreateSudoku();
        return s.GetSolveInfo();
    })
    .WithOpenApi()
    .WithTags(SudokuTag);

app.MapGet("/api/sudoku/smart", (string[] sudoku) =>
    {
        var s = sudoku.ToArray().CreateSudoku();

        return new SudokuResult()
        {
            Sudoku = s.SmartPrint(string.Empty).ToArray(),
            Info   = s.SmartPrintInfo()
        };
    })
    .WithOpenApi()
    .WithTags(SudokuTag);


app.MapGet("/api/sudoku/next", (string[] sudoku, int row, int col) =>
    {
        var s = sudoku.ToArray().CreateSudoku();
        s.UpdatePossible();
        s.SetNextPossible(row, col);

        return s.SmartPrint(string.Empty);
    })
    .WithOpenApi()
    .WithTags(SudokuTag);

app.MapGet("/api/sudoku/set", (string[] sudoku, int row, int col, int no) =>
    {
        var s = sudoku.ToArray().CreateSudoku();
        s.Set(row, col, no);

        return s.SmartPrint(string.Empty);
    })
    .WithOpenApi()
    .WithTags(SudokuTag);

app.MapGet("/api/sudoku/solutioncount", (string[] sudoku) =>
    {
        var s   = sudoku.ToArray().CreateSudoku();
        var cts = new CancellationTokenSource();

        var task = Task.Run(() => { return s.CalcPossibleSolutions(cts.Token); });

        bool isCompletedSuccessfully = task.Wait(TimeSpan.FromMilliseconds(1000));

        if (isCompletedSuccessfully)
        {
            return task.Result;
        }
        
        cts.Cancel(false);

        throw new TimeoutException("The function has taken longer than the maximum time allowed.");
    })
    .WithOpenApi()
    .WithTags(SudokuTag);

app.MapGet("/api/sudoku/finish", (string[] sudoku) =>
    {
        var s = sudoku.ToArray().CreateSudoku();
        if (s.Finish())
        {
            return new SudokuResult()
            {
                Sudoku = s.SmartPrint(string.Empty).ToArray(),
                Info   = s.SmartPrintInfo()
            };
        }
        throw new ("Cannot solve sudoku.");
    })
    .WithOpenApi()
    .WithTags(SudokuTag);


static Info Info()
{
    var ass     = Assembly.GetExecutingAssembly();
    var assName = ass.GetName();

    return new Info()
    {
        Version   = ass.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion!,
        Copyright = ass.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright!,
        Name      = assName.Name!,
        FullName  = assName.FullName,
    };
}

const string InfoTag = "Info";

app.MapGet("/api/info",           () => Info()).WithOpenApi().WithTags(InfoTag);
app.MapGet("/api/info/version",   () => Info().Version.ToString()).WithOpenApi().WithTags(InfoTag);
app.MapGet("/api/info/name",      () => Info().Name).WithOpenApi().WithTags(InfoTag);
app.MapGet("/api/info/fullname",  () => Info().FullName).WithOpenApi().WithTags(InfoTag);
app.MapGet("/api/info/copyright", () => Info().Copyright).WithOpenApi().WithTags(InfoTag);

app.MapGet("/", () => Info());

app.Run();