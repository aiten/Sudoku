namespace Sudoku.WebSudoku.Pages.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Framework.Repository.Abstraction;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Sudoku.Repository.Abstraction;
    using Sudoku.Repository.Abstraction.Entities;

    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork         _uow;
        private readonly ISudokuRepository   _sudokuRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateModel(IUnitOfWork uow, ISudokuRepository sudokuRepository, ICategoryRepository categoryRepository)
        {
            _uow                = uow;
            _sudokuRepository   = sudokuRepository;
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<SelectListItem> Categories { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            using (var trans = _uow.BeginTransaction())
            {
                Categories = await _categoryRepository.GetCategories();
            }

            return Page();
        }

        [BindProperty]
        public SudokuEntity SudokuEntity { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var trans = _uow.BeginTransaction())
            {
                SudokuEntity.LastStored = DateTime.Now;
                await _sudokuRepository.AddAsync(SudokuEntity);
                await _uow.SaveChangesAsync();
                await trans.CommitTransactionAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}