using System;
using System.Collections.Generic;
using System.Text;

namespace IGenerateSudoku
{
    public interface ISudoku
    {
        public ISquare[] Squares { get; }
    }
}
