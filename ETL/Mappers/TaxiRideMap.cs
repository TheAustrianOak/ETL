using CsvHelper.Configuration;
using ETL.DataLayer.Entities;

namespace ETL.Mappers
{
    public class TaxiRideMap : ClassMap<TaxiRide>
    {
        public TaxiRideMap()
        {
            Map(m => m.tpep_pickup_datetime);
            Map(m => m.tpep_dropoff_datetime);
            Map(m => m.passenger_count).TypeConverter<NullableInt32Converter>();
            Map(m => m.trip_distance);
            Map(m => m.store_and_fwd_flag);
            Map(m => m.PULocationID);
            Map(m => m.DOLocationID);
            Map(m => m.fare_amount);
            Map(m => m.tip_amount);

            Map(m => m.Id).Ignore(); // Id property ignored
        }
    }
}
