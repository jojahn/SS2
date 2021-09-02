using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SS2.AvaloniaUI.ViewModels;
using System.Collections.Generic;

namespace SS2.AvaloniaUI.Views
{
    public partial class NodeGrid : UserControl
    {
        public NodeGrid()
        {
            // IEnumerable<AvalonSS2.Models.Node> nodes = new List<AvalonSS2.Models.Node>();
            // DataContext = new NodeGridViewModel(nodes);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
