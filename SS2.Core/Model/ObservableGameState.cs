using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.Model
{
    public class ObservableGameState : IObservable<GameState>
    {
        public IDisposable Subscribe(IObserver<GameState> observer)
        {
            throw new NotImplementedException();
        }
    }
}
