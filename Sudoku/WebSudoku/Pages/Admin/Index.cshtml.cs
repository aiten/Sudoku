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
    using Framework.Repository.Abstraction;

    using Sudoku.Repository.Abstraction;

    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork       _uow;
        private readonly ISudokuRepository _sudokuRepository;

        public IndexModel(IUnitOfWork uow, ISudokuRepository sudokuRepository)
        {
            _uow              = uow;
            _sudokuRepository = sudokuRepository;
        }

        public IList<SudokuEntity> SudokuEntity { get; set; } = default!;

        public async Task OnGetAsync()
        {
            using (var trans = _uow.BeginTransaction())
            {
                SudokuEntity = await _sudokuRepository.GetAllAsync();
            }
        }
    }
}