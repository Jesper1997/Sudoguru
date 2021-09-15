﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using IGenerateSudoku;


namespace GenerateSudoku
{
    class Sudoku : ISudoku
    {
        [JsonIgnore]
        public ISquare[] Squares => squares;

        public bool response { get; set; }
        public int size { get; set; }
        //[JsonIgnore]
        public Square[] squares { get; set; }
    }
}
