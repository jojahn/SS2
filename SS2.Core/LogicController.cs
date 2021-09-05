using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS2.Core.Model;
using SS2.Core.Model.State;
using System.Numerics;
using SS2.Core.Logic;
using SS2.Core.Resources;

namespace SS2.Core
{
    public abstract class LogicController
    {
        protected static readonly int NumberOfNodes = 14;

        protected List<string> Responses { get; set; }

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

        protected GameState GameState { get; set; }
        protected PlayerState PlayerState { get; set; }
        protected DeviceState DeviceState { get; set; }

        public LogicController()
        {
            GameState = GameState.IDLE;
            PlayerState = new PlayerState(2, 1, 1000);
            DeviceState = new DeviceState(0.75, 1, 5);
            GenerateNodes();
            MakeInitialLines();
        }

        public void Reset()
        {
            GameState = GameState.IDLE;
            ResetNodes();
            MakeInitialLines();
        }

        public void Start()
        {
            GameState = GameState.STARTED;
        }

        public List<string> GetResponses()
        {
            return Responses;
        }

        public abstract void GenerateNodes();

        public abstract void ResetNodes();

        public abstract IEnumerable<Node> GetNodeList();

        public abstract GameState CheckState();

        public void OnNodeClicked(Node node)
        {
            bool success = TryNode(node);
            Responses.Add(NodeResponses.GetRandomResponse(success));
        }

        public abstract bool TryNode(Node node);

        private void MakeInitialLines()
        {
            double hackSkillDeduction = (-1) * Math.Round(Difficulty.ScaleHackSkill(DeviceState, PlayerState) * 100);
            double CYBStatDeduction = (-1) * Math.Round(Difficulty.ScaleCYBStat(DeviceState, PlayerState) * 100);
            double finalDifficulty = Math.Round(Difficulty.GetFinalDifficulty(DeviceState, PlayerState) * 100);
            string nodeOrNodes = DeviceState.ICENodes == 1 ? "node" : "nodes";
            Responses = new List<string>(new string[] {
                $"Initial Difficulty: {DeviceState.InitialDifficulty * 100}%.",
                $"Hack Skill {PlayerState.HackSkill}: {hackSkillDeduction}%.",
                $"CYB stat {PlayerState.CYBStat}: {CYBStatDeduction}%.",
                $"Final Difficulty: {finalDifficulty}%",
                $"{DeviceState.ICENodes} ICE {nodeOrNodes}."
            });
        }

        public abstract void SubscribeToNodeList(EventHandler eventHandler);
        public abstract void SubscribeToResponses(EventHandler eventHandler);
        public abstract void SubscribeToGameState(EventHandler eventHandler);

    }
}
