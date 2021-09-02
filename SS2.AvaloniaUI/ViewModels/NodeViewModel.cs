using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS2.Core.Model;

namespace SS2.AvaloniaUI.ViewModels
{
    public class NodeViewModel : ViewModelBase
    {
        private Node _node { get; set; }

        public NodeViewModel(Node node)
        {
            this._node = node;
        }

        public void OnClickCommand()
        {
            Console.WriteLine("Clicked on Node");
        }
    }
}
