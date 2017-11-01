using System;
using System.Collections.Generic;
using Microsoft.Graph;

namespace SharePointBrugerGruppe.SdkSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var graphClient = AuthenticationHelper.GetAuthenticatedClient();
            SendMail(graphClient);

            Console.WriteLine("Waiting for tasks to finish (press enter to exit)");
            Console.ReadLine();
        }

        private static async void SendMail(GraphServiceClient graphClient)
        {
            Console.WriteLine("- Sending e-mail");
            var email = new Message
            {
                Body = new ItemBody
                {
                    Content = "some content in the body",
                    ContentType = BodyType.Html
                },
                Subject = "test mail",
                ToRecipients = new List<Recipient>
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress { Address = "bernd@M365x895555.onmicrosoft.com" }
                    }
                }
            };

            try
            {
                await graphClient.Me.SendMail(email, true).Request().PostAsync();
                Console.WriteLine("- E-mail was sent");
            }
            catch (ServiceException exception)
            {
                throw new Exception("We could not send the message: " + exception.Error == null ? "No error message returned." : exception.Error.Message);
            }
        }
    }
}
