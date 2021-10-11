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
