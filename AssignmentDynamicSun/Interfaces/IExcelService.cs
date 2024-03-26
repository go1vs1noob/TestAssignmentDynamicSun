using AssignmentDynamicSun.Models;

namespace AssignmentDynamicSun.Interfaces
{
    public interface IExcelService
    {
        IList<Weather> CreateWeatherModels(IExcelData ExcelData);
    }
}
