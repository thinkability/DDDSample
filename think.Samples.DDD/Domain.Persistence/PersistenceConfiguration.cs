namespace Domain.Persistence
{
    public class PersistenceConfiguration
    {
        public string ConnectionString { get; set; }
        public string Schema { get; set; }
        public string Tenant { get; set; }
    }
}