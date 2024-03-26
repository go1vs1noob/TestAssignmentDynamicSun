using AssignmentDynamicSun.Models;

namespace AssignmentDynamicSun.Mappers
{
	public class ExcelMappers
	{
		private readonly ILogger<ExcelMappers> _logger;
        public ExcelMappers(ILogger<ExcelMappers> logger)
        {
			_logger = logger;
        }
        public Weather MapExcelWeatherRowToModel(List<string> row)
		{
			var weather = new Weather();

			if (row[0] == null || row[1] == null)
			{
				_logger.LogError("Date is not set");
				throw new Exception("Date is not set");
			}
			var dateTimeString = row[0] + " " + row[1];
			if (DateTime.TryParse(dateTimeString, out DateTime dateTime))
			{
				weather.DateTime = dateTime;
			}
			else
			{
				_logger.LogError("Error parsing date");
			}
			weather.Temperature = AssignDoubleOrNull(row.ElementAtOrDefault(2));
			weather.Humidity = AssignDoubleOrNull(row.ElementAtOrDefault(3));
			weather.DewPoint = AssignDoubleOrNull(row.ElementAtOrDefault(4));
			weather.AirPressure = AssignIntOrNull(row.ElementAtOrDefault(5));
			weather.WindDirection = row.ElementAtOrDefault(6);
			weather.WindSpeed = AssignIntOrNull(row[7]);
			weather.Cloudiness = AssignIntOrNull(row[8]);
			weather.CloudBase = AssignIntOrNull(row[9]);
			weather.HorizontalVisibility = AssignIntOrNull(row[10]);
			weather.NaturalPhenomena = row.ElementAtOrDefault(11);
			return weather;
		}

		private int? AssignIntOrNull(string? value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			try
			{
				return int.Parse(value);
			}
			catch(Exception ex) 
			{
				return null;
			}
		}
		private double? AssignDoubleOrNull(string? value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			try
			{
				return double.Parse(value);
			}
			catch(Exception ex)
			{
				return null;
			}
		}
	}
}
