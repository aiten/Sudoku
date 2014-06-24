using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Repository;

namespace Sudoku.Logic.POCO.Tools
{
    static class POCOExtension
    {
        public static Sudoku.Logic.BO.Sudoku ToBO(this Sudoku.Repository.POCO.Sudoku mythis)
        {
            return new Sudoku.Logic.BO.Sudoku().Copy(mythis);
        }

        public static Sudoku.Repository.POCO.Sudoku ToPOCO(this Sudoku.Logic.BO.Sudoku mythis)
        {
            return new Sudoku.Repository.POCO.Sudoku().Copy(mythis);
        }

        public static Sudoku.Repository.POCO.Sudoku Copy(this Sudoku.Repository.POCO.Sudoku dest, BO.Sudoku src)
        {
            dest.SudokuID = src.SudokuID;
            dest.Name = src.Name;
            return dest;
        }

        public static BO.Sudoku Copy(this BO.Sudoku dest, Sudoku.Repository.POCO.Sudoku src)
        {
            dest.SudokuID = src.SudokuID;
            dest.Name = src.Name;
            return dest;
        }
    }
}
