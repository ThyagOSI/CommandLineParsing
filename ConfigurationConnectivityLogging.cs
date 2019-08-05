using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CmdParser
{
    internal class ConfigurationConnectivityIdLogging
    {
        //private static  string s;

        internal ConfigurationConnectivityIdLogging(Command root)
        {
            var configurationConnectivityIdLogging = new Command("logging");
            root.Add(configurationConnectivityIdLogging);


            var getconnidLoggingcommand = new Command("getLogging");
            configurationConnectivityIdLogging.Add(getconnidLoggingcommand);
           
            getconnidLoggingcommand.AddOption(new Option("--connectivity-id", "",
            new Argument
            {
                Arity = ArgumentArity.ExactlyOne

            }));
            getconnidLoggingcommand.Handler = CommandHandler.Create<string>(GetConnectivityIdLogging);


            var deleteconnidLoggingcommand = new Command("deleteLogging");
            configurationConnectivityIdLogging.Add(deleteconnidLoggingcommand);
            deleteconnidLoggingcommand.AddOption(new Option("--connectivity-id", "",
                new Argument<string>
                {
                    Arity = ArgumentArity.ExactlyOne
                }));
            deleteconnidLoggingcommand.Handler = CommandHandler.Create<string>(DeleteConnectivityIdLogging);

            var postconnidLoggingcommand = new Command("postLogging");
            configurationConnectivityIdLogging.Add(postconnidLoggingcommand);
            postconnidLoggingcommand.AddOption(new Option("--connectivity-id", "",
                new Argument<string>
                {
                    Arity = ArgumentArity.ExactlyOne
                }));
            postconnidLoggingcommand.AddOption(new Option("--import-filepath", "",
                new Argument<string>
                {
                    Arity = ArgumentArity.ExactlyOne
                   
                }));
            postconnidLoggingcommand.Handler = CommandHandler.Create<string, string>(PostConnectivityIdLogging);

        }


        private static async Task GetConnectivityIdLogging(string connectivityId)
        {
            if (string.IsNullOrEmpty(connectivityId))
            {
                Console.WriteLine("Missing option: --connectivity-id");
                return;
            }
            var sb = new StringBuilder();
            sb.Append(RestClient.baseUrl);
            sb.Append("/configuration");
            sb.Append($"/{connectivityId}");
            sb.Append("/Logging");
            await RestClient.HttpGetter(sb.ToString());
        }


        private static async Task DeleteConnectivityIdLogging(string connectivityId)
        {
            if (string.IsNullOrEmpty(connectivityId))
            {
                Console.WriteLine("Missing option: --connectivity-id");
                return;
            }
            var sb = new StringBuilder();
            sb.Append(RestClient.baseUrl);
            sb.Append("/configuration");
            sb.Append($"/{connectivityId}");
            sb.Append("/Logging");
            await RestClient.HttpDeleter(sb.ToString());
        }

        private static async Task PostConnectivityIdLogging(string connectivityId, string importFilepath)
        {
            if (string.IsNullOrEmpty(connectivityId))
            {
                Console.WriteLine("Missing option: --connectivity-id");
                return;
            }

            if (string.IsNullOrEmpty(importFilepath))
            {
                Console.WriteLine("Missing option: --import-filepath");
                return;
            }

            var filecheck = new FileChecker(importFilepath);
            (bool flag, string reason) = filecheck.ValidateFile();
            if (flag == false)
            {
                Console.WriteLine(reason);
                return;
            }

            var sb = new StringBuilder();
            sb.Append(RestClient.baseUrl);
            sb.Append("/configuration");
            sb.Append($"/{connectivityId}");
            sb.Append("/Logging");
            await RestClient.HttpPoster(sb.ToString(), reason);
        }

    }
}