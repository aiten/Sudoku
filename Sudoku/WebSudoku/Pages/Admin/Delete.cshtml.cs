namespace Sudoku.WebSudoku.Pages.Admin
{
    using System.Threading.Tasks;

    using Framework.Repository.Abstraction;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using Sudoku.Repository.Abstraction;
    using Sudoku.Repository.Abstraction.Entities;

    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork       _uow;
        private readonly ISudokuRepository _sudokuRepository;

        public DeleteModel(IUnitOfWork uow, ISudokuRepository sudokuRepository)
        {
            _uow              = uow;
            _sudokuRepository = sudokuRepository;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) // || _uow.SudokuEntity == null)
            {
                return NotFound();
            }


            using (var trans = _uow.BeginTransaction())
            {
                var sudokuentity = await _sudokuRepository.GetTrackingAsync(id.Value);

                if (sudokuentity != null)
                {
                    SudokuEntity = sudokuentity;
                    _sudokuRepository.Delete(SudokuEntity);
                    await _uow.SaveChangesAsync();
                }

                await trans.CommitTransactionAsync();

                return RedirectToPage("./Index");
            }
        }
    }
}