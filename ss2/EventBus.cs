using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ss2
{
    class EventBus
    {
        private static EventBus eventBus;
        private Dictionary<Type, List<onEvent>> subscribers;

        private EventBus()
        {
            subscribers = new Dictionary<Type, List<onEvent>>();
        }

        public static EventBus getEventBus()
        {
            if (eventBus == null)
            {
                eventBus = new EventBus();
            }
            return eventBus;
        }

        public void subscribe(Type type, onEvent listener)
        {
            if (!subscribers.ContainsKey(type))
            {
                subscribers[type] = new List<onEvent>();
            }
            List<onEvent> list = subscribers[type];
            list.Add(listener);
            //Console.WriteLine("---> subscribed "+subscribers.ToString());
        }

        public void publish(EventObject ev)
        {
            Type type = ev.GetType();
            if (subscribers.ContainsKey(type))
            {
                List<onEvent> list = subscribers[type];

                for (int i = 0; i < list.Count() && i < 10; i++)
                {
                    list[i](ev);
                    Console.WriteLine("---> published i ");
                }
            }
            Console.WriteLine("---> published "+ev.GetType());
        }
    }

    public delegate void onEvent(EventObject ev);


    public interface IEventBusListener
    {
        void onEvent(EventObject ev);
    }

    public class EventObject
    {
        Object message;

        public EventObject(Object message)
        {
            this.message = message;
        }

        public Object getMessage()
        {
            return this.message;
        }
    }

    public class NodeClickEvent : EventObject
    {
        int row;
        int column;

        public NodeClickEvent(int row, int column) : base("node "+row + ", " + column)
        {
            this.row = row;
            this.column = column;
        }

        public int getRow()
        {
            return this.row;
        }

        public int getColumn()
        {
            return this.column;
        }
    }

    public class EdgeSetEvent : EventObject {
        int number;

        public EdgeSetEvent(int number) : base("edge "+number)
        {
            this.number = number;
        }

        public int getNumber()
        {
            return this.number;
        }
    }

    public class ResetEvent : EventObject
    {
        public ResetEvent() : base("reset")
        {
        }
    }
}
