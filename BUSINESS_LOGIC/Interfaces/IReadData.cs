using BUSINESS_LOGIC.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC.Interfaces
{
    public interface IReadData
    {
        /// <summary>
        /// Read Data from File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        List<ImportDataViewModel> GetDataWithExcelReader(IFormFile file);
    }
}
