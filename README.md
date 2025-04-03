# ETLCabData
**Overview**
ETLCabData is a C# .NET ETL project that imports taxi trip data from a CSV file into a Microsoft SQL Server database. The project reads, transforms, cleans, and loads data efficiently using bulk insertion.

**Setup and Usage**
1. Database Setup:

Create a SQL Server database named `CabDataDB`. Then, run the SQL script provided in [CreateCabTrips.sql](sql/CreateCabTrips.sql) using SQL Server Management Studio (SSMS) to create the `CabTrips` table.

3. Configuration:

The repository includes an appsettings.json file. Update the connection string if needed.

3. Running the ETL Process:

- Build and run the project.

- The ETL process will:

  Read and transform data from the CSV.

  Remove duplicate records (saving them in duplicates.csv).

  Bulk insert unique records into the CabTrips table.

  The console will output a success message with the count of unique records loaded.
