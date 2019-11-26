using System;

namespace JsonExecutor.Framework.Unit.Tests.TestTypes
{
    public static class Utilities
    {
        public static int Random()
        {
            return new Random().Next(1000);
        }

        public static string ConCat(string val1, string val2)
        {
            return val1 + val2;
        }
    }
}
