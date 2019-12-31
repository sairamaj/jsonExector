using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using Autofac;
using JsonExecutor.Gui.Repository;
using JsonExecutor.Gui.ViewModels;
using Wpf.Util.Core.Extensions;
using Wpf.Util.Core.Registration;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace JsonExecutor.Gui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            this.DispatcherUnhandledException += (s, ex) =>
            {
                Trace.WriteLine(ex.Exception.GetExceptionDetails());
                MessageBox.Show(ex.Exception.GetExceptionDetails());
                System.Environment.Exit(-1);
            };

            try
            {
                var builder = new ContainerBuilder();
                builder.RegisterType<Settings>().As<ISettings>();
                var serviceLocator = ServiceLocatorFactory.Create(builder);
                var testPath = serviceLocator.Resolve<ISettings>().TestPath;
                if (string.IsNullOrEmpty(testPath))
                {
                    testPath = GetTestFilePath();
                }

                if (testPath == null)
                {
                    return;
                }

                var win = new MainWindow();
                win.DataContext = new MainViewModel(testPath);
                win.Show();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        string GetTestFilePath()
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            var result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                return dlg.SelectedPath;
            }

            return null;
        }
    }
}
