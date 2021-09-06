using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SS2.AvaloniaUI.ViewModels;

namespace SS2.AvaloniaUI.Views
{
    public partial class ControlPanel : UserControl
    {
        public ControlPanel()
        {
            InitializeComponent();
            this.LayoutUpdated += this.OnEvent;
            this.DataContextChanged += DataContextChangeHandler;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void DataContextChangeHandler(object? sender, EventArgs e) {
            if (this.DataContext != null) {
                ((ControlPanelViewModel)this.DataContext).Items.CollectionChanged += OnEvent;
            }
        }

        private void OnEvent(object? sender, EventArgs args)
        {
            ScrollViewer viewer = this.FindControl<ScrollViewer>("ResponsesScrollViewer");
            viewer.ScrollToEnd();
        }
    }
}
