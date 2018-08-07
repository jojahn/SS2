using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ss2
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int matrixSize = 5;
        private static Color secondaryColor = (Color)ColorConverter.ConvertFromString("#47877b");
        private static SolidColorBrush secondaryBrush = new SolidColorBrush(secondaryColor);
        private static Color primaryColor = (Color)ColorConverter.ConvertFromString("#42f4df");
        private static SolidColorBrush primaryBrush = new SolidColorBrush(primaryColor);
        private static Color tertierColor = (Color)ColorConverter.ConvertFromString("#ed9421");
        private static SolidColorBrush tertierBrush = new SolidColorBrush(tertierColor);

        List<Rectangle> rectangles;
        Button[,] buttons; 

        private static GameState gs;

        EventBus eventBus = EventBus.getEventBus();

        public MainWindow()
        {
            Controller controller = new Controller();
            EventBus eventBus = EventBus.getEventBus();
            eventBus.subscribe(new EdgeSetEvent(-1).GetType(), setEdge);
            eventBus.subscribe(new NodeClickEvent(-1, -1).GetType(), controller.eventMethod);

            rectangles = new List<Rectangle>();
            buttons = new Button[matrixSize, matrixSize];

            gs = new GameState(matrixSize);
            gs.addNewEdge(0, 0, 2, 0, 3);
            gs.addNewEdge(1, 0, 3, 0, 4);
            Console.WriteLine(gs);

            InitializeComponent();
            loadVars();

        }

        private void loadVars()
        {
            for (int i = 0; i < matrixSize; i++)
            {
                Rectangle rect = (Rectangle)this.FindName("edge" + i);

                Console.WriteLine("---> edge" + i);

                if (rect != null)
                {
                    rectangles.Add(rect);
                    Console.WriteLine("---> edge" + i + " FOUND !!!!!");
                }

            }
            Console.WriteLine("---> " + rectangles.Count() + " edges found");

            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    Button btn = (Button)this.FindName("button" + i + j);
                    Console.WriteLine("---> button"+i+j);
                    if (btn != null)
                    {
                        buttons[i, j] = btn;
                        Console.WriteLine("---> button" + i + j+" FOUND !!!!!");
                    }
                }
            }

        }

        private void resetClick(object sender, RoutedEventArgs e) {
            Console.WriteLine("---> reset clicked");
            Console.WriteLine("---> rects size = "+rectangles.Count());

            foreach (Rectangle rect in rectangles)
            {
                Console.WriteLine("rect list i");
                rect.Fill = secondaryBrush;
            }

            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    Button btn = buttons[i, j];
                    if (btn != null)
                        btn.Background = secondaryBrush;
                }
            }
            eventBus.publish(new ResetEvent());
        }

        private void button02Click(object sender, RoutedEventArgs e)
        {
            eventBus.publish(new NodeClickEvent(0, 2));
            Button btn = sender as Button;
            btn.Background = primaryBrush;
        }

        private void button03Click(object sender, RoutedEventArgs e)
        {
            eventBus.publish(new NodeClickEvent(0, 3));
            Button btn = sender as Button;
            btn.Background = primaryBrush;
        }

        private void button04Click(object sender, RoutedEventArgs e)
        {
            eventBus.publish(new NodeClickEvent(0, 4));
            Button btn = sender as Button;
            btn.Background = primaryBrush;
        }

        private void setEdge(EventObject ev) {
            Console.WriteLine("//// setEdge: "+ev+" ////");
            EdgeSetEvent eEv = (EdgeSetEvent)ev;
            Rectangle rect = (Rectangle)this.FindName("edge"+eEv.getNumber());
            rect.Fill = tertierBrush;
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
