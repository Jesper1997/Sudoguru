using IGenerateSudoku;
using GenerateSudoku;
namespace FactoryGenerateSudoku
{
    public class GenerateSudokuFactory
    {
        public IGenerateSudoku.IGenerateSudoku GetGenerate => new GenerateSudoku.GenerateSudoku();
    }
}
