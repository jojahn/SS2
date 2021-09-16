using SS2.Core.Model;
using System;

namespace SS2.Core.Logic {
    public class Chance {

        private Random _random = new Random();
        public bool TryNode(Node node, Difficulty difficulty) {
            return false;
        }

        public bool SetNodeAsICE(Node node, Difficulty difficulty)
        {
            return false;
        }
    }
}