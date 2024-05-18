using CsvHelper;
using ETL.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Services
{
	public class TaxiRideService
	{
		public static List<TaxiRide> CleanData(List<TaxiRide> records)
		{
			return records.Select(record =>
			{
				record.store_and_fwd_flag = record.store_and_fwd_flag == "Y" ? "Yes" : "No";
				return record;
			}).ToList();
		}

		public static List<TaxiRide> GetDuplicateRecords(List<TaxiRide> records)
		{
			return records.GroupBy(r => new { r.tpep_pickup_datetime, r.tpep_dropoff_datetime, r.passenger_count })
						  .Where(g => g.Count() > 1)
						  .SelectMany(g => g)
						  .ToList();
		}

		public static void SaveDuplicatesToFile(List<TaxiRide> duplicates, string filePath)
		{
			using (var writer = new StreamWriter(filePath))
			using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(duplicates);
			}
		}
	}
}
