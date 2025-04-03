# ETLCabData
**Overview**
ETLCabData is a C# .NET ETL project that imports taxi trip data from a CSV file into a Microsoft SQL Server database. The project reads, transforms, cleans, and loads data efficiently using bulk insertion.

**Setup and Usage**
1. Database Setup:

Create a SQL Server database named CabDataDB.
Run the following SQL script in SQL Server Management Studio (SSMS) to create the CabTrips table:

  USE CabDataDB;
  GO
  
  CREATE TABLE CabTrips (
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


2. Configuration:

The repository includes an appsettings.json file. Update the connection string if needed.

3. Running the ETL Process:

- Build and run the project.

- The ETL process will:

  Read and transform data from the CSV.

  Remove duplicate records (saving them in duplicates.csv).

  Bulk insert unique records into the CabTrips table.

  The console will output a success message with the count of unique records loaded.
