using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using SS2.AvaloniaUI.Logic;
using System;
using System.Collections.Generic;
using System.Text;
using SS2.Core;

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

        void Onb2Click(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            //logic to handle the Click event     
            //                 desktop.MainWindow.Presenter.AddHandler(Avalonia.Controls.Panel.PointerPressedEvent, Onb2Click);

        }
    }
}
