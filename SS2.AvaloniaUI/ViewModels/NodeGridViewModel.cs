using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using SS2.Core.Model;

namespace SS2.AvaloniaUI.ViewModels
{
    public class NodeGridViewModel : ViewModelBase
    {
        public ObservableCollection<Node> Items { get; set; }
        public ICommand OnNodeClickCommand { get; }

        public NodeGridViewModel(IEnumerable<Node> nodes)
        {
            Items = new ObservableCollection<Node>(App.Controller.GetNodeList());
            App.Controller.SubscribeToNodeList(OnChange);
            // Items.CollectionChanged += OnChange;
            OnNodeClickCommand = ReactiveCommand.Create<Node, bool>(OnNodeClick);
        }


        public bool OnNodeClick(Node node)
        {
            App.Controller.OnNodeClicked(node);
            IEnumerable<Node> nodes = App.Controller.GetNodeList();
            Node? current = Items.FirstOrDefault(n => n.Id.Equals(node.Id));
            Node? updated = nodes.FirstOrDefault(n => n.Id.Equals(node.Id));
            if (null != current && null != updated) {
                int index = Items.IndexOf(current);
                Items.Remove(current);
                current.Failed = updated.Failed;
                current.Activated = current.Activated;
                Items.Insert(index, current);
            }
            // Node? item = nodes.FirstOrDefault(n => n.Id.Equals(node.Id));
            // item.Activated = updated.Activated;
            return true;
        }

        public void OnChange(object? sender, EventArgs args)
        {
            Items.Clear();
            List<Node> nodes = (List<Node>)App.Controller.GetNodeList();// (IReadOnlyList<Node>)((NotifyCollectionChangedEventArgs)args).NewItems;
            if (nodes != null)
            {
                foreach (Node node in nodes)
                {
                    Items.Add(node);
                }
            }
        }

    }
}
