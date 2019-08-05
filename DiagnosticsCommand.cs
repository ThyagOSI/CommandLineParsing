using System;
using System.Collections.Generic;
using System.Text;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace CmdParser
{
    internal class DiagnosticsCommand
    {
        internal DiagnosticsCommand(Command root)
        {
            var diagnosticscommand = new Command("diagnostics");
            root.Add(diagnosticscommand);

            var getProductInfoCommand = new Command("getProductInfo");
            diagnosticscommand.Add(getProductInfoCommand);
            getProductInfoCommand.Handler = CommandHandler.Create(GetProductInformation);

            var getSystem = new Command("getSystem");
            diagnosticscommand.Add(getSystem);
            getSystem.Handler = CommandHandler.Create(GetSystem);

        }

        private static async Task GetProductInformation()
        {
            var sb = new StringBuilder();
            sb.Append(RestClient.baseUrl);
            sb.Append("/diagnostics");
            sb.Append("/productinformation");
            await RestClient.HttpGetter(sb.ToString());
        }

        private static async Task GetSystem()
        {
            var sb = new StringBuilder();
            sb.Append(RestClient.baseUrl);
            sb.Append("/diagnostics");
            sb.Append("/system");
            await RestClient.HttpGetter(sb.ToString());
        }
    }
}
