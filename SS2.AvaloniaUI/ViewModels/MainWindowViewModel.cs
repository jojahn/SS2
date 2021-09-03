using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using SS2.AvaloniaUI.Logic;
using System;
using System.Collections.Generic;
using System.Text;
using SS2.Core;
using System.Windows.Input;
using ReactiveUI;

namespace SS2.AvaloniaUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private LogicController _controller;
        public MainWindowViewModel(LogicController controller)
        {
            _controller = controller;
            MyNodeGrid = new NodeGridViewModel(controller.GetNodeList());
            MyControlPanel = new MyControlPanelViewModel();
            CloseWindowCommand = ReactiveCommand.Create(CloseWindow);
        }

        public string Greeting => "Hack to open crate.\nCritical failure destorys it.";

        public NodeGridViewModel MyNodeGrid { get; }
        public MyControlPanelViewModel MyControlPanel { get; }

        public void BeginMoveDrag(object sender, Avalonia.Input.PointerPressedEventArgs args)
        {
            var window = Application.Current.ApplicationLifetime;
            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow.BeginMoveDrag(args);
            }
        }

        public void CloseWindow()
        {
            if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow.Close();
            }
        }

        public ICommand CloseWindowCommand { get; }

        void Onb2Click(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            //logic to handle the Click event     
            //                 desktop.MainWindow.Presenter.AddHandler(Avalonia.Controls.Panel.PointerPressedEvent, Onb2Click);

        }
    }
}
