using SS2.Core.Logic;
using SS2.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SS2.Core.Test
{
    public class NodeResponsesTest
    {
        static DeviceState DeviceState = new DeviceState(0.1, 0, 0);
        static PlayerState PlayerState = new PlayerState();
        static Difficulty Difficulty = new Difficulty(DeviceState, PlayerState);

        [Fact]
        public void TestGetInitialResponsesReturnsListOfStrings()
        {
            NodeResponses responses = new NodeResponses(100);
            List<string> initialResponses = responses.GetInitialResponses(Difficulty, DeviceState, PlayerState);
            Assert.NotEmpty(initialResponses);
            Assert.IsType<List<string>>(initialResponses);
        }

        [Fact]
        public void TestGetInitialResponsesContainsInitialDifficulty()
        {
            NodeResponses responses = new NodeResponses(100);
            List<string> initialResponses = responses.GetInitialResponses(Difficulty, DeviceState, PlayerState);
            string initialDiffcultyString = ((int)Math.Round((Difficulty.Initial * 100))).ToString();
            Assert.Contains(initialDiffcultyString, initialResponses[0]);
        }


        [Fact]
        public void TestGetRandomResponseReturnsString()
        {
            NodeResponses responses = new NodeResponses(100);
            string res = responses.GetRandomResponse(true);
            Assert.NotEmpty(res);
            Assert.IsType<string>(res);
        }
    }
}
