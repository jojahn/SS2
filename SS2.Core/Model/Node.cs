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
        public Guid Id { get; set; }

        public Vector2 Position { get; set; }

        public bool IsICE { get; set; }

        public double Chance { get; set; }

        public bool Activated { get; set; } = false;

        public bool Failed { get; set; } = false;

        public Node() { }

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

        public void Reset()
        {
            Activated = false;
            Failed = false;
        }

        public override string ToString()
        {
            return $"Node(Id={Id}, Position=({Position.X + "," + Position.Y}), ICE={IsICE}, A={Activated}, F={Failed}, C={Chance})";
        }
    }
}
