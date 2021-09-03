using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using SS2.AvaloniaUI.ViewModels;
using SS2.Core.Model;
using System;
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
            ItemsControl ctrl = this.FindControl<ItemsControl>("MyItemsControl");
            Grid grid = this.FindControl<Grid>("MyGrid");
            if (ctrl != null)
            {
                IItemsPresenter pres = ctrl.Presenter;
                if (pres != null)
                {
                    var s = pres.Styles;
                }
            }
            this.EffectiveViewportChanged += this.OnEvent;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void OnEvent(object sender, EventArgs e)
        {
            // TODO: Figure out how to properly set Grid.Row and Grid.Column on ContentPresenter
            // (Grid > ContentPresenter (not styleable using "ItemControls /template/ ContentPresenter") > Button)
            ItemsControl itemsControl = this.FindControl<ItemsControl>("MyItemsControl");
            if (itemsControl != null)
            {
                Grid grid = (Grid)itemsControl.Presenter.LogicalChildren[0];
                if (grid != null)
                {
                    Controls presenters = grid.Children;
                    foreach (Control presenter in presenters)
                    {
                        Button actualItem = (Button) ((ContentPresenter)presenter).Child;
                        SS2.Core.Model.Node node = (SS2.Core.Model.Node)actualItem.DataContext;
                        // actualItem.Command = MyCommand();
                        Grid.SetColumn(presenter, (int)node.Position.X);
                        Grid.SetRow(presenter, (int)node.Position.Y);
                    }
                }
            }
        }
    }
}
