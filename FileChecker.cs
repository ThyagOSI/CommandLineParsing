using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CmdParser
{
    internal class FileChecker
    {
        private static string filePath;
        private static string jsonFilecontent;
        internal FileChecker(string importFilePath)
        {
            filePath = importFilePath;
        }

        internal Tuple<bool, string> ValidateFile()
        {
            if (!File.Exists(filePath))
            {
                return Tuple.Create(false, "File path is invalid");
            }


            try
            {
                jsonFilecontent = JToken.Parse(File.ReadAllText(filePath)).ToString();
            }
            catch (JsonException jex)
            {
                return Tuple.Create(false, $"Invalid JSON format\n{jex.Message}");
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, ex.Message);
            }

            return Tuple.Create(true, jsonFilecontent);

        }

    }
}
