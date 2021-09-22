using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using IGenerateSudoku;
using System.Collections.Generic;
using System.Linq;

namespace GenerateSudoku
{
    public class GenerateSudoku : IGenerateSudoku.IGenerateSudoku
    {
        private const string URL = "http://www.cs.utep.edu/cheon/ws/sudoku/new/";

        public ISudoku GetSudoku(int size, int level)
        {
            Sudoku sudokuboard = GenerateSudokuFromExternalAPI(size,level);
            if(sudokuboard != null)
            {
                sudokuboard.squares = CompleteBoard(sudokuboard);
                return sudokuboard;
            }
            else
            {
                //TODO Generate the board by own code incase of failure
                return null;
            }
        }

        private Sudoku GenerateSudokuFromExternalAPI(int size, int level)
        {
            string urlParameters = "?size=" + size.ToString() + "&level="+level.ToString();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            //add and accpet header for json response
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //List data response
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                // parse the reponse body 
                Sudoku sudoku = JsonConvert.DeserializeObject<Sudoku>(response.Content.ReadAsStringAsync().Result);
                //Importent to prevent masive build of aloto of clients
                client.Dispose();
                return sudoku;
            }
            else
            {
                return null;
            }
        }

        public Square[] CompleteBoard(Sudoku sudoku)
        {   
            int maxsize = sudoku.size * sudoku.size;
            List<Square> board = new List<Square>();
            int boardLength = board.Count;
            bool exsist = false;
            int x = 0;
            int y = 0;
            while(boardLength < maxsize)
            {
                exsist = false;
                if (sudoku.squares != null)
                {
                    foreach (Square square in sudoku.squares)
                    {
                        if (x == square.x && y == square.y)
                        {
                            square.id = createid(x, y);
                            if (boardLength == 0)
                            {
                                board.Add(square);
                            }
                            else
                            {
                                board.Add( square);
                            }
                            exsist = true;
                            break;
                        }
                    }
                }

                if(!exsist)
                {
                    if (boardLength == 0)
                    {
                        board.Add(new Square { x = x, y = y, id = createid(x, y) });
                    }
                    else
                    {
                        board.Add(new Square { x = x, y = y, id = createid(x, y) });
                    }

                }

                x++;

                boardLength = board.Count;
                if(x == sudoku.size)
                {
                    y++;
                    x = 0;
                }

            }
            board.OrderBy(s => s.x).ThenBy(s => s.y);
            return board.ToArray(); ;
        }

        private int createid(int x, int y)
        {
            return Convert.ToInt32(string.Format("{0}{1}", y, x));
        }
    }
}
