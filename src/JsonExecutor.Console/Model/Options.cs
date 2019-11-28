using CommandLine;

namespace JsonExecutor.Console.Model
{
    public class Options
    {
        [Option('p', "path", Required = true, HelpText = "Base path of the test files.")]
        public string Path { get; set; }

        [Option('f', "filter", Required = false, HelpText = "Test names filter.")]
        public string Filter { get; set; }

        [Option('l', "list", Required = false, HelpText = "List the tests.")]
        public bool List { get; set; }

        [Option('r', "run", Required = false, HelpText = "Runs the tests.")]
        public bool Run { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }
    }
}
