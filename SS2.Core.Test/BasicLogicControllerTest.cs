using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SS2.Core;
using SS2.Core.Model;

namespace SS2.Core.Test
{
    public class BasicLogicControllerTest
    {
        [Fact]
        public void TestStartUpdatesGameState()
        {
            ILogicController controller = new BasicLogicController();
            Assert.Equal(GameState.IDLE, controller.GameState);
            controller.Start();
            Assert.Equal(GameState.STARTED, controller.GameState);
        }

        [Fact]
        public void TestGameStateChangesCreateEvents()
        {
            ILogicController controller = new BasicLogicController();
            // Assert.Raises<GameState>(handler => controller.GameStateChanged += handler, handler => controller.GameStateChanged -= handler, null);
        }

        [Fact]
        public void TestResetUpdatesGameState()
        {
            ILogicController controller = new BasicLogicController();
            controller.Start();
            Assert.Equal(GameState.STARTED, controller.GameState);
            controller.Reset();
            Assert.Equal(GameState.STARTED, controller.GameState);
        }
    }
}
