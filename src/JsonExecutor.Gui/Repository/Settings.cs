using System.Configuration;

namespace JsonExecutor.Gui.Repository
{
    class Settings : ISettings
    {
        public Settings()
        {
            this.TestPath = ConfigurationManager.AppSettings["testPath"];
        }

        public string TestPath { get; set; }
    }
}
