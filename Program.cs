using github_activity.Utility;
using github_activity.Models;
using System.Reflection;
using System.Web;
using System.Text.Json;

namespace github_activity
{
    public class Program
    {
        public static async Task Main()
        {
            PrintWelcome();

            while (true)
            {
                ConsoleMsg.PrintCommandMsg("Enter command: ");
                string input = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrEmpty(input))
                {
                    RestartCommand();
                    continue;
                }
                var commands = Utility.Utility.ParseInput(input);
                if(commands.Count != 2 && !commands[0].Equals("exit"))
                {
                    RestartCommand();
                    continue;
                }
                if (commands[0].Equals("exit"))
                {
                    break;
                }

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

                var command = commands[0];
                var username = commands[1];

                if (!command.Equals("github-activity"))
                {
                    RestartCommand(); continue;
                }
                if (string.IsNullOrEmpty(username))
                {
                    ConsoleMsg.PrintErrorMsg("Username is not entered!");
                    continue;
                }

                var activities = await GetUserActivity(client, username);
                List<ActivityInfo> infoList = new List<ActivityInfo>();
                if(activities.Count != 0)
                {
                    var group = activities.GroupBy(x => x.Type);
                    foreach (var item in group)
                    {
                        var info = new ActivityInfo
                        {
                            Type = item.Key,
                            Count = item.Count().ToString(),
                            RepoName = item.Select(x => x.repo.Name).Distinct().ToList()
                        };
                        infoList.Add(info);
                    }

                    Console.WriteLine("\nOutput: ");

                    foreach(var item in infoList)
                    {
                        switch (item.Type)
                        {
                            case "PushEvent":
                                ConsoleMsg.PrintInfoMsg($"{username} pushed {item.Count} commits to {PrintInString(item.RepoName)}");
                                break;
                            case "CreateEvent":
                                ConsoleMsg.PrintInfoMsg($"{username} created {item.Count} repository {PrintInString(item.RepoName)}");
                                break;
                            case "IssuesEvent":
                                ConsoleMsg.PrintInfoMsg($"{username} created {item.Count} issues on {PrintInString(item.RepoName)}");
                                break;
                            case "PullRequestEvent":
                                ConsoleMsg.PrintInfoMsg($"{username} created {item.Count} pull requests on {PrintInString(item.RepoName)}");
                                break;
                            case "WatchEvent":
                                ConsoleMsg.PrintInfoMsg($"{username} starred {PrintInString(item.RepoName)}");
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    ConsoleMsg.PrintErrorMsg("No meaningfull activities find for this user");
                    continue;
                }
            }
        }
        static string PrintInString(List<string> repoName)
        {
            if (repoName.Count <= 1)
            {
                return repoName.FirstOrDefault() ?? string.Empty;
            }
            if (repoName.Count > 1)
            {
                string names = string.Empty;
                foreach (var name in repoName)
                {
                    if(name == repoName.LastOrDefault())
                    {
                        names += "& " + name;
                    }
                    else
                    {
                        names += name + ", ";
                    }
                }
                return names;
            }
            return string.Empty;
        }
        public static void PrintWelcome()
        {
            ConsoleMsg.PrintInfoMsg("Welcome to Github Activity checker");
            ConsoleMsg.PrintInfoMsg("Print github-activity <username> to start");
        }
        public static void RestartCommand()
        {
            ConsoleMsg.PrintErrorMsg("Wrong Command!");
        }
        public static async Task<List<Activity>> GetUserActivity(HttpClient client, string name)
        {
            try
            {
                var encodedUsername = HttpUtility.UrlEncode(name);
                var url = $"https://api.github.com/users/{encodedUsername}/events";
                var httpResponse = await client.GetAsync(url);
                httpResponse.EnsureSuccessStatusCode();

                var content = await httpResponse.Content.ReadAsStringAsync();
                var activities = JsonSerializer.Deserialize<List<Activity>>(content);

                return activities ?? new();
            }
            catch (Exception ex)
            {
                ConsoleMsg.PrintErrorMsg("There's problem to get your github activities");
                throw;
            }
        }
    }
}
