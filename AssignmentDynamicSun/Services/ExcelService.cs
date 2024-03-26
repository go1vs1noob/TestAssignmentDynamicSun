using AssignmentDynamicSun.Interfaces;
using AssignmentDynamicSun.Mappers;
using AssignmentDynamicSun.Models;

namespace AssignmentDynamicSun.Services
{
	public class ExcelService : IExcelService
	{
		private readonly ExcelMappers _mappers;

		public ExcelService(ExcelMappers mappers)
        {
			_mappers = mappers;
        }
        public IList<Weather> CreateWeatherModels(IExcelData excelData)
		{
			List<Weather> weatherModels = new();
			foreach (var row in excelData)
			{
				if (row == null || row.Count() == 0)
				{
					continue;
				}
				Weather weather = _mappers.MapExcelWeatherRowToModel(row);
				weatherModels.Add(weather);
			}
			return weatherModels;
		}
	}
}
