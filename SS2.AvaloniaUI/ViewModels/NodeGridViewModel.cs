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
        public ObservableCollection<Node> Nodes { get; set; }
        public ObservableCollection<Edge> Edges { get; set; }
        public ICommand OnNodeClickCommand { get; }

        public NodeGridViewModel(IEnumerable<Node> nodes)
        {
            Nodes = new ObservableCollection<Node>(App.Controller.GetNodeList());
            Edges = new ObservableCollection<Edge>(App.Controller.GetEdgeList());
            App.Controller.SubscribeToNodeList(OnChange);
            App.Controller.SubscribeToEdgeList(OnEdgesChange);
            OnNodeClickCommand = ReactiveCommand.Create<Node>(OnNodeClick);
        }


        public void OnNodeClick(Node node)
        {
            if (node.Activated || node.Failed) {
                return;
            }
            App.Controller.OnNodeClicked(node);
            IEnumerable<Node> nodes = App.Controller.GetNodeList();
            Node? current = Nodes.FirstOrDefault(n => n.Id.Equals(node.Id));
            Node? updated = nodes.FirstOrDefault(n => n.Id.Equals(node.Id));
            if (null != current && null != updated) {
                int index = Nodes.IndexOf(current);
                Nodes.Remove(current);
                current.Failed = updated.Failed;
                current.Activated = current.Activated;
                Nodes.Insert(index, current);
            }
            return;
        }

        public void OnChange(object? sender, EventArgs args)
        {
            Nodes.Clear();
            List<Node> nodes = (List<Node>)App.Controller.GetNodeList();// (IReadOnlyList<Node>)((NotifyCollectionChangedEventArgs)args).NewItems;
            if (nodes != null)
            {
                foreach (Node node in nodes)
                {
                    Nodes.Add(node);
                }
            }
        }

        public void OnEdgesChange(object? sender, EventArgs args)
        {
            Edges.Clear();
            List<Edge> edges = (List<Edge>)App.Controller.GetEdgeList();// (IReadOnlyList<Node>)((NotifyCollectionChangedEventArgs)args).NewItems;
            if (edges != null)
            {
                foreach (Edge edge in edges)
                {
                    Edges.Add(edge);
                }
            }
        }

    }
}
