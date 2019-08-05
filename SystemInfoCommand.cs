using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;
using System.Threading.Tasks;

namespace CmdParser
{
    internal class SystemInfoCommand
    {
        internal SystemInfoCommand(Command root)
        {

            var systeminfocommand = new Command("systeminfo");
            root.Add(systeminfocommand);

            var getStorage = new Command("getStorage");
            systeminfocommand.Add(getStorage);
            getStorage.Handler = CommandHandler.Create(GetStorage);
        }


        private static async Task GetStorage()
        {
            var sb = new StringBuilder();
            sb.Append(RestClient.baseUrl);
            sb.Append("/systeminfo");
            sb.Append("/storage");
            await RestClient.HttpGetter(sb.ToString());
        }
    }
}
