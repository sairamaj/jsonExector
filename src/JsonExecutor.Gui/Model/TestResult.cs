using System;
using System.Collections.Generic;
using JsonExecutor.Framework;

namespace JsonExecutor.Gui.Model
{
    [Serializable]
    public class TestResult
    {
        private readonly IEnumerable<string> _traces;
        public TestResult(string name, bool result, string message, IEnumerable<string> traces)
        {
            this.Name = name;
            this.Result = result;
            this.Message = message;
            this._traces = traces;
        }

        public string Name { get; }
        public bool Result { get; }
        public string Message { get; }

        public IEnumerable<string> Traces => this._traces;

    }
}
