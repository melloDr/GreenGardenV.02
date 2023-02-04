using Azure.Core;
using Azure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;

namespace GreeenGarden.Business.Service.SecretServices
{
    public class KeyVaultServices
    {
        public static string GetConnectionString()
        {
            string conn = null;
            try
            {
                SecretClientOptions options = new SecretClientOptions()
                {
                    Retry =
                    {
                        Delay= TimeSpan.FromSeconds(2),
                        MaxDelay = TimeSpan.FromSeconds(16),
                        MaxRetries = 5,
                        Mode = RetryMode.Exponential
                    }
                };
                var client = new SecretClient(new Uri("https://ggkeyvault2023.vault.azure.net/"), new DefaultAzureCredential(), options);

                KeyVaultSecret secret = client.GetSecret("ConnectionString");

                conn = secret.Value;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return conn;
        }
    }
}
