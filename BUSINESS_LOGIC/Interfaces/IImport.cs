using BUSINESS_LOGIC.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC.Interfaces
{
    public interface IImport
    {
		/// <summary>
		/// Insert data into Products table and create Articles and Colors if they don't exist
		/// </summary>
		/// <param name="importData"></param>
		/// <returns></returns>
		Task<int> Import(List<ImportDataViewModel> importData,string fileName="");
	}
}
