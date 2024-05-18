using CsvHelper;
using CsvHelper.Configuration;
using ETL.DataLayer;
using ETL.DataLayer.Entities;
using ETL.Mappers;
using System.Globalization;

class Program
{
	static void Main(string[] args)
	{
		string csvFilePath = @"C:\work\sample-cab-data.csv";
		string duplicateFilePath = @"C:\work\duplicates.csv";

		var config = new CsvConfiguration(CultureInfo.InvariantCulture)
		{
			PrepareHeaderForMatch = args => args.Header.ToLower(),
			MissingFieldFound = null,
			BadDataFound = null
		};

		using (var reader = new StreamReader(csvFilePath))
		using (var csv = new CsvReader(reader, config))
		{
			csv.Context.RegisterClassMap<TaxiRideMap>();

			var records = csv.GetRecords<TaxiRide>().ToList();
			var cleanedRecords = CleanData(records);
			var duplicateRecords = GetDuplicateRecords(cleanedRecords);
			SaveDuplicatesToFile(duplicateRecords, duplicateFilePath);
			cleanedRecords = cleanedRecords.Except(duplicateRecords).ToList();

			using (var context = new TaxiRideContext())
			{
				context.Database.EnsureCreated();
				context.TaxiRides.AddRange(cleanedRecords);
				context.SaveChanges();
			}
		}
	}

	static List<TaxiRide> CleanData(List<TaxiRide> records)
	{
		return records.Select(record =>
		{
			record.store_and_fwd_flag = record.store_and_fwd_flag == "Y" ? "Yes" : "No";
			return record;
		}).ToList();
	}

	static List<TaxiRide> GetDuplicateRecords(List<TaxiRide> records)
	{
		return records.GroupBy(r => new { r.tpep_pickup_datetime, r.tpep_dropoff_datetime, r.passenger_count })
					  .Where(g => g.Count() > 1)
					  .SelectMany(g => g)
					  .ToList();
	}

	static void SaveDuplicatesToFile(List<TaxiRide> duplicates, string filePath)
	{
		using (var writer = new StreamWriter(filePath))
		using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
		{
			csv.WriteRecords(duplicates);
		}
	}
}
