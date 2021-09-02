using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SS2.AvaloniaUI.ViewModels;
using SS2.AvaloniaUI.Views;
using SS2.Core;

namespace SS2.AvaloniaUI
{
    public class App : Application
    {
        private LogicController controller = new BasicLogicController();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    TransparencyLevelHint = Avalonia.Controls.WindowTransparencyLevel.Blur,
                    DataContext = new MainWindowViewModel(controller),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}