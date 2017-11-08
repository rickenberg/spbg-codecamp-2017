using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Spbg.ClientCredentialsGrantDemo
{
    /// <summary>
    /// Simple example for a Microsoft Graph with app-only permission using the v2 endpoint in Azure AD.
    /// The code is based on the example shown here: 
    /// https://blogs.msdn.microsoft.com/tsmatsuz/2016/10/07/application-permission-with-v2-endpoint-and-microsoft-graph/
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Login
            var token = GetTokenForClientAsync().Result;

            Console.WriteLine("OAuth token");
            Console.WriteLine(token);

            // Get user profile by e-mail address
            using (var wc = new WebClient())
            {
                wc.Headers["Authorization"] = $"Bearer {token}";
                var responseJson = wc.DownloadString("https://graph.microsoft.com/v1.0/users/bernd@DEV365x973253.onmicrosoft.com");

                Console.WriteLine();
                Console.WriteLine("User profile");
                Console.WriteLine(responseJson);
            }
        }

        private static async Task<string> GetTokenForClientAsync()
        {
            // Browse to this URL for admin consent (before first execution)
            // https://login.microsoftonline.com/common/adminconsent?client_id=1329f001-78b3-4b0b-ac7b-710bd6f4ec01&state=1

            // Get the values below from your app registration on https://apps.dev.microsoft.com
            var clientId = "1329f001-78b3-4b0b-ac7b-710bd6f4ec01";
            var replyUri = "msal1329f001-78b3-4b0b-ac7b-710bd6f4ec01://auth";
            var clientSecret = "";

            // ID of your tenant
            var tenantId = "DEV365x973253.onmicrosoft.com";

            // NOTE: You cannot use the common endpoint when using app-only permission. We need your tenant ID.
            var authority = $"https://login.microsoftonline.com/{tenantId}/v2.0";
            
            var daemonClient = new ConfidentialClientApplication(
                clientId,
                authority,
                replyUri,
                new ClientCredential(clientSecret),
                null, null);

            // With app-only you cannot specify permission scopes on the fly. Only the default scope is accepted.
            string[] scopes = { "https://graph.microsoft.com/.default" };
            var result = await daemonClient.AcquireTokenForClientAsync(scopes);
            return result.AccessToken;
        }
    }
}
