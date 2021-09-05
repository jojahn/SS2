using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.Model
{
    public class ObservableNode : IObservable<Node>
    {
        public IDisposable Subscribe(IObserver<Node> observer)
        {
            throw new NotImplementedException();
        }
    }
}
