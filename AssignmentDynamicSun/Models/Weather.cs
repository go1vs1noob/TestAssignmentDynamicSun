using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AssignmentDynamicSun.Models
{
    public class Weather
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
		public double? Temperature { get; set; }
        [Range(0,100,ErrorMessage ="Percentage can be from 0 to 100")]
        public double? Humidity { get; set; }
        public double? DewPoint { get; set; }
        public int? AirPressure { get; set; }
        public string? WindDirection { get; set; }
        public int? WindSpeed { get; set; }
		[Range(0, 100, ErrorMessage = "Percentage can be from 0 to 100")]
		public int? Cloudiness { get; set; }
        public int? CloudBase { get; set; }
        public int? HorizontalVisibility { get; set; }
        public string? NaturalPhenomena { get; set; }
    }
}



