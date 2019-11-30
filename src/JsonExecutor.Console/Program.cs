using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CommandLine;
using JsonExecutor.Console.Model;

namespace JsonExecutor.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(Run);
        }

        private static void Run(Options options)
        {
            var success = 0;
            var failed = 0;
            var testFiles = GetTestFiles(options).ToList();
            if (options.List || !options.Run)
            {
                Ui.ShowTestFiles(testFiles);
            }

            if (options.ListMethods)
            {
                ListMethods(options.Path, testFiles.First());
            }

            if (!options.Run)
            {
                return;
            }

            foreach (var test in testFiles)
            {
                var testName = Path.GetFileNameWithoutExtension(test);
                Ui.ShowInfo($"Executing:{testName}");
                var result = new Executor(options.Path, testName).Execute(new Dictionary<string, object>(), options.Verbose);
                Ui.Show(result);
                if (result.Result)
                {
                    success++;
                }
                else
                {
                    failed++;
                }
            }

            Ui.ShowSummary(success, failed);
        }

        private static void ListMethods(string path, string dummyTestName)
        {
            Ui.ShowMethods(new Executor(path, dummyTestName).GetAvailableMethods());
        }

        static IEnumerable<string> GetTestFiles(Options options)
        {
            var testFiles = Directory.GetFiles(Path.Combine(options.Path, "tests"), "*.json").Select(Path.GetFileNameWithoutExtension);
            if (!string.IsNullOrWhiteSpace(options.Filter))
            {
                var filter = new Regex(options.Filter);
                testFiles = testFiles.Where(t => filter.IsMatch(t)).ToArray();
            }

            return testFiles;
        }
    }
}
