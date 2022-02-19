namespace WebApp.Repository.Settings
{
    public interface ISqlDbSettings
    {
        public string ConnectionString { get; set; }
    }

    public class SqlDbSettings : ISqlDbSettings
    {
        public string ConnectionString { get; set; }
    }
}