using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS2.Core.Model;
using System.Numerics;

namespace SS2.Core
{
    public abstract class LogicController
    {
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

        private GameState _state { get; set; }

        public LogicController()
        {
            GenerateNodes();
        }

        public void Update()
        {

        }

        public abstract void OnNodeClicked(Node node);

        public abstract void GenerateNodes();

        public abstract IEnumerable<Node> GetNodeList();
    }
}
