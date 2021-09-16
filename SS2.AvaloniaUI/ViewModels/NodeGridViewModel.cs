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
using SS2.Core.Resources;

namespace SS2.AvaloniaUI.ViewModels
{
    public class NodeGridViewModel : ViewModelBase
    {
        public ObservableCollection<Node> Nodes { get; set; }
        public ObservableCollection<Edge> Edges { get; set; }
        public ICommand OnNodeClickCommand { get; }

        private bool _gameStateEnabled = false;
        public bool GameStateEnabled
        {
            get => _gameStateEnabled;
            set => this.RaiseAndSetIfChanged(ref _gameStateEnabled, value);
        }
        private string _gameStateText = "";
        public string GameStateText
        {
            get => _gameStateText;
            set => this.RaiseAndSetIfChanged(ref _gameStateText, value);
        }

        public NodeGridViewModel(IEnumerable<Node> nodes)
        {
            Nodes = new ObservableCollection<Node>(App.Controller.GetNodeList());
            Edges = new ObservableCollection<Edge>(App.Controller.GetEdgeList());
            App.Controller.SubscribeToNodeList(OnChange);
            App.Controller.SubscribeToEdgeList(OnEdgesChange);
            App.Controller.SubscribeToGameState(OnGameStateChange);
            OnNodeClickCommand = ReactiveCommand.Create<Node>(OnNodeClick);
            SetGameStateDisplay(GameState.IDLE);
        }


        public void OnNodeClick(Node node)
        {
            if (node.Activated || node.Failed) {
                return;
            }
            App.Controller.OnNodeClicked(node);
            /*IEnumerable<Node> nodes = App.Controller.GetNodeList();
            Node? current = Nodes.FirstOrDefault(n => n.Id.Equals(node.Id));
            Node? updated = nodes.FirstOrDefault(n => n.Id.Equals(node.Id));
            if (null != current && null != updated) {
                int index = Nodes.IndexOf(current);
                Nodes.Remove(current);
                current.Failed = updated.Failed;
                current.Activated = current.Activated;
                Nodes.Insert(index, current);
            }*/
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

        public void OnGameStateChange(object? sender, GameState args)
        {
            SetGameStateDisplay(args);
        }

        private void SetGameStateDisplay(GameState gameState)
        {
            if (gameState != GameState.STARTED)
            {
                GameStateEnabled = true;
            }
            else
            {
                GameStateEnabled = false;
            }

            if (gameState == GameState.FAILED)
            {
                GameStateText = Resources.CriticalFailure;
            }
            else if (gameState == GameState.WON)
            {
                GameStateText = Resources.Success;
            }
            else if (gameState == GameState.IDLE)
            {
                GameStateText = Resources.ClickStart;
            }
        }
    }
}
