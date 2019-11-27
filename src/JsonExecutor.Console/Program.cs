using System.Collections.Generic;
using System.IO;

namespace JsonExecutor.Console
{
    class Program
    {
        private static string AssemblyPath;
        static void Main(string[] args)
        {
            var testBasePath = args[0];
            IDictionary<string, bool> testResults = new Dictionary<string, bool>();
            foreach (var test in Directory.GetFiles(Path.Combine(testBasePath,"tests"), "*.json"))
            {
                var testName = Path.GetFileNameWithoutExtension(test);
                var ret = new Executor(testBasePath, testName).Execute(new Dictionary<string, object>());
                var msg = ret ? "success" : "failed";
                System.Console.WriteLine($"{testName} {msg}");
            }

            foreach (var test in testResults)
            {
                System.Console.WriteLine($"{test.Key,30}:{test.Value}");
            }
        }
    }
}
