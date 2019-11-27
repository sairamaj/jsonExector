using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AppDomainToolkit;

namespace JsonExecutor.Console
{
    [Serializable]
    public class Executor
    {
        private readonly string _testDataJson;
        private readonly string _configJson;

        public Executor(string testDataJson, string configJson)
        {
            _testDataJson = testDataJson;
            _configJson = configJson;
        }

        public void Execute(string configFile, string assemblyPath, IDictionary<string,object> variables)
        {
           // CopyAssembliesToPath(assemblyPath);
            using (var context = AppDomainContext.Create(new AppDomainSetup()
            {
                ConfigurationFile = configFile
            }))
            {
                context.RemoteResolver.AddProbePath(assemblyPath);
                RemoteAction.Invoke(
                    context.Domain,
                    "Hello World",
                    variables,
                    (msg,dict) =>
                    {
                        System.Console.WriteLine(msg);
                        System.Console.WriteLine("_______ AppDomain __________");
                        foreach (var kv in dict)
                        {
                            System.Console.WriteLine($"{kv.Key}: {kv.Value}");
                        }
                        System.Console.WriteLine("_______ AppDomain __________");
                        InternalExecute(dict);
                    });
            }
        }

        bool InternalExecute(IDictionary<string,object> variables)
        {
            var ret = false;
            //System.Console.WriteLine("________________________");
            //System.Console.WriteLine(this._testDataJson);
            //System.Console.WriteLine("________________________");

            //System.Console.WriteLine("________________________");
            //System.Console.WriteLine(this._configJson);
            //System.Console.WriteLine("________________________");

            var executor = new Framework.JsonExecutor(this._testDataJson, this._configJson, track =>
            {
                System.Console.WriteLine($"\t{track.MethodName}");
            });

            try
            {
                executor.ExecuteAndVerify(variables);
                ret = true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            return ret;
        }
    }
}
