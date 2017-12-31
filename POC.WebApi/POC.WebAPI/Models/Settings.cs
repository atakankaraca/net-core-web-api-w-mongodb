using Microsoft.Extensions.Configuration;

namespace POC.WebAPI.Models
{
    public class Settings
    {
        public string ConnectionString;
        public string Database;
        public IConfiguration ConfigurtionRoot;
    }
}
