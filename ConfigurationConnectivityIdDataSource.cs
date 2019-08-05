using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;
using System.Threading.Tasks;

namespace CmdParser
{
    internal class ConfigurationConnectivityIdDataSource
    {
        internal ConfigurationConnectivityIdDataSource(Command root)
        {
            var configurationConnectivityIdDatasource = new Command("datasource");
            root.Add(configurationConnectivityIdDatasource);

            var getconniddatasourcecommand = new Command("getDataSource");
            configurationConnectivityIdDatasource.Add(getconniddatasourcecommand);
            getconniddatasourcecommand.AddOption(new Option("--connectivity-id", "connectivity id", 
                new Argument<string>{Arity = ArgumentArity.ExactlyOne} ));
            getconniddatasourcecommand.Handler = CommandHandler.Create<string>(GetConnectivityIdDataSource);

        }


        private static async Task GetConnectivityIdDataSource(string connectivityId)
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
            sb.Append("/datasource");
            await RestClient.HttpGetter(sb.ToString());
        }
    }
}
