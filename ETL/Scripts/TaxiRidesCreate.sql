CREATE TABLE TaxiRides (
    Id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    tpep_pickup_datetime DATETIME,
    tpep_dropoff_datetime DATETIME,
    passenger_count INT,
    trip_distance FLOAT,
    store_and_fwd_flag NVARCHAR(3),
    PULocationID INT,
    DOLocationID INT,
    fare_amount FLOAT,
    tip_amount FLOAT
);