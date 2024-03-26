namespace AssignmentDynamicSun.Interfaces
{
	public interface IExcelData : IEnumerable<List<string?>>
	{
		public IFormFile ExcelFile { get; set; }
		public string this[int row, int column] { get; }
		public List<string> this[int row] { get; }
		public int Count { get; }
		public Task ReadFromExcelFile(int skipRowsFromStartInSheet);
	}
}
