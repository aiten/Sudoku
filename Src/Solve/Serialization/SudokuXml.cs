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

namespace Sudoku.Solve.Serialization;

using System.Linq;
using System.Xml.Serialization;

public class SudokuElementXml
{
    public string XmlUserNote { get; set; } = string.Empty;
    public int    XmlNo       { get; set; }

    public bool ShouldSerializeXmlUserNote() => !string.IsNullOrEmpty(XmlUserNote);
    public bool ShouldSerializeXmlNo()       => XmlNo != 0;

    public bool ShouldSerialize() => ShouldSerializeXmlUserNote() || ShouldSerializeXmlNo();
}

public class Sudoku3X3Xml
{
    public SudokuElementXml XmlSudoku00 { get; set; } = new();
    public SudokuElementXml XmlSudoku01 { get; set; } = new();
    public SudokuElementXml XmlSudoku02 { get; set; } = new();

    public SudokuElementXml XmlSudoku10 { get; set; } = new();
    public SudokuElementXml XmlSudoku11 { get; set; } = new();
    public SudokuElementXml XmlSudoku12 { get; set; } = new();

    public SudokuElementXml XmlSudoku20 { get; set; } = new();
    public SudokuElementXml XmlSudoku21 { get; set; } = new();
    public SudokuElementXml XmlSudoku22 { get; set; } = new();

    public bool ShouldSerializeXmlSudoku00() => XmlSudoku00.ShouldSerialize();
    public bool ShouldSerializeXmlSudoku01() => XmlSudoku01.ShouldSerialize();
    public bool ShouldSerializeXmlSudoku02() => XmlSudoku02.ShouldSerialize();

    public bool ShouldSerializeXmlSudoku10() => XmlSudoku10.ShouldSerialize();
    public bool ShouldSerializeXmlSudoku11() => XmlSudoku11.ShouldSerialize();
    public bool ShouldSerializeXmlSudoku12() => XmlSudoku12.ShouldSerialize();

    public bool ShouldSerializeXmlSudoku20() => XmlSudoku20.ShouldSerialize();
    public bool ShouldSerializeXmlSudoku21() => XmlSudoku21.ShouldSerialize();
    public bool ShouldSerializeXmlSudoku22() => XmlSudoku22.ShouldSerialize();

    public bool ShouldSerialize() => ShouldSerializeXmlSudoku00() || ShouldSerializeXmlSudoku01() || ShouldSerializeXmlSudoku02() ||
                                     ShouldSerializeXmlSudoku10() || ShouldSerializeXmlSudoku11() || ShouldSerializeXmlSudoku12() ||
                                     ShouldSerializeXmlSudoku20() || ShouldSerializeXmlSudoku21() || ShouldSerializeXmlSudoku22();
}

[XmlRoot(ElementName = "Sudoku")]
public class SudokuXml
{
    public int Version { get; set; }

    public string[] XmlUserNoteCol { get; set; } = new string[0];
    public string[] XmlUserNoteRow { get; set; } = new string[0];

    public Sudoku3X3Xml XmlSudoku00 { get; set; } = new();
    public Sudoku3X3Xml XmlSudoku01 { get; set; } = new();
    public Sudoku3X3Xml XmlSudoku02 { get; set; } = new();

    public Sudoku3X3Xml XmlSudoku10 { get; set; } = new();
    public Sudoku3X3Xml XmlSudoku11 { get; set; } = new();
    public Sudoku3X3Xml XmlSudoku12 { get; set; } = new();

    public Sudoku3X3Xml XmlSudoku20 { get; set; } = new();
    public Sudoku3X3Xml XmlSudoku21 { get; set; } = new();
    public Sudoku3X3Xml XmlSudoku22 { get; set; } = new();

    public bool ShouldSerializeXmlUserNoteCol() => XmlUserNoteCol.Any(usernote => !string.IsNullOrEmpty(usernote));
    public bool ShouldSerializeXmlUserNoteRow() => XmlUserNoteRow.Any(usernote => !string.IsNullOrEmpty(usernote));

    public bool ShouldSerializeXmlSudoku00() => XmlSudoku00.ShouldSerialize();
    public bool ShouldSerializeXmlSudoku01() => XmlSudoku01.ShouldSerialize();
    public bool ShouldSerializeXmlSudoku02() => XmlSudoku02.ShouldSerialize();

    public bool ShouldSerializeXmlSudoku10() => XmlSudoku10.ShouldSerialize();
    public bool ShouldSerializeXmlSudoku11() => XmlSudoku11.ShouldSerialize();
    public bool ShouldSerializeXmlSudoku12() => XmlSudoku12.ShouldSerialize();

    public bool ShouldSerializeXmlSudoku20() => XmlSudoku20.ShouldSerialize();
    public bool ShouldSerializeXmlSudoku21() => XmlSudoku21.ShouldSerialize();
    public bool ShouldSerializeXmlSudoku22() => XmlSudoku22.ShouldSerialize();
}