using System;
using System.Collections.Generic;
using System.Text;

namespace ISudokuBoard
{
    public interface ISquare
    {
        public int X { get; }
        public int Y { get; }
        public int Value { get; }
    }
}
