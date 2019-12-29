using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
