using SS2.Core.Model;
using System;

namespace SS2.Core.Logic {
    public class Chance {

        private Random _random;

        public Chance(int seed)
        {
            _random = new Random(seed);
        }

        public bool TryNode(Node node, Difficulty difficulty) {
            if (difficulty.Final == 0)
            {
                return true;
            } else if (difficulty.Final >= 1.0)
            {
                return false;
            } else
            {
                return _random.NextDouble() <= node.Chance;
            }
        }

        public bool SetNodeAsICE(Node node, Difficulty difficulty)
        {
            return false;
        }

        public double GetNodeDifficulty(Node node, Difficulty difficulty)
        {
            return difficulty.Final;
        }
    }
}