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
            this.LayoutUpdated += this.OnEvent;
            this.DataContextChanged += this.MyDataContextChangeHandler;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void MyDataContextChangeHandler(object? sender, EventArgs e) {
            if (this.DataContext != null) {
                ((NodeGridViewModel)this.DataContext).Items.CollectionChanged += OnEvent;
            }
        }

        public void OnEvent(object? sender, EventArgs e)
        {
            Console.WriteLine("---> NodeGrid Update Rows, Columns");
            // TODO: Figure out how to properly set Grid.Row and Grid.Column on ContentPresenter
            // (Grid > ContentPresenter (not styleable using "ItemControls /template/ ContentPresenter") > Button)
            ItemsControl itemsControl = this.FindControl<ItemsControl>("MyItemsControl");
            if (itemsControl != null)
            {
                SetGridPositions((Grid)itemsControl.Presenter.LogicalChildren[0]);
                // SetGridPositions((Grid)itemsControl.Presenter.VisualChildren[0]);
            }
        }

        public void SetGridPositions(Grid grid) {
            if (grid != null)
                {
                    Controls presenters = grid.Children;
                    foreach (Control presenter in presenters)
                    {
                        Button actualItem = (Button) ((ContentPresenter)presenter).Child;
                        SS2.Core.Model.Node node = (SS2.Core.Model.Node)actualItem.DataContext;
                        if (node != null) {
                            Console.WriteLine($"--> Node.Position: {node.Position.X}, {node.Position.Y}");
                            Console.WriteLine($"--> Button.Grid: {Grid.GetColumn(actualItem)}, {Grid.GetRow(actualItem)}");
                            int row = Grid.GetRow(actualItem);
                            int column = Grid.GetColumn(actualItem);
                            Grid.SetColumn(presenter, column); // (int)node.Position.X);
                            Grid.SetRow(presenter, row); // (int)node.Position.Y);
                            Console.WriteLine($"--> Presenters: {Grid.GetColumn(presenter)}, {Grid.GetRow(presenter)}");
                            Console.WriteLine("");
                        }
                    }
                }
        }
    }
}
