using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CmdParser
{
    class Program
    {

        /// <summary>
        ///https://github.com/dotnet/command-line-api
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var command = new RootCommand("rootcommand");

            var diagnosticsCommand = new DiagnosticsCommand(command);

            var systemInfoCommand = new SystemInfoCommand(command);

            var configurationCommand = new ConfigurationCommand(command);

            var configurationSystemCommand = new ConfigurationSystemCommand(configurationCommand.GetCommand());

            var configurationConnnectivityIdDataSourceCommand = new 
                ConfigurationConnectivityIdDataSource(configurationCommand.GetCommand());

            var configurationConnnectivityIdLoggingCommand = new
                ConfigurationConnectivityIdLogging(configurationCommand.GetCommand());


            command.InvokeAsync(args).Wait();
        }

      


       


       
    }
}
