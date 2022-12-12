using SS2.Core.Logic;
using SS2.Core.Model;
using System;
using Xunit;

namespace SS2.Core.Test
{
    public class ChanceTest
    {
        Node Node = new Node();
        Difficulty Difficulty = new Difficulty(new DeviceState(0.5, 0, 0), new PlayerState());

        [Fact]
        public void TestSetNodeAsICEReturnsBool()
        {
            Chance chance = new Chance(100);   
            bool isICE = chance.SetNodeAsICE(Node, Difficulty);
            Assert.IsType<bool>(isICE);
        }

        [Fact]
        public void TestGetNodeDifficultyReturnsInitialDifficulty()
        {
            Chance chance = new Chance(100);
            double nodeDifficulty = chance.GetNodeDifficulty(Node, Difficulty);
            Assert.Equal(Difficulty.Initial, nodeDifficulty);
        }

        [Fact]
        public void TestTryNodeWithZeroDifficultyReturnsTrue()
        {
            Chance chance = new Chance(100);
            Difficulty difficulty = new Difficulty(new DeviceState(0, 0, 0), new PlayerState());
            bool success = chance.TryNode(Node, difficulty);
            Assert.True(success);
        }
    }
}
