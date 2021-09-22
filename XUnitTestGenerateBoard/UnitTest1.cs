using System;
using Xunit;
using GenerateSudoku;
using System.Collections.Generic;

namespace XUnitTestGenerateBoard
{
    public class UnitTest1
    {
        [Fact]
        public void TestCompleteBoardCompleteFillIn()
        {
            GenerateSudoku.GenerateSudoku generate = new GenerateSudoku.GenerateSudoku();
            Sudoku sudoku = new Sudoku { size = 9, squares = new List<Square>().ToArray() };
            sudoku.squares = generate.CompleteBoard(sudoku);

            Assert.Equal(81, sudoku.squares.Length);
            Assert.True(sudoku.Squares.Length == 81);
        }

        [Fact]
        public void TestCompleteBoardPartialFillIn()
        {
            GenerateSudoku.GenerateSudoku generate = new GenerateSudoku.GenerateSudoku();
            Sudoku sudoku = new Sudoku { 
                size = 9,
                squares = new List<Square> {
                    new Square {x = 0, y = 0 , value = 4},
                    new Square {x = 6, y = 7, value = 3 }, 
                    new Square {x = 8, y = 8, value = 9},
                    new Square { x = 4, y = 3, value = 9 }

                    }.ToArray() };

            sudoku.squares = generate.CompleteBoard(sudoku);

            Assert.True(sudoku.Squares[0].X == 0 && sudoku.Squares[0].Y == 0 && sudoku.Squares[0].Value == 4 );
            Assert.True(sudoku.Squares[80].X == 8 && sudoku.Squares[80].Y == 8 && sudoku.Squares[80].Value == 9);
            Assert.Equal(81, sudoku.squares.Length);
            Assert.True(sudoku.Squares.Length == 81);
        }

        [Fact]
        public void TestCompleteFullBoard()
        {
            GenerateSudoku.GenerateSudoku generate = new GenerateSudoku.GenerateSudoku();
            List<Square> squares = new List<Square>();
            Sudoku sudoku = new Sudoku
            {
                size = 9,                
            };
            for (int y = 0; y < 9; y++)
            {
                for(int x =0; x < 9; x++)
                {
                    squares.Add(new Square { x = x, y = y });
                }
            }
            sudoku.squares = squares.ToArray();
            sudoku.squares = generate.CompleteBoard(sudoku);

            Assert.Equal(81, squares.Count);
            Assert.Equal(81, sudoku.squares.Length);
            Assert.True(sudoku.Squares.Length == 81);

        }
    }
}
