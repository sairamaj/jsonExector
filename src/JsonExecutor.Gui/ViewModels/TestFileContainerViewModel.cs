using System;
using System.IO;
using System.Linq;
using Wpf.Util.Core.ViewModels;

namespace JsonExecutor.Gui.ViewModels
{
    internal class TestFileContainerViewModel : CommandTreeViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestFileContainerViewModel"/> class.
        /// </summary>
        /// <param name="testFileContainerPath">
        /// Test file container path containing the test files.
        /// </param>
        public TestFileContainerViewModel(string testFileContainerPath)
            : base(null, Path.GetFileNameWithoutExtension(testFileContainerPath), Path.GetFileNameWithoutExtension(testFileContainerPath))
        {
            this.TestFileContainerPath = testFileContainerPath ?? throw new ArgumentNullException(nameof(testFileContainerPath));
            this.IsExpanded = true;
        }

        /// <summary>
        /// Gets test file container path.
        /// </summary>
        public string TestFileContainerPath { get; }

        /// <summary>
        /// Loads test files.
        /// </summary>
        protected override void LoadChildren()
        {
            base.LoadChildren();
            Directory.GetFiles(Path.Combine(this.TestFileContainerPath,"tests"), "*.json")
                .Select(t => new TestFileViewModel(t, true)).ToList()
                .ForEach(t => this.Children.Add(t));
        }
    }
}
