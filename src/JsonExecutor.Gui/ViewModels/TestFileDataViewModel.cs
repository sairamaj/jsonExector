using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentAssertions.Execution;
using JsonExecutor.Framework;
using JsonExecutor.Gui.Model;
using Newtonsoft.Json;
using Wpf.Util.Core;
using Wpf.Util.Core.Command;
using Wpf.Util.Core.ViewModels;

namespace JsonExecutor.Gui.ViewModels
{
    /// <summary>
    /// Test file data view model.
    /// </summary>
    public class TestFileDataViewModel : CoreViewModel
    {
        private readonly string _basePath;

        /// <summary>
        /// Test file name.
        /// </summary>
        private readonly string _fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestFileDataViewModel"/> class.
        /// </summary>
        /// <param name="basePath">Base path of the test file.</param>
        /// <param name="name">
        /// Test name.
        /// </param>
        /// <param name="fileName">
        /// Test file data.
        /// </param>
        public TestFileDataViewModel(string basePath, string name, string fileName)
        {
            this.Name = name;
            this._basePath = basePath;
            this._fileName = fileName;
            if (File.Exists(fileName))
            {
                this.Data = File.ReadAllText(fileName);
            }

            this.TraceMessages = new SafeObservableCollection<TreeViewItemViewModel>();
            this.RunTestFileCommand = new DelegateCommand(async () => { await this.Execute(false); });
            this.RunTestFileWithVerifyCommand = new DelegateCommand(async () => { await this.Execute(true); });
            this.TestStatus = TestStatus.None;
        }

        /// <summary>
        /// Gets test name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets test data.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets result data.
        /// </summary>
        public string ResultsData { get; set; }

        /// <summary>
        /// Gets or sets test status.
        /// </summary>
        public TestStatus TestStatus { get; set; }

        /// <summary>
        /// Gets run test file command.
        /// </summary>
        public ICommand RunTestFileCommand { get; }

        /// <summary>
        /// Gets run test file with verify command.
        /// </summary>
        public ICommand RunTestFileWithVerifyCommand { get; }

        /// <summary>
        /// Gets trace messages.
        /// </summary>
        public ObservableCollection<TreeViewItemViewModel> TraceMessages { get; }

        /// <summary>
        /// Executes the test.
        /// </summary>
        /// <param name="isVerify">
        /// true for verifying the results with expected one.
        /// </param>
        /// <returns>
        /// Task instance.
        /// </returns>
        public async Task Execute(bool isVerify)
        {
            await new TaskFactory().StartNew(() =>
            {
                try
                {
                    this.TraceMessages.Clear();
                    this.TestStatus = TestStatus.Running;

                    var executor = new Executor(this._basePath, this.Name);
                    var result = executor.Execute(new Dictionary<string, object>(), true);
                    this.ResultsData = result.Message;
                    this.TestStatus = result.Result ? TestStatus.Success : TestStatus.Error;
                }
                catch (AssertionFailedException ae)
                {
                    this.TestStatus = TestStatus.Error;
                    this.ResultsData = ae.Message;      // good with message.
                }
                catch (Exception e)
                {
                    this.TestStatus = TestStatus.Error;
                    this.ResultsData = e.ToString();
                }
                finally
                {
                    this.OnPropertyChanged(() => this.ResultsData);
                    this.OnPropertyChanged(() => this.TestStatus);
                }
            });
        }

        /// <summary>
        /// Trace action.
        /// </summary>
        /// <param name="traceInfo">
        /// A <see cref="ExecuteTraceInfo"/> instance.
        /// </param>
        private void TraceAction(ExecuteTraceInfo traceInfo)
        {
            ExecuteAsync(() =>
            {
                if (traceInfo.TraceType == TraceType.Verification)
                {
                    this.TraceMessages.Add(new VerificationViewModel(traceInfo));
                }
                else
                {
                    this.TraceMessages.Add(new MethodViewTreeViewModel(traceInfo));
                }
            });
        }
    }
}
