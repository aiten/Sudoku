namespace WebSudoku.Pages.SolveSudoku
{
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using Sudoku.Solve.Abstraction;

    public class IndexModel : PageModel
    {
        public IndexModel(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        private IHttpClientFactory _httpFactory;

        public HttpClient Http => _httpFactory.CreateClient("pi");

        public async Task OnGetAsync(string sudoku)
        {
            if (string.IsNullOrEmpty(sudoku))
            {
                Sudoku = new List<string>
                {
                    "1, ,8,5, , ,2,3,4",
                    "5, , ,3, ,2,1,7,8",
                    " , , ,8, , ,5,6,9",
                    "8, , ,6, ,5,7,9,3",
                    " , ,5,9, , ,4,8,1",
                    "3, , , , ,8,6,5,2",
                    "9,8, ,2, ,6,3,1, ",
                    " , , , , , ,8, , ",
                    " , , ,7,8, ,9, , ",
                };
            }
            else
            {
                Sudoku = sudoku.Split('|');
            }

            await StartCalc();
        }

        public IEnumerable<string> Sudoku { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SudokuAsString => string.Join('|', Sudoku);

        public SudokuSolveResult? _sudokuResult;
        public int?               SolutionCount { get; set; }

        private string ToFirsteQuery(IEnumerable<string> sudoku)
        {
            return "?sudoku=" + string.Join("&sudoku=", sudoku);
        }

        private async Task<SudokuSolveResult?> GetResult(IEnumerable<string> sudoku)
        {
            try
            {
                return await Http.GetFromJsonAsync<SudokuSolveResult>("Sudoku" + ToFirsteQuery(Sudoku));
            }
            catch (Exception exception)
                //    catch (AccessTokenNotAvailableException exception)
            {
                //            exception.Redirect();
                return null;
            }
        }

        private async Task StartCalc()
        {
            _sudokuResult = await GetResult(Sudoku);
            await GetSolutionCount(); // do not wait
        }

        public async Task OnPostNewSudoku()
        {
            Sudoku = new[] { "" };
            await StartCalc();
        }

        public async Task OnPostNextNo(string sudoku, int row, int col)
        {
            try
            {
                Sudoku = (sudoku??"").Split('|');
                var query = ToFirsteQuery(Sudoku);
                query  += $"&row={row}&col={col}";
                Sudoku =  (await Http.GetFromJsonAsync<IEnumerable<string>>("Sudoku/next" + query))!;
                await StartCalc();
            }
            catch (Exception exception)
            {
            }
        }

        public async Task OnSetNextNo(int row, int col, int no)
        {
            try
            {
                var query = ToFirsteQuery(Sudoku);
                query  += $"&row={row}&col={col}&no={no}";
                Sudoku =  (await Http.GetFromJsonAsync<IEnumerable<string>>("Sudoku/set" + query))!;
                await StartCalc();
            }
            catch (Exception exception)
            {
            }
        }

        private async Task GetSolutionCount()
        {
            try
            {
                SolutionCount = null;
                var query         = ToFirsteQuery(Sudoku);
                var solutionCount = await Http.GetFromJsonAsync<int>("Sudoku/solutioncount" + query);
                SolutionCount = solutionCount;
            }
            catch (Exception exception)
            {
            }
        }

        public async Task OnMyKeyDown(KeyboardEventArgs eventArgs)
        {
            if (eventArgs != null && uint.TryParse(eventArgs.Key, out uint code))
            {
                //await OnSetNo.InvokeAsync((int)code);
            }
        }
    }
}