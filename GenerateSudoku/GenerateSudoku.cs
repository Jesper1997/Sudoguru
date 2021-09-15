using System;
//for external api
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using IGenerateSudoku;

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
                foreach (Square square in sudokuboard.Squares )
                {
                    square.id = Convert.ToInt32(string.Format("{0}{1}" , square.y, square.x));
                }
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
    }
}
