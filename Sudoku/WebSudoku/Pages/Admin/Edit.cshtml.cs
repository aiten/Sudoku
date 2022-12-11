using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Sudoku.Repository.Abstraction.Entities;


namespace WebSudoku.Pages.Admin
{
    using Framework.Repository.Abstraction;

    using Sudoku.Repository.Abstraction;
    using Sudoku.Repository.Context;

    public class EditModel : PageModel
    {
        private readonly IUnitOfWork         _uow;
        private readonly ISudokuRepository   _sudokuRepository;
        private readonly ICategoryRepository _categoryRepository;

        public EditModel(IUnitOfWork uow, ISudokuRepository sudokuRepository, ICategoryRepository categoryRepository)
        {
            _uow                = uow;
            _sudokuRepository   = sudokuRepository;
            _categoryRepository = categoryRepository;
        }

        [BindProperty]
        public SudokuEntity SudokuEntity { get; set; } = default!;

        public IEnumerable<SelectListItem> Categories { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) // || _uow.SudokuEntity == null)
            {
                return NotFound();
            }

            using (var trans = _uow.BeginTransaction())
            {
                Categories = await _categoryRepository.GetCategories();

                var sudokuentity = await _sudokuRepository.GetAsync(id.Value);
                if (sudokuentity == null)
                {
                    return NotFound();
                }

                SudokuEntity = sudokuentity;
                return Page();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var trans = _uow.BeginTransaction())
            {
                try
                {
                    SudokuEntity.LastStored = DateTime.Now;
                    _sudokuRepository.SetState(SudokuEntity, MyEntityState.Modified);
                    await _uow.SaveChangesAsync();

                    await trans.CommitTransactionAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SudokuEntityExists(SudokuEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SudokuEntityExists(int id)
        {
            return true; // _sudokuRepository.GetAsync(id);
        }
    }
}