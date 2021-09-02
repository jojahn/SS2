using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS2.Core.Model;

namespace SS2.Core
{
    public class BasicLogicController : LogicController
    {
        private List<Node> _nodes;

        public BasicLogicController() : base()
        {
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

        public override void OnNodeClicked(Node node)
        {
            throw new NotImplementedException();
        }
    }
}
