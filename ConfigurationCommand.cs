using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;
using System.Threading.Tasks;

namespace CmdParser
{
    internal class ConfigurationCommand
    {
        private Command configurationcommand;
        // private static string componentIdString;
        internal ConfigurationCommand(Command root)
        {
            configurationcommand = new Command("configuration");
            root.Add(configurationcommand);
            configurationcommand.Handler = CommandHandler.Create(GetAllConfiguration);

            configurationcommand.AddOption(new Option("--component-id")
            {
                Argument = new Argument<string>()
            });

            configurationcommand.Handler = CommandHandler.Create<string>(GetComponentIdConfiguration);

        }

        private static async Task GetAllConfiguration()
        {
            var sb = new StringBuilder();
            sb.Append(RestClient.baseUrl);
            sb.Append("/configuration");
            await RestClient.HttpGetter(sb.ToString());
        }

        private static async Task GetComponentIdConfiguration(string componentId)
        {
            // componentIdString = componentId;
            var sb = new StringBuilder();
            sb.Append(RestClient.baseUrl);
            sb.Append("/configuration");
            sb.Append($"/{componentId}");
            await RestClient.HttpGetter(sb.ToString());
        }

        internal Command GetCommand()
        {
            return configurationcommand;
        }
    }
}
