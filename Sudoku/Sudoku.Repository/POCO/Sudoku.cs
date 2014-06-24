using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Repository.POCO
{
    public partial class Sudoku
    {
        [Key]
        public int SudokuID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
