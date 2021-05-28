/*
  This file is part of Sudoku - A library to solve a sudoku.

  Copyright (c) Herbert Aitenbichler

  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
  to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
  and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/

namespace Sudoku.Solve.Serialization
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    public static class SudokuLoadSaveExtensions
    {
        public static bool SaveXml(this Solve.Sudoku sudoku, string fileName)
        {
            using (TextWriter fs = new StreamWriter(fileName))
            {
                var serializer = new XmlSerializer(typeof(SudokuXml));
                var sudokuXml  = sudoku.ToSudokuXml();

                try
                {
                    serializer.Serialize(fs, sudokuXml);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }

            sudoku.Modified = false;
            return true;
        }

        private static Sudoku LoadXml(string fileName)
        {
            SudokuXml sudokuXml;
            // Open the file containing the data that you want to deserialize.
            using (TextReader fs = new StreamReader(fileName))
            {
                var serializer = new XmlSerializer(typeof(SudokuXml));

                try
                {
                    sudokuXml = (SudokuXml)serializer.Deserialize(fs);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }

            return sudokuXml.ToSudoku();
        }

        public static Sudoku Load(string fileName)
        {
            return LoadXml(fileName);
        }
    }
}