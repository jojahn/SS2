using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.Model
{
    public class Edge
    {
        public Guid Id { get; }
        public bool Activated { get; set; } = false;
        public bool Bridged { get; set; } = false;

        public Vector2 From { get; }
        public Vector2 To { get; }

        public bool IsHorizontal { get; }

        public Edge(Vector2 from, Vector2 to) {
            Id = Guid.NewGuid();
            From = from;
            To = to;
            IsHorizontal = from.Y == to.Y;
        }

        public Edge(int xFrom, int yFrom, int xTo, int yTo) {
            Id = Guid.NewGuid();
            From = new Vector2(xFrom, yFrom);
            To = new Vector2(xTo, yTo);
            IsHorizontal = yFrom == yTo;
        }

        public void Reset() {
            Activated = false;
            Bridged = false;
        }
    }
}
