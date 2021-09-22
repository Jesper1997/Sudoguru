using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using IGenerateSudoku;


namespace GenerateSudoku
{
    public class Sudoku : ISudoku
    {
        [JsonIgnore]
        public ISquare[] Squares => squares;
        [JsonIgnore]
        public bool response { get; set; }
        [JsonIgnore]
        public int size { get; set; }
        public Square[] squares { get; set; }
    }
}
