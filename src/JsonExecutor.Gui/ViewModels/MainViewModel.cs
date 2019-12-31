using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Wpf.Util.Core;
using Wpf.Util.Core.Command;
using Wpf.Util.Core.ViewModels;

namespace JsonExecutor.Gui.ViewModels
{
    internal class MainViewModel :CoreViewModel
    {
        /// <summary>
        /// Flag for test running status.
        /// </summary>
        private bool _isRunning;

        public MainViewModel(string testPath)
        {
            this.TestFileContainers = new SafeObservableCollection<TestFileContainerViewModel>();
            this.TestFileContainers.Add(new TestFileContainerViewModel(testPath));   // will add browsing here.

            this.SelectAllCommand = new DelegateCommand(() =>
            {
                this.TestFileContainers.ToList().ForEach(container => container.SelectAll());
            });

            this.SelectNoneCommand = new DelegateCommand(() =>
            {
                this.TestFileContainers.ToList().ForEach(container => container.SelectNone());
            });

            this.RunCommand = new DelegateCommand(async () =>
            {
                await this.Run(false);
            });
            this.RunVerifyCommand = new DelegateCommand(async () => await this.Run(true));

        }

        /// <summary>
        /// Gets or sets a value indicating whether a test is running or not.
        /// </summary>
        public bool IsRunning
        {
            get => this._isRunning;
            set
            {
                this._isRunning = value;
                this.OnPropertyChanged(() => this.IsRunning);
            }
        }

        /// <summary>
        /// Gets or sets test file containers.
        /// </summary>
        public ObservableCollection<TestFileContainerViewModel> TestFileContainers { get; set; }

        /// <summary>
        /// Gets or sets Select all commands handler.
        /// </summary>
        public ICommand SelectAllCommand { get; set; }

        /// <summary>
        /// Gets or sets Select none command.
        /// </summary>
        public ICommand SelectNoneCommand { get; set; }

        /// <summary>
        /// Gets or sets run command handler.
        /// </summary>
        public ICommand RunCommand { get; set; }

        /// <summary>
        /// Gets or sets run verify command handler.
        /// </summary>
        public ICommand RunVerifyCommand { get; set; }

        /// <summary>
        /// Run all test cases.
        /// </summary>
        /// <param name="verify">
        /// true to also verify with expected values.
        /// </param>
        /// <returns>
        /// A run task.
        /// </returns>
        private async Task Run(bool verify)
        {
            try
            {
                this.IsRunning = true;
                foreach (var viewModel in this.TestFileContainers)
                {
                    await viewModel.RunAsync(verify);
                }
            }
            finally
            {
                this.IsRunning = false;
            }
        }
    }
}
