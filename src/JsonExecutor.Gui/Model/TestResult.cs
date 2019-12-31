using System;

namespace JsonExecutor.Gui.Model
{
    [Serializable]
    public class TestResult
    {
        public TestResult(string name, bool result, string message)
        {
            this.Name = name;
            this.Result = result;
            this.Message = message;
        }

        public string Name { get; }
        public bool Result { get; }
        public string Message { get; }
    }
}
