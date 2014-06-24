using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Logic;

namespace Sudoku.Service.BO.Tools
{
    static class BOExtension
    {
        public static Sudoku.Logic.BO.Sudoku ToBO(this Contracts.DTO.Sudoku mythis)
        {
            return new Sudoku.Logic.BO.Sudoku().Copy(mythis);
        }

        public static Contracts.DTO.Sudoku ToDTO(this Sudoku.Logic.BO.Sudoku mythis)
        {
            return new Sudoku.Service.Contracts.DTO.Sudoku().Copy(mythis);
        }

        public static Contracts.DTO.Sudoku Copy(this Contracts.DTO.Sudoku dest, Sudoku.Logic.BO.Sudoku src)
        {
            dest.SudokuID = src.SudokuID;
            dest.Name = src.Name;
            return dest;
        }

        public static Sudoku.Logic.BO.Sudoku Copy(this Sudoku.Logic.BO.Sudoku dest, Contracts.DTO.Sudoku src)
        {
            dest.SudokuID = src.SudokuID;
            dest.Name = src.Name;
            return dest;
        }
    }
}
