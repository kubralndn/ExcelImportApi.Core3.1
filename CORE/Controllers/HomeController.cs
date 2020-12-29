using BUSINESS_LOGIC.Interfaces;
using BUSINESS_LOGIC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IJson __jsonImport;
        private readonly IProduct _dbImport;
        private readonly IReadData _data;
        public HomeController(IProduct dbImport, IJson jsonImport, IReadData data)
        {
            __jsonImport = jsonImport;
            _dbImport = dbImport;
            _data = data;
        }

        [Route("ImportExcel")]
        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("No file was sent.");
            }
          
            var importData = _data.GetDataWithExcelReader(file);
           
            Stopwatch swDB = Stopwatch.StartNew();
            var importDBCount = await _dbImport.Import(importData);
            swDB.Stop();
           
            await __jsonImport.Import(importData: importData, fileName: file.Name);
  
            return Ok($"{importDBCount} record(s) imported successfully. Time taken: {swDB.Elapsed}");

        }



    }
}

