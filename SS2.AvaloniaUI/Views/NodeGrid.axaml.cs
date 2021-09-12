using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Shapes;
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
            this.LayoutUpdated += this.OnEvent;
            this.DataContextChanged += this.MyDataContextChangeHandler;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void MyDataContextChangeHandler(object? sender, EventArgs e) {
            if (this.DataContext != null) {
                ((NodeGridViewModel)this.DataContext).Nodes.CollectionChanged += OnEvent;
            }
        }

        public void OnEvent(object? sender, EventArgs e)
        {
            SetGridPositions("NodesItemControl",
                (object n) => (int)((SS2.Core.Model.Node)n).Position.X,
                (object n) => (int)((SS2.Core.Model.Node)n).Position.Y);
            SetGridPositions("EdgesItemControl",
                (object n) => {
                    SS2.Core.Model.Edge edge = (SS2.Core.Model.Edge)n;
                    return (int)(edge.IsHorizontal ? edge.From.X : edge.To.X);
                },
                (object n) => {
                    SS2.Core.Model.Edge edge = (SS2.Core.Model.Edge)n;
                    return (int)(edge.IsHorizontal ? edge.To.Y : edge.From.Y);
                });
        }

        public void SetGridPositions(string itemControlId, Func<object, int> getColumn, Func<object, int> getRow) {
            // TODO: Figure out how to properly set Grid.Row and Grid.Column on ContentPresenter
            // (Grid > ContentPresenter (not styleable using "ItemControls /template/ ContentPresenter") > Button)
            ItemsControl itemsControl = this.FindControl<ItemsControl>(itemControlId);
            if (itemsControl == null)
            {
                return;
            }
            Grid grid = (Grid)itemsControl.Presenter.LogicalChildren[0];
            if (grid != null)
                {
                    Controls presenters = grid.Children;
                    foreach (Control presenter in presenters)
                    {
                        Control? actualItem = (Control)((ContentPresenter)presenter).Child;
                        object dataContext = actualItem?.DataContext;
                        if (null != dataContext) {
                            Grid.SetColumn(presenter, getColumn(dataContext));
                            Grid.SetRow(presenter, getRow(dataContext));
                        }
                    }
                }
        }
    }
}
