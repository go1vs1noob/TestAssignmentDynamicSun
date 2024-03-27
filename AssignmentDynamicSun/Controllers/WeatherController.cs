using AssignmentDynamicSun.Data;
using AssignmentDynamicSun.Helpers;
using AssignmentDynamicSun.Interfaces;
using AssignmentDynamicSun.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentDynamicSun.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IExcelService _excelService;
        private readonly ILogger<WeatherController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private int PageSize { get; init; }
        public WeatherController(IExcelService excelService, ApplicationDbContext dbContext, ILogger<WeatherController> logger)
        {
            _excelService = excelService;
            _dbContext = dbContext;
            PageSize = 10;
            _logger = logger;
        }

        public IActionResult Index(int? year, int? month, int page = 1)
        {
            IEnumerable<Weather> query = _dbContext.Weathers;
            if (!ModelState.IsValid)
            {
                RedirectToAction("BadRequestPage");
            }
            if (year.HasValue)
            {
                query = query.Where(w => w.DateTime.Year == year);
            }
            if (month.HasValue)
            {
                query = query.Where(w => w.DateTime.Month == month);
            }
            int totalCount = query.Count();
            int pageCount = (int)Math.Ceiling((double)totalCount / PageSize);
            var weatherData = query.Skip((page - 1) * PageSize)
                                   .Take(PageSize)
                                   .ToList();
            ViewBag.PageCount = pageCount;
            ViewBag.Year = year;
            ViewBag.Month = month;
            ViewBag.CurrentPage = page;
            return View(weatherData);
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IEnumerable<IFormFile> fileInputs)
        {
            try
            {
                IList<Weather> weatherModelsToAdd = new List<Weather>();
                foreach (var fileInput in fileInputs)
                {
                    ExcelData excelData = new(fileInput);
                    await excelData.ReadFromExcelFile(skipRowsFromStartInSheet: 4);
                    var newWeatherModels = _excelService.CreateWeatherModels(excelData);
                    foreach (var newWeatherModel in newWeatherModels)
                    {
                        var weatherModelToUpdate = await _dbContext.Weathers.FirstOrDefaultAsync(w => w.DateTime == newWeatherModel.DateTime);
                        if (weatherModelToUpdate != null)
                        {
                            TransferValuesBetweenWeatherModels(weatherModelToUpdate, newWeatherModel);
                        }
                        else
                        {
                            weatherModelsToAdd.Add(newWeatherModel);
                        }
                    }
                }
                _dbContext.Weathers.AddRange(weatherModelsToAdd);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("UploadFailure");
            }
            return RedirectToAction("UploadSuccess");
        }

        public ActionResult UploadSuccess()
        {
            return View();
        }

        public ActionResult UploadFailure()
        {
            return View();
        }

        public ActionResult BadRequestPage()
        {
            return View();
        }

        private void TransferValuesBetweenWeatherModels(Weather to, Weather from)
        {
            to.Temperature = from.Temperature;
            to.AirPressure = from.AirPressure;
            to.Cloudiness = from.Cloudiness;
            to.CloudBase = from.CloudBase;
            to.DewPoint = from.DewPoint;
            to.Humidity = from.Humidity;
            to.NaturalPhenomena = from.NaturalPhenomena;
            to.WindDirection = from.WindDirection;
            to.WindSpeed = from.WindSpeed;
            to.HorizontalVisibility = from.HorizontalVisibility;
        }

    }
}