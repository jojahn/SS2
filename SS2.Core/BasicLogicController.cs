using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SS2.Core.Model;

namespace SS2.Core
{
    public class BasicLogicController : LogicController
    {
        private Random _random = new Random();
        private event EventHandler<GameState> _gameStateChanged;


        public BasicLogicController() : base()
        {
            publishGameState();
        }

        public override void Start()
        {
            base.Start();
            publishGameState();
        }

        public override void Reset()
        {
            base.Reset();
            publishGameState();
        }

        public override GameState CheckState()
        {
            if (GameState == GameState.STARTED)
            {
                List<Node> remainingNodes = Nodes.Where((Node node) => !node.Activated || node.Failed).ToList();
                if (remainingNodes.Count == 0)
                {
                    return GameState.FAILED;
                }

            }
            return GameState;
        }

        protected override void GenerateNodes()
        {
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < NumberOfNodes; i++)
            {
                Node node = new Node(LogicController.NodePositions[i], false, 0.5);
                nodes.Add(node);
            }
            Nodes = new ObservableCollection<Node>(nodes);
        }

        public override List<Node> GetNodeList()
        {
            return Nodes.ToList();
        }

        protected override void ResetNodes()
        {
            List<Node> nodes = Nodes.ToList();
            foreach(Node node in nodes)
            {
                node.Reset();
                Nodes.Remove(node);
                Nodes.Add(node);
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
                .Skip(Responses.Count)
                .ToList();
            foreach (string res in responses)
            {
                Responses.Add(res);
            }
            Nodes.Remove(Nodes.First(n => n.Id.Equals(node.Id)));
            Nodes.Add(foundNode);
            _updateEdges(node);
            List<Node> unclickedNodes = Nodes
                .Where(n => !n.Activated && !n.Failed)
                .ToList();
            if (unclickedNodes.Count == 0 && GameState != GameState.WON)
            {
                GameState = GameState.FAILED;
                publishGameState();
            }
            publishGameState();
        }

        public override void SubscribeToNodeList(EventHandler eventHandler)
        {
            NotifyCollectionChangedEventHandler ev = (object sender, NotifyCollectionChangedEventArgs args) => {
                eventHandler.Invoke(sender, args);
            };
            Nodes.CollectionChanged += ev;
        }

        public override void SubscribeToResponses(EventHandler eventHandler)
        {
            NotifyCollectionChangedEventHandler ev = (object sender, NotifyCollectionChangedEventArgs args) => {
                eventHandler.Invoke(sender, args);
            };
            Responses.CollectionChanged += ev;
        }

        public override void SubscribeToGameState(EventHandler<GameState> eventHandler)
        {
            _gameStateChanged += eventHandler;
        }

        public override Node GetNodeById(Guid id)
        {
            return Nodes.First(n => n.Id.Equals(id));
        }

        private void publishGameState()
        {
            EventHandler<GameState> handler = _gameStateChanged;
            if (handler != null)
            {
                handler(this, this.GameState);
            }
        }

        public override IEnumerable<Edge> GetEdgeList()
        {
            return (List<Edge>)Edges.ToList();
        }

        protected override void ResetEdges()
        {
            List<Edge> edges = Edges.ToList();
            foreach(Edge edge in edges)
            {
                edge.Reset();
                Edges.Remove(edge);
                Edges.Add(edge);
            }
        }

        protected override void GenerateEdges()
        {
            List<Edge> edges = new List<Edge>();
            for (int i = 0; i < NumberOfEdges; i++)
            {
                Edge edge = new Edge(LogicController.EdgeConnections[i][0], LogicController.EdgeConnections[i][1]);
                edges.Add(edge);
            }
            Edges = new ObservableCollection<Edge>(edges);
        }

        public override void SubscribeToEdgeList(EventHandler eventHandler)
        {
            NotifyCollectionChangedEventHandler ev = (object sender, NotifyCollectionChangedEventArgs args) => {
                eventHandler.Invoke(sender, args);
            };
            Edges.CollectionChanged += ev;
        }

        private void _updateEdges(Node node)
        {
            List<Node> neighbors = Nodes
                .Where(n => !n.Id.Equals(node.Id))
                .Where(n => n.Position.X == node.Position.X || n.Position.Y == node.Position.Y)
                .ToList();
            foreach(Node neighbor in neighbors)
            {
                Edge edge = null;
                if (node.Activated && neighbor.Activated)
                {
                    edge = GetConnectingEdge(node, neighbor);
                    if (null != edge)
                    {
                        edge.Activated = true;
                    }
                }

                List<Node> secondNeighbors = Nodes
                    .Where(n => !n.Id.Equals(node.Id) && !n.Id.Equals(neighbor.Id))
                    .Where(n => (n.Position.X == neighbor.Position.X && node.Position.X == n.Position.X)
                        || (n.Position.Y == neighbor.Position.Y && n.Position.Y == node.Position.Y))
                    .ToList();
                foreach(Node secondNeighbor in secondNeighbors)
                {
                    Edge secondEdge = GetConnectingEdge(secondNeighbor, neighbor);
                    if (null == secondEdge)
                    {
                        secondEdge = GetConnectingEdge(secondNeighbor, node);
                    }
                    bool connectionTested = TestNodeConnections(node, neighbor, secondNeighbor);
                    if (node.Activated && neighbor.Activated && secondNeighbor.Activated && null != secondEdge && connectionTested)
                    {
                        secondEdge.Bridged = true;
                        Edges.Remove(secondEdge);
                        Edges.Add(secondEdge);

                        if (null != edge)
                        {
                            edge.Bridged = true;
                            Edges.Remove(edge);
                            Edges.Add(edge);
                        }
                        GameState = GameState.WON;
                        publishGameState();
                    }
                }

                if (null != edge)
                {
                    Edges.Remove(edge);
                    Edges.Add(edge);
                }
            }
        }

        private Edge GetConnectingEdge(Node a, Node b)
        {
            for (int i = 0; i < NumberOfEdges; i++)
            {
                Vector2 from = EdgeConnections[i][0];
                Vector2 to = EdgeConnections[i][1];
                if (
                    from.Equals(a.Position) && to.Equals(b.Position)
                    || (to.Equals(a.Position) && from.Equals(b.Position))
                )
                {
                    return Edges
                        .First(e => e.From.Equals(from) && e.To.Equals(to));
                }
            }
            return null;
        }

        private bool TestNodeConnections(params Node[] nodes)
        {
            foreach(Node i in nodes)
            {
                foreach (Node j in nodes)
                {
                    Edge edge = GetConnectingEdge(i, j);
                    if (null != edge)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
