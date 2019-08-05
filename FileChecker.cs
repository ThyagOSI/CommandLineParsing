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
        private string _filePath;
        private string _jsonFilecontent;
        internal FileChecker(string importFilePath)
        {
            _filePath = importFilePath;
        }

        internal Tuple<bool, string> ValidateFile()
        {
            if (!File.Exists(_filePath))
            {
                return Tuple.Create(false, "File path is invalid");
            }


            try
            {
                _jsonFilecontent = JToken.Parse(File.ReadAllText(_filePath)).ToString();
            }
            catch (JsonException jex)
            {
                return Tuple.Create(false, $"Invalid JSON format\n{jex.Message}");
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, ex.Message);
            }

            return Tuple.Create(true, _jsonFilecontent);

        }

    }
}
