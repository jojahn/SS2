using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS2.Core.Model;

namespace SS2.Core
{
    public class BasicLogicController : LogicController
    {
        private List<Node> _nodes;
        private Random _random = new Random();
        private ObservableCollection<Node> _observableNodes;
        private ObservableCollection<string> _observableResponses;
        private event EventHandler<GameState> _gameStateChanged;


        public BasicLogicController() : base()
        {
            _observableNodes = new ObservableCollection<Node>();
            foreach(Node n in _nodes) {
                _observableNodes.Add(n);
            }
            _observableResponses = new ObservableCollection<string>(this.Responses);
        }

        public override GameState CheckState()
        {
            if (GameState == GameState.STARTED)
            {
                List<Node> remainingNodes = _nodes.Where((Node node) => !node.Activated || node.Failed).ToList();
                if (remainingNodes.Count == 0)
                {
                    return GameState.FAILED;
                }

            }
            return GameState;
        }

        public override void GenerateNodes()
        {
            _nodes = new List<Node>();
            for (int i = 0; i < NumberOfNodes; i++)
            {
                Node node = new Node(LogicController.NodePositions[i], false, 0.5);
                _nodes.Add(node);
            }
        }

        public override List<Node> GetNodeList()
        {
            return _nodes;
        }

        public override void ResetNodes()
        {
            foreach(Node node in _nodes)
            {
                node.Reset();
                _observableNodes.Remove(_observableNodes.First(n => n.Id.Equals(node.Id)));
                _observableNodes.Add(node);
            }
        }

        public override bool TryNode(Node node)
        {
            return _random.NextDouble() > node.Chance;
        }

        public override void OnNodeClicked(Node node)
        {
            base.OnNodeClicked(node);
            Node foundNode = GetNodeById(node.Id);
            List<string> responses = Responses
                .Skip(_observableResponses.Count)
                .ToList();
            foreach (string res in responses)
            {
                _observableResponses.Add(res);
            }
            _observableNodes.Remove(_observableNodes.First(n => n.Id.Equals(node.Id)));
            _observableNodes.Add(foundNode);
        }

        public override void SubscribeToNodeList(EventHandler eventHandler)
        {
            NotifyCollectionChangedEventHandler ev = (object sender, NotifyCollectionChangedEventArgs args) => {
                eventHandler.Invoke(sender, args);
            };
            _observableNodes.CollectionChanged += ev;
        }

        public override void SubscribeToResponses(EventHandler eventHandler)
        {
            NotifyCollectionChangedEventHandler ev = (object sender, NotifyCollectionChangedEventArgs args) => {
                eventHandler.Invoke(sender, args);
            };
            _observableResponses.CollectionChanged += ev;
        }

        public override void SubscribeToGameState(EventHandler<GameState> eventHandler)
        {
            _gameStateChanged += eventHandler;
        }

        protected override void MakeInitialLines()
        {
            base.MakeInitialLines();
            if (_observableResponses != null) {
                _observableResponses.Clear();
                foreach(string res in Responses) {
                    _observableResponses.Add(res);
                }
            } else {
                _observableResponses = new ObservableCollection<string>(Responses);
            }

            // _observableResponses = new ObservableCollection<string>(Responses);
        }

        public override Node GetNodeById(Guid id)
        {
            return _nodes.First(n => n.Id.Equals(id));
        }

        private void publishGameState()
        {
            EventHandler<GameState> handler = _gameStateChanged;
            if (handler != null)
            {
                handler(this, this.GameState);
            }
        }
    }
}
