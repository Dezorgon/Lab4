namespace DataAccess
{
    internal interface ILogger
    {
        void WriteErrorToDB(string massage);
    }
}