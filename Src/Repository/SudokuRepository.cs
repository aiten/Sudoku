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

namespace Sudoku.Repository;

using System.Linq;

using Framework.Repository;

using Microsoft.EntityFrameworkCore;

using Sudoku.Repository.Abstraction;
using Sudoku.Repository.Abstraction.Entities;
using Sudoku.Repository.Context;

public class SudokuRepository : CrudRepository<SudokuContext, SudokuEntity, int>, ISudokuRepository
{
    #region ctr/default/overrides

    public SudokuRepository(SudokuContext context) : base(context)
    {
    }

    protected override FilterBuilder<SudokuEntity, int> FilterBuilder =>
        new()
        {
            PrimaryWhere   = (query, key) => query.Where(item => item.Id == key),
            PrimaryWhereIn = (query, keys) => query.Where(item => keys.Contains(item.Id))
        };

    protected override IQueryable<SudokuEntity> AddInclude(IQueryable<SudokuEntity> query, params string[] includeProperties)
    {
        return query.Include(x => x.Category);
    }

    #endregion
}