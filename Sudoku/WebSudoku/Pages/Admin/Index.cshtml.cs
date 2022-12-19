namespace Sudoku.WebSudoku.Pages.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Framework.Repository.Abstraction;

    using Microsoft.AspNetCore.Mvc.RazorPages;

    using Sudoku.Repository.Abstraction;
    using Sudoku.Repository.Abstraction.Entities;

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