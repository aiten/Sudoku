using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Sudoku.Repository
{
    public class SudokuContext : DbContext
    {
        public SudokuContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Die Namingkonvention von für den Tabellenplural entfernen. 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<POCO.Sudoku> Sudokus { get; set; }
    }
}
