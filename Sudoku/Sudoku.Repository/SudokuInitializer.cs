using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Sudoku.Repository
{
    public class SudokuInitializer : DropCreateDatabaseAlways<SudokuContext>
    {
        protected override void Seed(SudokuContext context)
        {
            base.Seed(context);

            //context.CreateUniqueIndex("Username", typeof(Entities.User).Name);
            //context.CreateUniqueIndex(new string[] { "From", "To" }, typeof(Entities.Round6outOf45).Name);
        }
    }
}
