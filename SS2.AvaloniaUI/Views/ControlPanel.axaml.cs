using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SS2.AvaloniaUI.Views
{
    public partial class ControlPanel : UserControl
    {
        public ControlPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
