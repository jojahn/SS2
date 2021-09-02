using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.Model
{
    public class Node
    {
        public Guid Id { get; }

        public Vector2 Position { get; }

        public bool IsICE { get; }


        public double Chance { get; }

        public Node(long x, long y, bool isICE, double chance)
        {
            Id = Guid.NewGuid();
            Position = new Vector2(x, y);
            Chance = chance;
            IsICE = isICE;
        }

        public Node(Vector2 position, bool isICE, double chance)
        {
            Id = Guid.NewGuid();
            Position = position;
            Chance = chance;
            IsICE = isICE;
        }
    }
}
