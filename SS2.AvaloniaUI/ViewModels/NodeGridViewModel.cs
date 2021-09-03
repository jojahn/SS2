using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Items.CollectionChanged += OnChange;
            OnNodeClickCommand = ReactiveCommand.Create<Node, bool>(OnNodeClick);
            foreach(Node node in nodes)
            {
                // node.Activated = true;
            }
        }


        public bool OnNodeClick(Node node)
        {
            App.Controller.OnNodeClicked(node);
            node.Activated = true;
            return true;
        }

        public void OnChange(object sender, EventArgs e)
        {
            var a = e;
        }

    }
}
