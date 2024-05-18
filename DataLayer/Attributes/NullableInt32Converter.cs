using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

public class NullableInt32Converter : Int32Converter
{
	public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
	{
		if (string.IsNullOrEmpty(text))
		{
			return 0; // Default value for missing passenger_count
		}
		return base.ConvertFromString(text, row, memberMapData);
	}
}