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

namespace Sudoku.Host.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Sudoku.Host.Shared;
    using Sudoku.Solve;
    using Sudoku.Solve.Abstraction;

    [Route("[controller]")]
    public class SudokuController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<SudokuSolveResult>> Get(IEnumerable<string> sudoku)
        {
            await Task.CompletedTask;
            var s    = sudoku.ToArray().CreateSudoku();
            var info = s.GetSolveInfo();

            return Ok(info);
        }

        [HttpGet("next")]
        public async Task<ActionResult<IEnumerable<string>>> SetNext(IEnumerable<string> sudoku, int row, int col)
        {
            await Task.CompletedTask;
            var s = sudoku.ToArray().CreateSudoku();
            s.UpdatePossible();
            s.SetNextPossible(row, col);

            return Ok(s.SmartPrint(string.Empty));
        }

        [HttpGet("set")]
        public async Task<ActionResult<IEnumerable<string>>> SetNext(IEnumerable<string> sudoku, int row, int col, int no)
        {
            await Task.CompletedTask;
            var s = sudoku.ToArray().CreateSudoku();
            s.Set(row, col, no);

            return Ok(s.SmartPrint(string.Empty));
        }

        [HttpGet("solutioncount")]
        public async Task<ActionResult<int>> SolutionCount(IEnumerable<string> sudoku)
        {
            await Task.CompletedTask;
            var s   = sudoku.ToArray().CreateSudoku();
            var cts = new CancellationTokenSource();

            var task = Task.Run(() => { return s.CalcPossibleSolutions(cts.Token); });

            bool isCompletedSuccessfully = task.Wait(TimeSpan.FromMilliseconds(1000));

            if (isCompletedSuccessfully)
            {
                return Ok(task.Result);
            }

            throw new TimeoutException("The function has taken longer than the maximum time allowed.");
        }
    }
}