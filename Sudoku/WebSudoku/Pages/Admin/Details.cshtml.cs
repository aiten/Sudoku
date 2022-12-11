using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Sudoku.Repository.Abstraction.Entities;

namespace WebSudoku.Pages.Admin
{
    using Framework.Repository;
    using Framework.Repository.Abstraction;

    using Sudoku.Repository.Abstraction;
    using Sudoku.Repository.Context;

    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork       _uow;
        private readonly ISudokuRepository _sudokuRepository;

        public DetailsModel(IUnitOfWork uow, ISudokuRepository sudokuRepository)
        {
            _uow              = uow;
            _sudokuRepository = sudokuRepository;
        }

        public SudokuEntity SudokuEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) // || _uow.SudokuEntity == null)
            {
                return NotFound();
            }

            using (var trans = _uow.BeginTransaction())
            {
                var sudokuentity = await _sudokuRepository.GetAsync(id.Value);
                if (sudokuentity == null)
                {
                    return NotFound();
                }

                SudokuEntity = sudokuentity;
                return Page();
            }
        }
    }
}