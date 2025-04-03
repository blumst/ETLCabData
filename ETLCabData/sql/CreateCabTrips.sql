-- Create the database
CREATE DATABASE CabDataDB;
GO

-- Create the table in the new database using a fully qualified name
CREATE TABLE CabDataDB.dbo.CabTrips (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    tpep_pickup_datetime DATETIME2 NOT NULL,
    tpep_dropoff_datetime DATETIME2 NOT NULL,
    passenger_count TINYINT NOT NULL,
    trip_distance DECIMAL(5,2) NOT NULL,
    store_and_fwd_flag NVARCHAR(3) NOT NULL,
    PULocationID INT NOT NULL,
    DOLocationID INT NOT NULL,
    fare_amount DECIMAL(8,2) NOT NULL,
    tip_amount DECIMAL(8,2) NOT NULL
);
GO

-- Create an index on PULocationID (including tip_amount for covering index)
CREATE INDEX IX_CabTrips_PULocationID 
    ON CabDataDB.dbo.CabTrips(PULocationID) INCLUDE (tip_amount);
GO

-- Create an index on trip_distance for fast sorting
CREATE INDEX IX_CabTrips_TripDistance 
    ON CabDataDB.dbo.CabTrips(trip_distance);
GO

-- Create a composite index on pickup and dropoff datetimes for travel time queries
CREATE INDEX IX_CabTrips_Time 
    ON CabDataDB.dbo.CabTrips(tpep_pickup_datetime, tpep_dropoff_datetime);
GO
