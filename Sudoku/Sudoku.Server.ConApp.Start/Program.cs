using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku.Server;

namespace Sudoku.Server.ConApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SudokuServer server = new SudokuServer();

            server.Start();

            Console.WriteLine("Server is running\nPress Enter to stop server");
            Console.ReadLine();

            server.Stop();
        }
    }
}
