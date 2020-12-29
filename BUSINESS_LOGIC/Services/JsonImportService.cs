using BUSINESS_LOGIC.Dtos;
using BUSINESS_LOGIC.Interfaces;
using Jil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC.Services
{
    public class JsonImportService : IJson
    {

        public async Task<int> Import(List<ImportDataViewModel> importData, string fileName)
        { 
            try
            {
                string jsonDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImportedFiles");

                if (!Directory.Exists(jsonDirectory))
                {
                    Directory.CreateDirectory(jsonDirectory);
                }

                string jsonFilePath = Path.Combine(jsonDirectory, fileName + "_" + DateTime.Now.Ticks.ToString() + ".json");

                //string json = JsonConvert.SerializeObject(importData);

                string json = JSON.Serialize(importData);

                await System.IO.File.WriteAllTextAsync(jsonFilePath, json);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return importData.Count;
        }

    }
}
