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

namespace Sudoku.Solve;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class Sudoku
{
    #region Private Data

    private readonly SudokuField[,] _fields      = new SudokuField[9, 9];
    private readonly string[]       _userNoteCol = new string[9];
    private readonly string[]       _userNoteRow = new string[9];

    private int _setCount;

    #endregion

    #region Constructor / Initalisation / properties

    public Sudoku()
    {
        for (var row = 0; row < 9; row++)
        {
            for (var col = 0; col < 9; col++)
            {
                _fields[row, col] = new SudokuField()
                {
                    AbsCol = col,
                    AbsRow = row
                };
            }
        }
    }

    public int  StepCount => _setCount;
    public bool Modified  { get; internal set; }

    #endregion

    #region Get Field Information

    public int Get(int row, int col)
    {
        return GetDef(row, col).No;
    }

    public SudokuField GetDef(int row, int col)
    {
        return _fields[row, col];
    }

    public delegate SudokuField GetSudokuField(int row, int col);

    public SudokuField GetSudokuFieldRow(int row, int col)
    {
        return GetDef(row, col);
    }

    public SudokuField GetSudokuFieldCol(int row, int col)
    {
        return GetDef(col, row);
    }

    public SudokuField GetSudokuFieldS3(int row, int col)
    {
        var s3 = ConvertToS3(row, col);
        return _fields[s3.Row, s3.Col];
    }

    public GetSudokuField ToGetDef(Orientation orientation)
    {
        return orientation switch
        {
            Orientation.Column => GetSudokuFieldCol,
            Orientation.Row    => GetSudokuFieldRow,
            Orientation.X3     => GetSudokuFieldS3,
            _                  => throw new ArgumentException()
        };
    }

    public static (int Row, int Col) ConvertToRow(int row, int col)
    {
        return (row, col);
    }

    public static (int Row, int Col) ConvertToCol(int row, int col)
    {
        return (col, row);
    }

    public static (int Row, int Col) ConvertToS3(int row, int col)
    {
        var rowX3 = (row / 3) * 3;
        var colX3 = (row % 3) * 3;

        var dRow = col / 3;
        var dCol = col % 3;

        return (rowX3 + dRow, colX3 + dCol);
    }

    public (int row, int col) ConvertTo(Orientation orientation, (int row, int col) absRowCol)
    {
        return orientation switch
        {
            Orientation.Column => ConvertToCol(absRowCol.row, absRowCol.col),
            Orientation.Row    => ConvertToRow(absRowCol.row, absRowCol.col),
            Orientation.X3     => ConvertToS3(absRowCol.row, absRowCol.col),
            _                  => throw new ArgumentException()
        };
    }

    public static (int Row, int Col) ConvertFromRow(int row, int col)
    {
        return (row, col);
    }

    public static (int Row, int Col) ConvertFromCol(int row, int col)
    {
        return (col, row);
    }

    public static (int Row, int Col) ConvertFromS3(int row, int col)
    {
        return ((row / 3) * 3 + col / 3, (row % 3) * 3 + (col % 3));
    }

    #endregion

    #region Set and Validation

    public bool SetNextPossible(int row, int col)
    {
        var def = GetDef(row, col);
        var no  = def.No;

        if (def.IsEmpty)
        {
            var only = def.GetPossibleNos().FirstOrDefault();
            if (only > 0 && Set(row, col, only))
            {
                return true;
            }
        }

        for (var i = 1; i <= 10; i++)
        {
            var newNo = no + i;

            if (newNo > 9) newNo -= 10;
            if (Set(row, col, newNo))
            {
                return true;
            }
        }

        Clear(row, col);
        return false;
    }

    public bool Set(int row, int col, int no)
    {
        if (no < 0 || no > 9)
            return false;

        if (!CanSet(row, col, no))
        {
            return false;
        }

        var def = GetDef(row, col);
        var old = def.No;

        def.No = no;

        AddUndo(row, col, old);

        if ((old == 0 && no > 0))
        {
            _setCount++;
        }
        else if (old > 0 && no == 0)
        {
            _setCount--;
        }

        return true;
    }

    public bool Clear(int row, int col)
    {
        return Set(row, col, 0);
    }

