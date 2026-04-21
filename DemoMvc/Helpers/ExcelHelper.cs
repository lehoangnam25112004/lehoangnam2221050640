using OfficeOpenXml;
using System.Reflection;
namespace DemoMvc.Helpers
{
public static class ExcelHelper
{
    public static List<T> ToList<T>(IFormFile file) where T : new()
    {
        var list = new List<T>();
        ExcelPackage.License.SetNonCommercialPersonal("Nam");

        using (var stream = new MemoryStream())
        {
            file.CopyTo(stream);
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;
                var colCount = worksheet.Dimension.Columns;

                // Lấy danh sách các cột tiêu đề ở dòng 1
                var properties = typeof(T).GetProperties();

                for (int row = 2; row <= rowCount; row++)
                {
                    var obj = new T();
                    for (int col = 1; col <= colCount; col++)
                    {
                        var header = worksheet.Cells[1, col].Value?.ToString();
                        var cellValue = worksheet.Cells[row, col].Value;

                        // Tự động tìm thuộc tính trong Model có tên giống tiêu đề Excel
                        var prop = properties.FirstOrDefault(p => p.Name == header);
                        if (prop != null && cellValue != null)
                        {
                            // Chuyển kiểu dữ liệu tự động (String sang Int, DateTime...)
                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            var convertedValue = Convert.ChangeType(cellValue, targetType);
                            prop.SetValue(obj, convertedValue);
                        }
                    }
                    list.Add(obj);
                }
            }
        }
        return list;
    }
}
}