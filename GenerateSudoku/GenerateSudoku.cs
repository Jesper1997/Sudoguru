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
                Sudoku board = new Sudoku();
                var list = new List<Square>{ 
                //Start first row
                new Square{ x = 0, y = 0, value = 1 },
                new Square{ x = 1, y = 0, value = 0 },
                new Square{ x = 2, y = 0, value = 7 },
                new Square{ x = 3, y = 0, value = 0 },
                new Square{ x = 4, y = 0, value = 0 },
                new Square{ x = 5, y = 0, value = 0 },
                new Square{ x = 6, y = 0, value = 9 },
                new Square{ x = 7, y = 0, value = 8 },
                new Square{ x = 8, y = 0, value = 5 },
                //Start Second row
                new Square{ x = 0, y = 1, value = 0 },
                new Square{ x = 1, y = 1, value = 0 },
                new Square{ x = 2, y = 1, value = 0 },
                new Square{ x = 3, y = 1, value = 6 },
                new Square{ x = 4, y = 1, value = 5 },
                new Square{ x = 5, y = 1, value = 0 },
                new Square{ x = 6, y = 1, value = 0 },
                new Square{ x = 7, y = 1, value = 0 },
                new Square{ x = 8, y = 1, value = 3 },
                //start third row
                new Square{ x = 0, y = 2, value = 3 },
                new Square{ x = 1, y = 2, value = 0 },
                new Square{ x = 2, y = 2, value = 0 },
                new Square{ x = 3, y = 2, value = 1 },
                new Square{ x = 4, y = 2, value = 0 },
                new Square{ x = 5, y = 2, value = 0 },
                new Square{ x = 6, y = 2, value = 0 },
                new Square{ x = 7, y = 2, value = 6 },
                new Square{ x = 8, y = 2, value = 0 },
                //Start fourth row
                new Square{ x = 0, y = 3, value = 0 },
                new Square{ x = 1, y = 3, value = 0 },
                new Square{ x = 2, y = 3, value = 0 },
                new Square{ x = 3, y = 3, value = 0 },
                new Square{ x = 4, y = 3, value = 7 },
                new Square{ x = 5, y = 3, value = 0 },
                new Square{ x = 6, y = 3, value = 0 },
                new Square{ x = 7, y = 3, value = 0 },
                new Square{ x = 8, y = 3, value = 4 },
                //start fhith row
                new Square{ x = 0, y = 4, value = 5 },
                new Square{ x = 1, y = 4, value = 0 },
                new Square{ x = 2, y = 4, value = 0 },
                new Square{ x = 3, y = 4, value = 2 },
                new Square{ x = 4, y = 4, value = 0 },
                new Square{ x = 5, y = 4, value = 0 },
                new Square{ x = 6, y = 4, value = 0 },
                new Square{ x = 7, y = 4, value = 1 },
                new Square{ x = 8, y = 4, value = 0 },
                //Start sixth row
                new Square{ x = 0, y = 5, value = 2 },
                new Square{ x = 1, y = 5, value = 0 },
                new Square{ x = 2, y = 5, value = 0 },
                new Square{ x = 3, y = 5, value = 0 },
                new Square{ x = 4, y = 5, value = 4 },
                new Square{ x = 5, y = 5, value = 0 },
                new Square{ x = 6, y = 5, value = 5 },
                new Square{ x = 7, y = 5, value = 0 },
                new Square{ x = 8, y = 5, value = 0 },
                //start seventh row
                new Square{ x = 0, y = 6, value = 0 },
                new Square{ x = 1, y = 6, value = 5 },
                new Square{ x = 2, y = 6, value = 0 },
                new Square{ x = 3, y = 6, value = 4 },
                new Square{ x = 4, y = 6, value = 0 },
                new Square{ x = 5, y = 6, value = 0 },
                new Square{ x = 6, y = 6, value = 8 },
                new Square{ x = 7, y = 6, value = 0 },
                new Square{ x = 8, y = 6, value = 0 },
                //start eigth row
                new Square{ x = 0, y = 7, value = 8 },
                new Square{ x = 1, y = 7, value = 0 },
                new Square{ x = 2, y = 7, value = 0 },
                new Square{ x = 3, y = 7, value = 7 },
                new Square{ x = 4, y = 7, value = 0 },
                new Square{ x = 5, y = 7, value = 5 },
                new Square{ x = 6, y = 7, value = 0 },
                new Square{ x = 7, y = 7, value = 0 },
                new Square{ x = 8, y = 7, value = 6 },
                //Start last Row
                new Square{ x = 0, y = 8, value = 0 },
                new Square{ x = 1, y = 8, value = 0 },
                new Square{ x = 2, y = 8, value = 6 },
                new Square{ x = 3, y = 8, value = 9 },
                new Square{ x = 4, y = 8, value = 0 },
                new Square{ x = 5, y = 8, value = 8 },
                new Square{ x = 6, y = 8, value = 3 },
                new Square{ x = 7, y = 8, value = 5 },
                new Square{ x = 8, y = 8, value = 7 }
                };
                foreach(Square square in list)
                {
                    square.id = createid(square.x, square.y);
                }
                board.squares = list.ToArray();
                //TODO Generate the board by own code incase of failure
                return board;
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
            int x = 0;
            int y = 0;

            int numberofbasesquares = sudoku.squares.Length;
            int numberchecksquare = 0;
            //De gegevens die je hebt gekregen opvolorden zetten
            board = sudoku.squares.OrderBy(s => s.y).ThenBy(s => s.x).ToList();
            while(boardLength < maxsize)
            {
                if (numberchecksquare < numberofbasesquares)
                {
                    if (board[numberchecksquare].x == x && board[numberchecksquare].y == y)
                    {
                        board[numberchecksquare].id = createid(x, y);
                        numberchecksquare++;
                    }
                    else
                    {  
                        AddNewSquare(board, x, y);
                    }
                }
                else
                {
                    AddNewSquare(board, x, y);
                }

                x++;
                boardLength++;
                if(x == sudoku.size)
                {
                    y++;
                    x = 0;
                }

            }
            return board.OrderBy(s => s.y).ThenBy(s => s.x).ToArray(); ;
        }

        private void AddNewSquare(List<Square> board, int x, int y)
        {
            board.Add(new Square { x = x, y = y, id = createid(x, y) });
        }

        private int createid(int x, int y)
        {
            return Convert.ToInt32(string.Format("{0}{1}", y, x));
        }
    }
}
