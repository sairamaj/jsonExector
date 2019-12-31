using JsonExecutor.Gui.Views;
using Wpf.Util.Core;
using Wpf.Util.Core.ViewModels;

namespace JsonExecutor.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(ICommandTreeItemViewMapper viewMapper)
        {
            InitializeComponent();
            this.TestFilesContainerView.SelectionChangedEvent += (s, e) =>
            {
                if (!(e.SelectedItem is CommandTreeViewModel viewModel))
                {
                    return;
                }

                var ctrl = new TestView { DataContext = viewModel.DataContext };
                this.DetailViewContainer.ShowView(ctrl);
            };
        }
    }
}
