using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;
using System.Threading.Tasks;

namespace CmdParser
{
    internal class ConfigurationSystemCommand
    {
        internal ConfigurationSystemCommand(Command root)
        {
            var configsystemcommand = new Command("system");
            root.Add(configsystemcommand);

            var configsystemloggingcommand = new Command("getLogging");
            configsystemcommand.Add(configsystemloggingcommand);
            configsystemloggingcommand.Handler = CommandHandler.Create(GetConfigurationSystemLogging);

            var getconfigsystemhealthcommand = new Command("getHealthEndpoints");
            configsystemcommand.Add(getconfigsystemhealthcommand);
            getconfigsystemhealthcommand.Handler = CommandHandler.Create(GetConfigurationSystemHealth);

            var deleteconfigsystemhealthcommand = new Command("deleteHealthEndpoints");
            configsystemcommand.Add(deleteconfigsystemhealthcommand);
            deleteconfigsystemhealthcommand.Handler = CommandHandler.Create(DeleteConfigurationSystemHealth);

            var postconfigsystemhealthcommand = new Command("postHealthEndpoints");
            configsystemcommand.Add(postconfigsystemhealthcommand);
            

            postconfigsystemhealthcommand.AddOption(new Option("--import-filepath", "",
                new Argument<string>
                {
                    Arity = ArgumentArity.ExactlyOne
                }));
            postconfigsystemhealthcommand.Handler = CommandHandler.Create<string>(PostConfigurationSystemHealth);
            //getconfigsystemhealthcommand.AddOption(new Option("--component-id", "component id", new Argument<string>()));
            //getconfigsystemhealthcommand.Handler = CommandHandler.Create<string>(GetConfigurationSystemHealthId);
        }

        private static async Task GetConfigurationSystemHealthId(string componentId)
        {
            var sb = new StringBuilder();
            sb.Append(RestClient.baseUrl);
            sb.Append("/configuration");
            sb.Append("/system");
            sb.Append("/healthendpoints");
            sb.Append($"/{componentId}");
            await RestClient.HttpGetter(sb.ToString());
        }

        private static async Task GetConfigurationSystemHealth()
        {
            var sb = new StringBuilder();
            sb.Append(RestClient.baseUrl);
            sb.Append("/configuration");
            sb.Append("/system");
            sb.Append("/healthendpoints");
            await RestClient.HttpGetter(sb.ToString());
        }

        private static async Task DeleteConfigurationSystemHealth()
        {
            var sb = new StringBuilder();
            sb.Append(RestClient.baseUrl);
            sb.Append("/configuration");
            sb.Append("/system");
            sb.Append("/healthendpoints");
            await RestClient.HttpDeleter(sb.ToString());
        }

        private static async Task PostConfigurationSystemHealth(string importFilepath)
        {
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
            sb.Append("/system");
            sb.Append("/healthendpoints");
            await RestClient.HttpPoster(sb.ToString(), reason);
        }

        private static async Task GetConfigurationSystemLogging()
        {
            var sb = new StringBuilder();
            sb.Append(RestClient.baseUrl);
            sb.Append("/configuration");
            sb.Append("/system");
            sb.Append("/logging");
            await RestClient.HttpGetter(sb.ToString());
        }

    }
}
