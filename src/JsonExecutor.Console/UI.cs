using System;
using System.Collections.Generic;
using System.Linq;
using JsonExecutor.Console.Model;

namespace JsonExecutor.Console
{
    static class Ui
    {
        public static void Show(TestResult result)
        {
            var message = result.Result ? "Success" : "Failed";
            new ConsoleUi(result.Result ? MessageType.Success: MessageType.Fail ).Show($"{result.Name,30}: {result.Message}");
        }

        public static void ShowSummary(int success, int failed)
        {
            var successMessage = $"Success:{success}";
            System.Console.WriteLine("");
            new ConsoleUi(MessageType.Info).Show($"{successMessage,30}: Failed:{failed}");
        }

        public static void ShowTestFiles(IEnumerable<string> files)
        {
            System.Console.WriteLine("");
            var consoleUi = new ConsoleUi(MessageType.Info);
            files.ToList().ForEach(f => consoleUi.Show($"{f,30}"));
        }

        public static void ShowInfo(string message)
        {
            System.Console.WriteLine("___________________________________");
            var consoleUi = new ConsoleUi(MessageType.Info);
            new ConsoleUi(MessageType.Info).Show($"{message,30}");
            System.Console.WriteLine("___________________________________");
        }
    }
}
