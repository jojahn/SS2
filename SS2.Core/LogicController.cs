using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS2.Core.Model;
using System.Numerics;
using SS2.Core.Logic;
using SS2.Core.Resources;
using SS2.Core.OS;
using System.Collections.ObjectModel;
using Serilog;

namespace SS2.Core
{
    public abstract class LogicController : ILogicController
    {
        public event EventHandler<GameState> GameStateChanged;

        protected StatePersistance persistance;
        protected NodeResponses nodeResponses;
        protected Difficulty Difficulty;

        protected static readonly int NumberOfNodes = 14;

        protected static readonly Vector2[] NodePositions = new Vector2[14] {
            new Vector2(2, 0),
            new Vector2(3, 0),
            new Vector2(4, 0),
            
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(2, 1),
            new Vector2(4, 1),

            new Vector2(0, 2),
            new Vector2(2, 2),
            new Vector2(3, 2),
            new Vector2(4, 2),

            new Vector2(0, 3),
            new Vector2(1, 3),
            new Vector2(2, 3),
        };

        protected static readonly int NumberOfEdges = 15;

        protected static readonly Vector2[][] EdgeConnections = new Vector2[15][] {
            // Horizontal
            new Vector2[2] { new Vector2(2, 0), new Vector2(3, 0) },
            new Vector2[2] { new Vector2(3, 0), new Vector2(4, 0) },
            new Vector2[2] { new Vector2(0, 1), new Vector2(1, 1) },
            new Vector2[2] { new Vector2(1, 1), new Vector2(2, 1) },
            new Vector2[2] { new Vector2(2, 2), new Vector2(3, 2) },
            new Vector2[2] { new Vector2(3, 2), new Vector2(4, 2) },
            new Vector2[2] { new Vector2(0, 3), new Vector2(1, 3) },
            new Vector2[2] { new Vector2(1, 3), new Vector2(2, 3) },

            // Vertical
            new Vector2[2] { new Vector2(2, 0), new Vector2(2, 1) },
            new Vector2[2] { new Vector2(4, 0), new Vector2(4, 1) },

            new Vector2[2] { new Vector2(0, 1), new Vector2(0, 2) },
            new Vector2[2] { new Vector2(2, 1), new Vector2(2, 2) },
            new Vector2[2] { new Vector2(4, 1), new Vector2(4, 2) },

            new Vector2[2] { new Vector2(0, 2), new Vector2(0, 3) },
            new Vector2[2] { new Vector2(2, 2), new Vector2(2, 3) },
        };

        public GameState GameState { get; protected set; }
        protected PlayerState PlayerState { get; set; }
        protected DeviceState DeviceState { get; set; }

        public ObservableCollection<Node> Nodes { get; protected set; }
        public ObservableCollection<Edge> Edges { get; protected set; }
        public ObservableCollection<string> Responses { get; protected set; }

        public LogicController()
        {
            LoadSavedGame();
        }

        public virtual void Reset()
        {
            GameState = GameState.STARTED;
            ResetNodes();
            ResetEdges();
            Responses.Clear();
            foreach(string res in nodeResponses.GetInitialResponses(Difficulty, DeviceState, PlayerState))
            {
                Responses.Add(res);
            }
            using (var logger = Logging.Logger())
            {
                logger.Information("GAME: Game restarted!");
            }
        }

        public virtual void Start()
        {
            GameState = GameState.STARTED;
            PlayerState = new PlayerState(2, 1, 1000);
            DeviceState = new DeviceState(0.75, 1, 5);
            Difficulty = new Difficulty(DeviceState, PlayerState);
            Reset();
            using (var logger = Logging.Logger())
            {
                logger.Information("GAME: Game started!");
            }
        }

        // Nodes
        protected abstract void GenerateNodes();
        protected abstract void ResetNodes();
        public abstract IEnumerable<Node> GetNodeList();
        public abstract Node GetNodeById(Guid id);

        // GameState
        public abstract GameState CheckState();

        // Edges
        protected abstract void ResetEdges();
        protected abstract void GenerateEdges();
        public abstract IEnumerable<Edge> GetEdgeList();

        public void OnExit()
        {
            using (var logger = Logging.Logger())
            {
                logger.Information("Saving game...");
            }
            persistance.SaveGame(new SavedGame()
            {
                PlayerState = PlayerState,
                GameState = GameState,
                DeviceState = DeviceState,
                Edges = (List<Edge>)GetEdgeList(),
                Nodes = (List<Node>)GetNodeList()
            });
            using (var logger = Logging.Logger())
            {
                logger.Information("Game saved!");
            }
        }

        public virtual void OnNodeClicked(Node node)
        {
            if (!GameState.Equals(GameState.STARTED))
            {
                return;
            }
            Node foundNode = GetNodeById(node.Id);
            bool success = TryNode(foundNode);
            if (success)
            {
                foundNode.Activated = true;
                using (var logger = Logging.Logger())
                {
                    logger.Information("{node} activated!", node);
                }
            }
            else
            {
                foundNode.Failed = true;
                using (var logger = Logging.Logger())
                {
                    logger.Information("{node} failed!", node);
                }
            }
            if (!success && node.IsICE)
            {
                GameState = GameState.FAILED;
                using (var logger = Logging.Logger())
                {
                    logger.Information("GAME: Game lost with ICE {node}!", node);
                }
            }
            Responses.Add(nodeResponses.GetRandomResponse(success));
        }

        public abstract bool TryNode(Node node);

        public abstract void SubscribeToNodeList(EventHandler eventHandler);
        public abstract void SubscribeToEdgeList(EventHandler eventHandler);
        public abstract void SubscribeToResponses(EventHandler eventHandler);
        public abstract void SubscribeToGameState(EventHandler<GameState> eventHandler);

        private void LoadSavedGame()
        {
            using (var logger = Logging.Logger())
            {
                logger.Information("Loading saved game...");
            }

            persistance = new StatePersistance();
            SavedGame game;

            try
            {
                game = persistance.LoadGame();

            }
            catch (Exception ex)
            {
                using (var logger = Logging.Logger())
                {
                    logger.Error(ex, "Failed to load saved game!\nNew game will be created");
                }
                MakeNewGame();
                return;
            }

            GameState = game.GameState;
            PlayerState = game.PlayerState;
            DeviceState = game.DeviceState;
            Edges = new ObservableCollection<Edge>(game.Edges);
            Nodes = new ObservableCollection<Node>(game.Nodes);
            Difficulty = new Difficulty(DeviceState, PlayerState);
            using (var logger = Logging.Logger())
            {
                logger.Information("Saved game loaded!");
            }
        }

        private void MakeNewGame()
        {
            using (var logger = Logging.Logger())
            {
                logger.Information("Creating a new game...");
            }
            GameState = GameState.IDLE;
            PlayerState = new PlayerState(2, 1, 1000);
            DeviceState = new DeviceState(0.75, 1, 5);
            Difficulty = new Difficulty(DeviceState, PlayerState);

            GenerateNodes();
            GenerateEdges();

            nodeResponses = new NodeResponses(100);
            Responses = new ObservableCollection<string>(nodeResponses.GetInitialResponses(Difficulty, DeviceState, PlayerState));

            persistance = new StatePersistance();
            persistance.SaveGame(new SavedGame()
            {
                PlayerState = PlayerState,
                GameState = GameState,
                DeviceState = DeviceState,
                Edges = Edges.ToList(),
                Nodes = Nodes.ToList(),
                Responses = Responses.ToList()
            });
            using (var logger = Logging.Logger())
            {
                logger.Information("New game created!");
            }
        }
    }
}
