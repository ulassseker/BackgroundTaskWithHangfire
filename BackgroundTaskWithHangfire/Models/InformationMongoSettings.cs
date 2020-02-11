

namespace BackgroundTaskWithHangfire.Models
{
    public class InformationMongoSettings : IInformationMongoSettings
    {
        public string InformationCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IInformationMongoSettings
    {
        string InformationCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}