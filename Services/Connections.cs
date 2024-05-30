using Microsoft.Extensions.Configuration;
namespace JSearch.Services
{
    public class Connections
    {
        private readonly IConfiguration configuration;

        public Connections(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetConnection()
        {
            return this.configuration.GetConnectionString("Connection") ?? string.Empty;
        }
    }
}