    private bool CanSet(int idx, int no, GetSudokuField getDef)
    {
        for (var i = 0; i < 9; i++)
        {
            if (getDef(idx, i).No == no)
            {
                return false;
            }
        }

        return true;
    }

    public bool CanSet(int row, int col, int no)
    {
        return no == 0 ||
               (CanSet(row,                     no, GetSudokuFieldRow) &&
                CanSet(col,                     no, GetSudokuFieldCol) &&
                CanSet((row / 3) * 3 + col / 3, no, GetSudokuFieldS3));
    }

    private bool SetNoUndo(int row, int col, int no)
    {
        var old = UndoAvailable;
        UndoAvailable = false;
        var ret = Set(row, col, no);
        UndoAvailable = old;
        return ret;
    }

    #endregion

    #region Undo / Redo

    private class UndoInfo
    {
        public int Row;
        public int Col;
        public int No;
    };

    private readonly List<UndoInfo> _undoList = new List<UndoInfo>();

    public bool UndoAvailable { get; set; } = false;

    public bool Undo()
    {
        if (CanUndo())
        {
            var undo = _undoList.Last();
            SetNoUndo(undo.Row, undo.Col, undo.No);
            _undoList.RemoveAt(_undoList.Count - 1);
            return true;
        }

        return false;
    }

    public bool CanUndo()
    {
        return _undoList.Count > 0;
    }

    public void AddUndo(int row, int col, int no)
    {
        if (UndoAvailable)
        {
            _undoList.Add(new UndoInfo
            {
                Row = row,
                Col = col,
                No  = no
            });
            Modified = true;
        }
    }

    public void ClearUndo()
    {
        _undoList.Clear();
    }

    #endregion

    #region UserNote

    public void SetUserNote(int row, int col, string userNote)
    {
        var def = GetDef(row, col);
        if (String.Compare(def.UserNote, userNote, StringComparison.Ordinal) != 0)
            Modified = true;
        def.SetUserNote(userNote);
    }

    public string GetUserNoteRow(int row)
    {
        return _userNoteRow[row] ?? "";
    }

    public string GetUserNoteCol(int col)
    {
        return _userNoteCol[col] ?? "";
    }

    public void SetUserNoteRow(int row, string userNote)
    {
        var old = GetUserNoteRow(row);
        if (String.Compare(old, userNote, StringComparison.Ordinal) != 0)
            Modified = true;
        _userNoteRow[row] = userNote;
    }

    public void SetUserNoteCol(int col, string userNote)
    {
        var old = GetUserNoteCol(col);
        if (String.Compare(old, userNote, StringComparison.Ordinal) != 0)
            Modified = true;
        _userNoteCol[col] = userNote;
    }

    #endregion

    #region Calc Possibilities

    private void CommitChanges()
    {
        for (var row = 0; row < 9; row++)
        {
            for (var col = 0; col < 9; col++)
            {
                GetSudokuFieldCol(row, col).CommitChanges();
            }
        }
    }

    private bool HavePossibility()
    {
        // Update with sudoku (mainRule) rules

        for (var row = 0; row < 9; row++)
        {
            for (var col = 0; col < 9; col++)
            {
                var def = GetDef(row, col);

                if (def.IsEmpty && def.PossibleCount() == 1)
                    return true;
            }
        }

        return false;
    }

    public void UpdatePossible()
    {
        // Update with sudoku (mainRule) rules

        for (var row = 0; row < 9; row++)
        {
            for (var col = 0; col < 9; col++)
            {
                var def = GetDef(row, col);
                def.InitHelpVar();

                if (def.IsEmpty)
                {
                    for (var z = 1; z <= 9; z++)
                    {
                        if (SetNoUndo(row, col, z))
                        {
                            def.MainRulePossible[z - 1] = true;
                        }
                    }

                    SetNoUndo(row, col, 0);
                }
            }
        }

        CommitChanges();

        var solverB1 = new SolverBlockade1(this);

        var solverBase = new SolverBase[]
        {
            new SolverBlockade3(this),
            new SolverBlockade2(this),
            new SolverBlockade2SubSet(this),
        };

        var solverExtended = new SolverBase[]
        {
            new SolverXWing(this),
            new SolverSwordfish(this),
            new SolverJellyfish(this),
            new SolverWWing(this),
            new SolverXYWing(this),
            new SolverXYZWing(this),
        };

        solverB1.Solve(Orientation.X3);

        CommitChanges();

        while (!HavePossibility())
        {
            solverB1.Solve();

            CommitChanges();

            if (HavePossibility())
                break;

            var changeCount = 0;

            foreach (var solver in solverBase)
            {
                if (solver.Solve()) changeCount++;
            }

            // only use extended as last option if no other works

            foreach (var solver in solverExtended)
            {
                if (changeCount == 0 && solver.Solve()) changeCount++;
            }

            CommitChanges();

            if (changeCount == 0)
                break;
        }
    }

