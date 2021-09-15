using System;
using System.Collections.Generic;
using System.Text;

namespace ISudokuBoard
{
    public interface ISudoku
    {
        public ISquare[] Squares { get; }
    }
}
