using AssignmentDynamicSun.Interfaces;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections;

namespace AssignmentDynamicSun.Helpers
{
	public class ExcelData : IExcelData
	{
		private List<List<string?>> _data;
		private IFormFile _excelFile;
		public int Count => _data.Count;
		public List<string?> this[int row] => _data[row];
		public string? this[int row, int column] => _data[row][column];

		public ExcelData(IFormFile excelFile)
		{
			_data = new List<List<string?>>();
			ExcelFile = excelFile;
		}

		public IFormFile ExcelFile
		{
			get => _excelFile;
			set
			{
				if (IsExcelFile(value))
				{
					_excelFile = value;
				}
				else
				{
					throw new Exception("Incorrect file format. Use Excel file");
				}
			}
		}

		public async Task ReadFromExcelFile(int skipRowsFromStartInSheet = 0)
		{
			try
			{
				using (var stream = new MemoryStream())
				{
					await ExcelFile.CopyToAsync(stream);
					stream.Position = 0;

					using (var workbook = new XSSFWorkbook(stream))
					{
						workbook.MissingCellPolicy = MissingCellPolicy.RETURN_BLANK_AS_NULL;
						for (int i = 0; i < workbook.NumberOfSheets; i++)
						{
							ISheet sheet = workbook.GetSheetAt(i);
							if (sheet != null)
							{
								for (int row = 0 + skipRowsFromStartInSheet; row <= sheet.LastRowNum; row++)
								{
									IRow currentRow = sheet.GetRow(row);
									if (currentRow != null)
									{
										var rowData = new List<string?>();
										for (int j = 0; j < currentRow.LastCellNum; j++)
										{
											ICell cell = currentRow.GetCell(j);
											if (cell == null)
											{
												rowData.Add(null);
											}
											else { 
												rowData.Add(cell.ToString());
											}
										}
										_data.Add(rowData);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error reading excel data from file: {ex.Message}", ex);
			}
		}

		public IEnumerator<List<string?>> GetEnumerator()
		{
			return _data.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private bool IsExcelFile(IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				throw new Exception("Your file is null or empty");
			}
			if (!file.FileName.EndsWith(".xlsx") && !file.FileName.EndsWith(".xls"))
			{
				throw new Exception("Incorrect file extension");
			}
			return true;
		}
	}
}