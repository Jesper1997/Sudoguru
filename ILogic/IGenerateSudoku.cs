using System;
using System.Collections.Generic;
using System.Text;


namespace IGenerateSudoku
{
    public interface IGenerateSudoku
    {
        public ISudoku GetSudoku(int size, int level);
    }
}
