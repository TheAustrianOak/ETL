using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class TaxiRide
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime tpep_pickup_datetime { get; set; }
		public DateTime tpep_dropoff_datetime { get; set; }

		[TypeConverter(typeof(NullableInt32Converter))]
		public int passenger_count { get; set; }
		public double trip_distance { get; set; }
		public string store_and_fwd_flag { get; set; }
		public int PULocationID { get; set; }
		public int DOLocationID { get; set; }
		public double fare_amount { get; set; }
		public double tip_amount { get; set; }
	}
}
