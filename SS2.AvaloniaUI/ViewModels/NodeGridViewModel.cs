using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using SS2.Core.Model;

namespace SS2.AvaloniaUI.ViewModels
{
    public class NodeGridViewModel : ViewModelBase
    {
        public NodeGridViewModel(IEnumerable<Node> nodes)
        {
            Items = new ObservableCollection<Node>(nodes);
            MyCommand = ReactiveCommand.Create(OnClick);
        }

        public ObservableCollection<Node> Items { get; }
        public string NodeGridGreeting => "Node Grid";

        public void OnClick()
        {
            throw new NotImplementedException();
        }

        public ICommand MyCommand { get; }
    }
}
