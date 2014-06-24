using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Data.Entity;

namespace Sudoku.Logic
{
    public partial class EntityController : IDisposable
    {
        public static void GlobalInitialize()
        {
            Database.SetInitializer(new Repository.SudokuInitializer());


            using (var ctrl = new Logic.EntityController())
            {
                ctrl.TestInit();
            }
        }
        
        private void TestInit()
        {
            var app = new Repository.POCO.Sudoku
            {
                Name  = "Administrator",
            };

            Context.Sudokus.Add(app);
            Context.SaveChanges();
        }

        protected static string ContextTypeName = "Sudoku.Repository.SudokuContext, Sudoku.Repository";

        public Repository.SudokuContext Context
        {
            get;
            protected set;
        }

        public EntityController()
        {
            Context = new Repository.SudokuContext("SudokuSDF");
        }

        public EntityController(Repository.SudokuContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Context = context;
        }

        public void Dispose()
        {
        }
    }
}
