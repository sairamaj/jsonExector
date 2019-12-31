using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Wpf.Util.Core;

namespace JsonExecutor.Gui.ViewModels
{
    class MainViewModel
    {
        public MainViewModel(ICommandTreeItemViewMapper viewMapper)
        {
            this.TestFileContainers = new SafeObservableCollection<TestFileContainerViewModel>();
            this.TestFileContainers.Add(new TestFileContainerViewModel(@"c:\temp\temp\sample"));   // will add browsing here.
        }

        /// <summary>
        /// Gets or sets test file containers.
        /// </summary>
        public ObservableCollection<TestFileContainerViewModel> TestFileContainers { get; set; }
    }
}
