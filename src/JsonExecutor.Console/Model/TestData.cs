using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace JsonExecutor.Console.Model
{
    class TestData
    {
        public TestData(string path)
        {
            Path = path;
        }
        public string Path { get; }

        public string ConfigurationJson
        {
            get
            {
                var configFileName = System.IO.Path.Combine(this.Path, "config.json");
                return File.ReadAllText(configFileName);
            }
        }

        public IDictionary<string, object> Variables
        {
            get
            {
                var variablesFileName = System.IO.Path.Combine(this.Path, "variables.json");
                return JsonConvert.DeserializeObject<IDictionary<string, object>>(File.ReadAllText(variablesFileName));
            }
        }

        public IEnumerable<Tuple<string, string>> Tests
        {
            get
            {
                var excludes = new string[] { "config.json", "variables.json" };
                foreach (var file in Directory.GetFiles(this.Path, "*.json").Where(f => !excludes.Contains(System.IO.Path.GetFileName(f))))
                {
                    yield return new Tuple<string, string>(
                        System.IO.Path.GetFileNameWithoutExtension(file),
                        File.ReadAllText(file)
                    );
                }
            }
        }
    }
}
