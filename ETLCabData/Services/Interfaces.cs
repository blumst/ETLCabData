using ETLCabData.Models;

namespace ETLCabData.Services
{
    public interface ICsvProcessor
    {
        IEnumerable<CabTrip> ReadCsv(string filePath);
    }

    public interface IDataTransformer
    {
        CabTrip Transform(CabTrip record);
    }

    public interface IDuplicateHandler
    {
        List<CabTrip> RemoveDuplicates(IEnumerable<CabTrip> trips, out List<CabTrip> duplicates);
        void WriteDuplicates(string filePath, List<CabTrip> duplicates);
    }

    public interface IDbInserter
    {
        void BulkInsert(string connectionString, List<CabTrip> trips);
    }
}
