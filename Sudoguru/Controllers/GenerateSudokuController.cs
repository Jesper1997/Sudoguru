using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryGenerateSudoku;


namespace Sudoguru.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class GenerateSudokuController : Controller
    {

        [HttpGet]
        [Route("GetSudoku/{size}&{level}")]
        public IActionResult GetSudoku([FromRoute] int size, int level)
        {
            GenerateSudokuFactory factory = new GenerateSudokuFactory();
            var genrate = factory.GetGenerate;
            return Ok(genrate.GetSudoku(size, level));
        }
    }
}
