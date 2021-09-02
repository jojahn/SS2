using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ss2
{
    static class Program
    {
        public static GameState gs;

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void proc()
        {
            Controller controller = new Controller();
            EventBus eventBus = EventBus.getEventBus();
            eventBus.subscribe(new NodeClickEvent(-1, -1).GetType(), controller.eventMethod);
            gs = new GameState(3);
            gs.setNode(0, 1);
            Console.WriteLine(gs);



            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new ss2form());
            gs.setNode(10, 10);
        }


        public class Controller
        {

            public Controller()
            {
            }

            public void eventMethod(EventObject ev)
            {
                Console.WriteLine("//// " + ev.getMessage() + " ////");
                NodeClickEvent nEv = (NodeClickEvent)ev;
                Console.WriteLine("//// row=" + nEv.getRow() + ", column=" + nEv.getColumn() + " ////");
                gs.setNode(nEv.getRow(), nEv.getColumn());
                Console.WriteLine(gs);
            }
        }
    }
}
