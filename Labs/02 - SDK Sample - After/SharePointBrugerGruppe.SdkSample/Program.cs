using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace SharePointBrugerGruppe.SdkSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var graphClient = AuthenticationHelper.GetAuthenticatedClient();

            Console.WriteLine($"https://graph.microsoft.com/v1.0/Me");
            var me = graphClient.Me.Request().GetAsync().Result;
            ObjectDumper.Dump(me);

            var groupId = "bdd1e45b-a0ea-4bc1-b8ab-9e8bd50d4709";
            // https://graph.microsoft.com/v1.0/me/joinedGroups
            // Not available
            // https://graph.microsoft.com/beta/me/joinedTeams
            // Not available

            Console.WriteLine($"https://graph.microsoft.com/v1.0/groups/{groupId}");
            var group = graphClient.Groups[groupId].Request().GetAsync().Result;
            ObjectDumper.Dump(group);

            Console.WriteLine($"https://graph.microsoft.com/v1.0/groups/{groupId}/events");
            var events = graphClient.Groups[groupId].Events.Request().GetAsync().Result;
            ObjectDumper.Dump(events);

            Console.WriteLine($"https://graph.microsoft.com/v1.0/groups/{groupId}/members");
            var members = graphClient.Groups[groupId].Members.Request().GetAsync().Result;
            ObjectDumper.Dump(members);

            Console.WriteLine($"https://graph.microsoft.com/v1.0/groups/{groupId}/drive");
            var drive = graphClient.Groups[groupId].Drive.Request().GetAsync().Result;
            ObjectDumper.Dump(drive);

            Console.WriteLine($"https://graph.microsoft.com/v1.0/groups/{groupId}/drive/root/children");
            var files = graphClient.Groups[groupId].Drive.Root.Children.Request().GetAsync().Result;
            ObjectDumper.Dump(files);

            var fileId = "01E63P7DH7NEOIVADZI5HYGYEY7ESWV3BH";
            Console.WriteLine($"https://graph.microsoft.com/v1.0/groups/{groupId}/drive/items/{fileId}");
            var file = graphClient.Groups[groupId].Drive.Items[fileId].Request().GetAsync().Result;
            ObjectDumper.Dump(file);

            Console.WriteLine("(press enter to exit)");
            Console.ReadLine();
        }
    }
}
