namespace Sudoku.WebUi.Areas.Data.Pages.Sudokus;

using System.Threading.Tasks;

using Framework.Repository.Abstraction;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Sudoku.Repository.Abstraction;
using Sudoku.Repository.Abstraction.Entities;
using Sudoku.Shared;

[Authorize(Roles = SudokuConst.Role_User)]
public class DetailsModel : PageModel
{
    private readonly IUnitOfWork       _uow;
    private readonly ISudokuRepository _sudokuRepository;

    public DetailsModel(IUnitOfWork uow, ISudokuRepository sudokuRepository)
    {
        _uow              = uow;
        _sudokuRepository = sudokuRepository;
    }

    public SudokuEntity SudokuEntity { get; set; } = default!;

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