using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SS2.Core.Model;

namespace SS2.Core
{
    public interface ILogicController {
        public void Start();
        public void Reset();

        public IEnumerable<Node> GetNodeList();
        public IEnumerable<Edge> GetEdgeList();

        public GameState GameState { get; }

        public event EventHandler<GameState> GameStateChanged;

        public ObservableCollection<string> Responses { get; }
        public ObservableCollection<Node> Nodes { get; }
        public ObservableCollection<Edge> Edges { get; }
    }
}