    #endregion

    #region Other public User Functions

    public bool Finish()
    {
        var dummy = 0;
        ClearUndo();
        return _CalcPossibleSolutions(-1, 9, true, ref dummy, new CancellationToken()) == 1;
    }

    #endregion

    #region Calc Possibile Solutions (BackTracking)

    public delegate void FoundSolutionHandler(object sudoku, SudokuEventArgs solutionInfo);

    public event FoundSolutionHandler? FoundSolution;

    protected virtual void OnFoundSolution(object sudoku, SudokuEventArgs solutionInfo)
    {
        if (FoundSolution != null)
        {
            FoundSolution(sudoku, solutionInfo);
        }
    }

    private volatile bool    _endCalc    = false;
    private          Sudoku? _calcSudoku = null;

    public int CalcPossibleSolutions(CancellationToken cancellationToken)
    {
        OnFoundSolution(this, new SudokuEventArgs(0));

        _endCalc                  = false;
        _calcSudoku               = this.Clone();
        _calcSudoku.FoundSolution = FoundSolution;
        var possibleSolutions = 0;
        var ret               = _calcSudoku._CalcPossibleSolutions(-1, 9, false, ref possibleSolutions, cancellationToken);
        _calcSudoku = null;
        OnFoundSolution(this, new SudokuEventArgs(ret, true));
        return ret;
    }

    public void StopPossibleSolutions()
    {
        _endCalc = true;
        if (_calcSudoku != null)
            _calcSudoku.StopPossibleSolutions();
    }

    private int _CalcPossibleSolutions(int row, int col, bool calcEnd, ref int possibleSolutions, CancellationToken cancellationToken)
    {
        if (!calcEnd && _endCalc)
            return -1;

        if (StepCount >= 9 * 9)
        {
            possibleSolutions = possibleSolutions > 1 ? possibleSolutions : 1;

            if (!cancellationToken.IsCancellationRequested)
            {
                OnFoundSolution(this, new SudokuEventArgs(possibleSolutions));
            }

            return 1;
        }


        var results = 0;
        int myRow;
        int myCol;
        col++;
        if (col >= 9)
        {
            row++;
            col = 0;
        }

        if (row > 8)
        {
            possibleSolutions = possibleSolutions > 1 ? possibleSolutions : 1;
            return 1;
        }

        for (myRow = row; myRow < 9; myRow++)
        {
            for (myCol = col; myCol < 9; myCol++)
            {
                var no = Get(myRow, myCol);

                if (no == 0)
                {
                    //int old = no;
                    for (var newNo = 1; newNo <= 9; newNo++)
                    {
                        if (SetNoUndo(myRow, myCol, newNo))
                        {
                            if (cancellationToken.IsCancellationRequested)
                            {
                                return -1;
                            }

                            var res = _CalcPossibleSolutions(myRow, myCol, calcEnd, ref possibleSolutions, cancellationToken);
                            if (res < 0)
                            {
                                if (!calcEnd)
                                    SetNoUndo(myRow, myCol, 0);
                                return res; // abort
                            }

                            if (res > 0)
                            {
                                if (calcEnd)
                                {
                                    return 1;
                                }

                                results           += res;
                                possibleSolutions =  possibleSolutions > results ? possibleSolutions : results;
                            }
                        }
                    }

                    SetNoUndo(myRow, myCol, 0);
                    return results;
                }
            }

            col = 0;
        }

        return 0;
    }

    #endregion
}