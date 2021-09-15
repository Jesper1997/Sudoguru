using IGenerateSudoku;
using System.Text.Json.Serialization;

namespace GenerateSudoku
{
    public class Square : ISquare
    {
        [JsonIgnore]
        public int X => x;
        [JsonIgnore]
        public int Y => y;
        [JsonIgnore]
        public int Value => value;

        public int id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int value { get; set; }
    }
}
