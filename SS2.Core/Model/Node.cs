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

        public int X { get; }
        public string Y { get; }

        public int YA = 10;

        public bool IsICE { get; }


        public double Chance { get; }

        public Node(long x, long y, bool isICE, double chance)
        {
            Id = Guid.NewGuid();
            Position = new Vector2(x, y);
            X = (int)x;
            Y = y.ToString();
            Chance = chance;
            IsICE = isICE;
        }

        public Node(Vector2 position, bool isICE, double chance)
        {
            Id = Guid.NewGuid();
            Position = position;
            X = (int)position.X;
            Y = position.Y.ToString();
            Chance = chance;
            IsICE = isICE;
        }
    }
}
