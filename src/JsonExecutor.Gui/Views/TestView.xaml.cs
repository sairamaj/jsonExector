using System.Reflection;
using System.Xml;

namespace JsonExecutor.Gui.Views
{
    /// <summary>
    /// Interaction logic for TestView.xaml
    /// </summary>
    public partial class TestView
    {
        public TestView()
        {
            InitializeComponent();

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "JsonExecutor.Gui.AvalonJsonSyntax.xml";

            using (var xshd_stream = assembly.GetManifestResourceStream(resourceName))
            {
                var xshd_reader = new XmlTextReader(xshd_stream);
                this.TextEditor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(xshd_reader, ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);
            }
        }                                              
    }
}
