using System;
using System.Collections.Generic;
using System.IO;
using AppDomainToolkit;
using JsonExecutor.Console.Model;
using JsonExecutor.Framework;
using Newtonsoft.Json;

namespace JsonExecutor.Console
{
    [Serializable]
    public class Executor
    {
        private readonly string _basePath;
        private readonly string _testFileName;
        private readonly string _binPath;
        private readonly string _appConfigFile;
        private readonly string _variablesFile;
        private readonly string _configFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="Executor"/> class.
        /// </summary>
        /// <param name="basePath">
        /// Base path of the test data.
        ///     bin\                API assembly and any dependencies
        ///     test\               Test Files
        ///     app.config          Application configuration file (standard .NET app.config)
        ///     variables.json      Variables for all the tests
        ///     config.json         Configuration containing the type names.
        /// </param>
        /// <param name="testName">
        /// Test name which corresponds to a file name.
        /// </param>
        public Executor(string basePath, string testName)
        {
            this.TestName = testName ?? throw new ArgumentNullException(nameof(testName));
            this._basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));

            var testFileFullPath = Path.Combine(Path.Combine(basePath, "tests"), testName) + ".json";
            if (!File.Exists(testFileFullPath))
            {
                throw new ArgumentException($"{testFileFullPath} does not exist.");
            }

            this._testFileName = testFileFullPath;
            this._binPath = Path.Combine(basePath, "bin");
            this._appConfigFile = Path.Combine(basePath, "app.config");
            this._variablesFile = Path.Combine(basePath, "variables.json");
            this._configFile = Path.Combine(basePath, "config.json");
        }

        public string TestName { get; }

        public TestResult Execute(IDictionary<string, object> variables, bool verbose)
        {
            // CopyAssembliesToPath(assemblyPath);
            using (var context = AppDomainContext.Create(new AppDomainSetup()
            {
                ConfigurationFile = this._appConfigFile
            }))
            {
                context.RemoteResolver.AddProbePath(this._binPath);
                return RemoteFunc.Invoke(context.Domain, variables, verbose, this.InternalExecute);
            }
        }

        private TestResult InternalExecute(IDictionary<string, object> runtimeVariables, bool verboseOptions)
        {
            try
            {
                var ret = false;
                var testDataJson = File.ReadAllText(this._testFileName);
                var configJson = File.ReadAllText(this._configFile);
                var fileVariables = JsonConvert.DeserializeObject<IDictionary<string, object>>(File.ReadAllText(this._variablesFile));

                // override with incoming variables
                foreach (var kv in runtimeVariables)
                {
                    fileVariables[kv.Key] = kv.Value;
                }

                var executor = new Framework.JsonExecutor(testDataJson, configJson, track =>
                {
                    if (verboseOptions)
                    {
                        System.Console.WriteLine(track.TraceType);
                        System.Console.WriteLine(track.TestInfo?.Name);
                        System.Console.WriteLine(track.TestInfo?.Api);
                        System.Console.WriteLine(track.MethodName);
                        System.Console.WriteLine(track.MethodReturnValue);
                        System.Console.WriteLine(track.MethodException);
                    }
                });

                executor.ExecuteAndVerify(fileVariables);
                return new TestResult(this.TestName, true, "Success");
            }
            catch (Exception e)
            {
                if (verboseOptions)
                {
                    if (e.InnerException != null)
                    {
                        System.Console.WriteLine(e.ToString());
                    }

                    System.Console.WriteLine(e.ToString());
                }

                return new TestResult(this.TestName, false, e.Message);
            }
        }
    }
}
