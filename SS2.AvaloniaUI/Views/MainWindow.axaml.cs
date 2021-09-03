using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace SS2.AvaloniaUI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            Panel panel = this.FindControl<Panel>("MainPanel");
            panel.PointerPressed += OnPointerPressed;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void OnPointerPressed(object sender, PointerPressedEventArgs args)
        {
            if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow.BeginMoveDrag(args);
            }
        }
    }
}