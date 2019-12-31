using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using JsonExecutor.Gui.ViewModels;
using Wpf.Util.Core;
using Wpf.Util.Core.Extensions;
using Wpf.Util.Core.Registration;

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
                var serviceLocator = ServiceLocatorFactory.Create(builder);
                var commandTreeMapper = serviceLocator.Resolve<ICommandTreeItemViewMapper>();
                var win = new MainWindow(commandTreeMapper);
                win.DataContext = new MainViewModel(commandTreeMapper);
                win.Show();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
