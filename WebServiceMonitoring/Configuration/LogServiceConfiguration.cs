using System.Configuration;

namespace LogService.Configuration
{
    public sealed class LogServiceConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string Conexao {

            get {

                return base["connectionString"].ToString();
            }
            set {
                base["connectionString"] = value;
            }
        }

        private static LogServiceConfiguration instance = null;

        public static LogServiceConfiguration Instance {
            get {

                if (instance == null) {
                    instance = (LogServiceConfiguration)ConfigurationManager.GetSection("logConfiguracao");
                }
                return instance;
            }

        }
    }
}
