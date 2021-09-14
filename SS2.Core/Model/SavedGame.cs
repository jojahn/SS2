using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.Model
{
    public class SavedGame
    {
        public List<Edge> Edges { get; set; }
        public List<Node> Nodes { get; set; }
        public GameState GameState { get; set; }

        public SavedGame(List<Edge> edges, List<Node> nodes, GameState gameState)
        {
            Edges = edges;
            Nodes = nodes;
            GameState = gameState;
        }
    }
}
