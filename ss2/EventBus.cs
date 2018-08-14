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
                }
            }
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

    public class WinEvent : EventObject
    {
        private bool win;

        public WinEvent(bool win) : base(win)
        {
            this.win = win;
        }

        public bool isWin()
        {
            return this.win;
        }
    }

    public class NodeSetEvent : NodeClickEvent
    {
        private bool ice;
        private bool success;

        public NodeSetEvent(int row, int column, bool ice, bool success) : base(row, column)
        {
            this.ice = ice;
            this.success = success;
        }

        public bool isIce() {
            return this.ice;
        }

        public bool isSuccess() {
            return this.success;
        }
    }
}
