using CsvHelper;
using CsvHelper.Configuration;
using ETL.DataLayer;
using ETL.DataLayer.Entities;
using ETL.Mappers;
using ETL.Services;
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
			var cleanedRecords = TaxiRideService.CleanData(records);
			var duplicateRecords = TaxiRideService.GetDuplicateRecords(cleanedRecords);
			TaxiRideService.SaveDuplicatesToFile(duplicateRecords, duplicateFilePath);
			cleanedRecords = cleanedRecords.Except(duplicateRecords).ToList();

			using (var context = new TaxiRideContext())
			{
				context.Database.EnsureCreated();
				context.TaxiRides.AddRange(cleanedRecords);
				context.SaveChanges();
			}
		}
	}

	
}
