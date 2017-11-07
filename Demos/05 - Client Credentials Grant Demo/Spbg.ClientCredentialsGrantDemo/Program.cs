using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Spbg.ClientCredentialsGrantDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Login
            var token = GetTokenForClientAsync().Result;
            Console.WriteLine("OAuth token");
            Console.WriteLine(token);

            // Get user profile
            using (var wc = new WebClient())
            {
                wc.Headers["Authorization"] = $"Bearer {token}";
                var responseJson = wc.DownloadString("https://graph.microsoft.com/v1.0/me/");
                Console.WriteLine();
                Console.WriteLine("User profile");
                Console.WriteLine(responseJson);
            }
        }

        private static async Task<AuthenticationResult> GetTokenForClientAsync()
        {
            // https://login.microsoftonline.com/common/adminconsent?client_id=1329f001-78b3-4b0b-ac7b-710bd6f4ec01&state=1&redirect_uri=msal1329f001-78b3-4b0b-ac7b-710bd6f4ec01://auth

            var clientId = "1329f001-78b3-4b0b-ac7b-710bd6f4ec01";
            var tenantId = "DEV365x973253.onmicrosoft.com";
            var authority = $"https://login.microsoftonline.com/{tenantId}/common/v2.0";
            var replyUri = "msal1329f001-78b3-4b0b-ac7b-710bd6f4ec01://auth";
            var clientSecret = "";

            var daemonClient = new ConfidentialClientApplication(
                clientId,
                authority,
                replyUri,
                new ClientCredential(clientSecret),
                null, null);

            string[] scopes = { "user.read" };
            var result = await daemonClient.AcquireTokenForClientAsync(scopes);
            return result;
        }
    }
}
