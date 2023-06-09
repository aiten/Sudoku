﻿/*
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

namespace Sudoku.Repository.Context;

using System;
using System.Linq;

using Framework.Repository.Abstraction.Entities;
using Framework.Repository.Mappings;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Sudoku.Repository.Abstraction.Entities;
using Sudoku.Repository.Mappings;

public class SudokuContext : IdentityDbContext<ApplicationUser>
{
    public SudokuContext(DbContextOptions<SudokuContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging(true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SudokuEntity>().Map();
        modelBuilder.Entity<CategoryEntity>().Map();

        modelBuilder.Entity<Log>().Map();

        base.OnModelCreating(modelBuilder);
    }

    protected void InitOrUpdateDatabase()
    {
        if (Set<SudokuEntity>().Any())
        {
            SaveChanges();
        }
        else
        {
            new SudokuDefaultData(this).Import();
            SaveChanges();
        }
    }

    public static void InitializeDatabase(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var ctx = scope.ServiceProvider.GetRequiredService<SudokuContext>();
            ctx.InitializeDatabase();
        }
    }

    public void InitializeDatabase()
    {
        Database.Migrate();
        InitOrUpdateDatabase();
    }
}