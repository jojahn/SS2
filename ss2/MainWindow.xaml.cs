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
        private const int edgeCount = 15;

        private static Color cyan = (Color)ColorConverter.ConvertFromString("#068C68");
        private static SolidColorBrush cyanBrush = new SolidColorBrush(cyan);
        private static Color lightCyan = (Color)ColorConverter.ConvertFromString("#0DFAC1");
        private static SolidColorBrush lightCyanBrush = new SolidColorBrush(lightCyan);
        private static Color orange = (Color)ColorConverter.ConvertFromString("#E9840E");
        private static SolidColorBrush orangeBrush = new SolidColorBrush(orange);
        private static Color darkCyan = (Color)ColorConverter.ConvertFromString("#00412F");
        private static SolidColorBrush darkCyanBrush = new SolidColorBrush(darkCyan);

        List<Rectangle> rectangles;
        Button[,] buttons;

        private static GameState gs;

        EventBus eventBus = EventBus.getEventBus();

        public MainWindow()
        {
            EventBus eventBus = EventBus.getEventBus();
            eventBus.subscribe(new EdgeSetEvent(-1).GetType(), setEdge);
            eventBus.subscribe(new NodeClickEvent(-1, -1).GetType(), eventMethod);
            eventBus.subscribe(new NodeSetEvent(-1, -1, false, false).GetType(), setNode);
            eventBus.subscribe(new WinEvent(true).GetType(), winGame);

            rectangles = new List<Rectangle>();
            buttons = new Button[matrixSize, matrixSize];

            gs = new GameState(matrixSize);
            gs.addNewEdge(0, 0, 2, 0, 3);
            gs.addNewEdge(1, 0, 3, 0, 4);
            gs.addNewEdge(2, 0, 2, 1, 2);
            gs.addNewEdge(3, 0, 4, 1, 4);
            gs.addNewEdge(4, 1, 0, 1, 1);
            gs.addNewEdge(5, 1, 1, 1, 2);
            gs.addNewEdge(6, 1, 0, 2, 0);
            gs.addNewEdge(7, 1, 2, 2, 2);
            gs.addNewEdge(8, 1, 4, 2, 4);
            gs.addNewEdge(9, 2, 2, 2, 3);
            gs.addNewEdge(10, 2, 3, 2, 4);
            gs.addNewEdge(11, 2, 0, 3, 0);
            gs.addNewEdge(12, 2, 2, 3, 2);
            gs.addNewEdge(13, 3, 0, 3, 1);
            gs.addNewEdge(14, 3, 1, 3, 2);
            Console.WriteLine(gs);

            InitializeComponent();
            initialize();
        }

        private void initialize()
        {
            for (int i = 0; i < edgeCount; i++)
            {
                Rectangle rect = (Rectangle)this.FindName("edge" + i);

                if (rect != null)
                {
                    rectangles.Add(rect);
                }

            }

            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    Button btn = (Button)this.FindName("button" + i + j);
                    if (btn != null)
                    {
                        buttons[i, j] = btn;
                        btn.IsEnabled = false;
                    }
                }
            }
        }

        private void setButtons(bool value)
        {
             for (int i = 0; i<buttons.GetLength(0); i++)
            {
                for (int j = 0; j<buttons.GetLength(1); j++)
                {
                    Button btn = buttons[i, j];
                    if (btn != null)
                        btn.IsEnabled = value;
                }
}
        }

        private void resetClick(object sender, RoutedEventArgs e) {
            Console.WriteLine("---> reset clicked");

            TextBox field = (TextBox)this.FindName("winReport");
            field.Visibility = Visibility.Hidden;
            field = (TextBox)this.FindName("loseReport");
            field.Visibility = Visibility.Hidden;

            foreach (Rectangle rect in rectangles)
            {
                rect.Fill = cyanBrush;
            }

            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    Button btn = buttons[i, j];
                    if (btn != null)
                        btn.Background = cyanBrush;
                }
            }
            eventBus.publish(new ResetEvent());
        }

        private void nodeClick(object sender, RoutedEventArgs e) {
            Button btn = sender as Button;
            String str = btn.Name.Replace("button", "");

            int row = Int32.Parse(str.ToCharArray()[0].ToString());
            int column = Int32.Parse(str.ToCharArray()[1].ToString());

            eventBus.publish(new NodeClickEvent(row, column));
        }

        private void setNode(EventObject ev) {
            NodeSetEvent nEv = (NodeSetEvent)ev;
            Button btn = (Button)this.FindName("button" + nEv.getRow() + nEv.getColumn());
            if (nEv.isSuccess())
            {
                btn.Background = lightCyanBrush;
            }
            else {
                btn.Background = darkCyanBrush;
                if (nEv.isIce()) {
                    failGame();
                }
            }
        }

        private void setEdge(EventObject ev) {
            Console.WriteLine("//// setEdge: "+ev+" ////");
            EdgeSetEvent eEv = (EdgeSetEvent)ev;
            Rectangle rect = (Rectangle)this.FindName("edge"+eEv.getNumber());
            rect.Fill = orangeBrush;
        }

        public void winGame(EventObject ev) {
            WinEvent wEv = ev as WinEvent;
            if (wEv.isWin())
            {
                TextBox field = (TextBox)this.FindName("winReport");
                field.Visibility = Visibility.Visible;
                Console.WriteLine(":::::::::::: WIN :::::::::::::");
            }
            else
            {
                TextBox field = (TextBox)this.FindName("loseReport");
                field.Visibility = Visibility.Visible;
                Console.WriteLine(":::::::::::: LOST :::::::::::::");
            }
        }

        public void failGame() {
            Console.WriteLine(":::::::::::: LOST :::::::::::::");
        }

        private void LeftMouseDown_Event(object sender, EventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void startHacking(object sender, EventArgs e)
        {
            TextBox rect = (TextBox)this.FindName("statusReport");
            Button start = (Button)this.FindName("startButton");
            Button reset = (Button)this.FindName("resetButton");

            rect.Visibility = Visibility.Collapsed;
            start.Visibility = Visibility.Collapsed;
            reset.Visibility = Visibility.Visible;
            reset.IsEnabled = true;

            setButtons(true);
        }

        private void exit(object sender, EventArgs e) {
            Close();
        }

        public void eventMethod(EventObject ev)
        {
            NodeClickEvent nEv = (NodeClickEvent)ev;
            Console.WriteLine("//// row=" + nEv.getRow() + ", column=" + nEv.getColumn() + " ////");
            gs.setNode(nEv.getRow(), nEv.getColumn());
            Console.WriteLine(gs);
        }


        private void dragWindowEvent(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }


}
