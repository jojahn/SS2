using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SS2.AvaloniaUI.ViewModels;

namespace SS2.AvaloniaUI.Views
{
    public partial class Node : UserControl
    {
        public Node()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
