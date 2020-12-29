using BUSINESS_LOGIC.Dtos;
using BUSINESS_LOGIC.Interfaces;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;

namespace BUSINESS_LOGIC.Services
{
    public class DataService : IReadData
    {
		#region Const Fields

		private const int KeyIndex = 0;
		private const int ArticleCodeIndex = 1;
		private const int ColorCodeIndex = 2;
		private const int DescriptionIndex = 3;
		private const int PriceIndex = 4;
		private const int DiscountPriceIndex = 5;
		private const int DeliveredInIndex = 6;
		private const int Q1Index = 7;
		private const int SizeIndex = 8;
		private const int ColorIndex = 9;

		#endregion
		public List<ImportDataViewModel> GetDataWithExcelReader(IFormFile file)
        {
			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

			using var stream = file.OpenReadStream();

			using var reader = ExcelReaderFactory.CreateReader(stream);

			var data = reader.AsDataSet(new ExcelDataSetConfiguration()
			{
				UseColumnDataType = false,
				ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
				{
					UseHeaderRow = true,
					ReadHeaderRow = (rowReader) =>
					{
						rowReader.Read();
						rowReader.Read();
					}
				}
			});

			List<ImportDataViewModel> result = new List<ImportDataViewModel>();

			foreach (DataRow row in data.Tables[0].Rows)
			{
				try
				{
					ImportDataViewModel item = new ImportDataViewModel()
					{
						ArticleCode = row[ArticleCodeIndex].ToString(),
						Color = row[ColorIndex].ToString(),
						ColorCode = row[ColorCodeIndex].ToString(),
						DeliveredIn = row[DeliveredInIndex].ToString(),
						Description = row[DescriptionIndex].ToString(),
						DiscountPrice = Convert.ToDecimal(row[DiscountPriceIndex]),
						Key = row[KeyIndex].ToString(),
						Price = Convert.ToDecimal(row[PriceIndex]),
						Q1 = row[Q1Index].ToString(),
						Size = row[SizeIndex].ToString()
					};

					result.Add(item);
				}
				catch (Exception)
				{
					continue;
				}
			}

			return result;
		}
	}
}
