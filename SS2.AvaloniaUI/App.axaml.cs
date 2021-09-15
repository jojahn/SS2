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
        public static LogicController Controller { get; } = new BasicLogicController();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow {
                    DataContext = new MainWindowViewModel(),
                    WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.Manual,
                };
                PixelPoint bottomLeft = desktop.MainWindow.Screens.Primary.WorkingArea.BottomLeft;
                desktop.MainWindow.Position = new PixelPoint(
                    bottomLeft.X + 10,
                    bottomLeft.Y - (int)desktop.MainWindow.Height - 10
                );
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}