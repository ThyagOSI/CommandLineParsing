using System;
using System.CommandLine;
using System.CommandLine.Invocation;

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

            try
            {
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

      


       


       
    }
}
