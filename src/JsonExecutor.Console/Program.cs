using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JsonExecutor.Console.Model;

namespace JsonExecutor.Console
{
    class Program
    {
        private static string AssemblyPath;
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            var testData = new TestData(args[0]);
            AssemblyPath = Path.Combine(args[0], "Assemblies");
            IDictionary<string, bool> testResults = new Dictionary<string, bool>();
            foreach (var test in testData.Tests)
            {
                System.Console.WriteLine($"Executing {test.Item1}...");
                var executor = new Framework.JsonExecutor(test.Item2, testData.ConfigurationJson, track =>
                {
                    System.Console.WriteLine($"\t{track.MethodName}");
                });

                try
                {
                    executor.ExecuteAndVerify(testData.Variables);
                    testResults[test.Item1] = true;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                    testResults[test.Item1] = false;
                }
            }

            foreach (var test in testResults)
            {
                System.Console.WriteLine($"{test.Key,30}:{test.Value}");
            }
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var excludes = new string[] {"nspec", "xunit.assert"};
            var assemblyName = args.Name.Split(',').First();
            if (excludes.Contains(assemblyName))
            {
                return null;
            }
            System.Console.WriteLine("____________________________");
            System.Console.WriteLine(args.Name);
            System.Console.WriteLine("____________________________");

            try
            {
                var found = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == assemblyName);
                if (found != null)
                {
                    System.Console.WriteLine($"Assembly {found.GetName().Name}");
                    return found;
                }

                var fileName = Path.Combine(AssemblyPath, assemblyName) + ".dll";
                System.Console.WriteLine($"Loading {fileName}");
                return Assembly.LoadFrom(fileName);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }

            return null;
        }
    }

}
