using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ss2
{
    /// <summary>
    /// This is the data/model class.
    /// Contains all data for the controller classes.
    /// </summary>
    class GameState
    {
        private Node[,] nodes = new Node[3, 3];
        private List<Edge> edges;
        private int matrixSize;
        public static double skillChance = 0.6;
        private EventBus eventBus = EventBus.getEventBus();
        private Random rng = new Random();

        private int clickedNodes = 0;


        public GameState(int n)
        {
            this.edges = new List<Edge>();
            initialize(n);
            eventBus.subscribe(new ResetEvent().GetType(), reset);
        }

        /// <summary>
        /// Initialize the entire matrix with new nodes
        /// </summary>
        /// <param name="n">Size of the nodes matrix</param>
        private void initialize(int n) {
            nodes = new Node[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    nodes[i, j] = new Node(i, j);
                }
            }
            this.matrixSize = n;
        }

        /// <summary>
        /// Revert all set nodes and edges to begin a new game
        /// </summary>
        /// <param name="ev">the ResetEventObject</param>
        private void reset(EventObject ev) {
            foreach (Edge e in edges){
                e.resetStatus();
            }
            initialize(matrixSize);
        }

        /// <summary>
        /// Activate one node by row and column
        /// (rows and columns start at 0)
        /// </summary>
        /// <param name="row">row of the clicked node</param>
        /// <param name="column">column of the clicked node</param>
        public void setNode(int row, int column)
        {
            bool success = false;

            // Check if coordinates are out of bound
            if (row > matrixSize - 1 || column > matrixSize - 1)
            {
                return;
            }

            // Check if node is already activated
            if (nodes[row, column].getStatus())
            {
                return;
            }

            Node myNode = nodes[row, column];

            // Calculate the success of activating the node using its own success chance           
            // Set Node Failed!
            if (((double)rng.Next(0, 101) / 100.0) > myNode.getChance())
            {
                Console.WriteLine("---> FAILED");
                success = false;
            }
            // Set Node Succeeded!
            else {
                success = true;
                myNode.setStatus();
                List<Node> list = FindNeighboursByNode(row, column);
                
                // Iterate over the entire edge list to find edges 
                // to other activated nodes if so activate the edge
                foreach (Node node in list)
                {
                    if (node.getStatus())
                    {
                        Edge e = findEdgeByNodes(myNode, node);
                        e.setStatus();
                        eventBus.publish(new EdgeSetEvent(e.getNumber()));
                    }
                }
            }

            clickedNodes++;

            // Notify the controller of the outcome of this 'Set Node' method
            eventBus.publish(new NodeSetEvent(row, column, myNode.isIce(), success));

            // See if setting the node changed any game relevant requirements for winning
            checkForGameEndingState();
        }

        /// <summary>
        /// Check if the game winning requirements are met
        /// (3 activated nodes connected directly by edges is a win)
        /// </summary>
        public void checkForGameEndingState() {
            for (int i = 0; i < matrixSize; i++) {
                List<Node> row = new List<Node>();
                for (int j = 0; j < matrixSize; j++) {
                    Node n = nodes[i, j];
                    if (n.getStatus()){
                            if ((j < matrixSize - 1 && j > 0) && nodes[i, j - 1].getStatus() && nodes[i, j + 1].getStatus())
                            {
                                if (findEdgeByNodes(nodes[i, j - 1], nodes[i, j]) != null && findEdgeByNodes(nodes[i, j + 1], nodes[i, j]) != null)
                                {
                                    eventBus.publish(new WinEvent(true));
                                    return;
                                }
                            }

                        if ((i < matrixSize - 1 && i > 0) && nodes[i - 1, j].getStatus() && nodes[i + 1, j].getStatus())
                        {
                            if (findEdgeByNodes(nodes[i - 1, j], nodes[i, j]) != null && findEdgeByNodes(nodes[i + 1, j], nodes[i, j]) != null)
                            {
                                eventBus.publish(new WinEvent(true));
                                return;
                            }
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            String res = "NODES:";

            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    res += nodes[i, j] + ".";
                }
            }

            res += "\n+EDGES:";

            foreach (Edge e in edges)
            {
                res += e + ".";
            }

            return res;
        }

        /// <summary>
        /// Add one new edge between two nodes (addressed by row, column)
        /// (rows and columns start at 0)
        /// </summary>
        /// <param name="number">Identifier for the new edge</param>
        /// <param name="rowA">row of the first node</param>
        /// <param name="columnA">column of the first node</param>
        /// <param name="rowB">row of the second node</param>
        /// <param name="columnB">column of the second node</param>
        public void addNewEdge(int number, int rowA, int columnA, int rowB, int columnB) {
            edges.Add(new Edge(number, nodes[rowA, columnA], nodes[rowB, columnB]));
        }

        /// <summary>
        /// Find all neighbours of one node
        /// To be able to check for edges that need to be activated
        /// </summary>
        /// <param name="row">row of the relevent node</param>
        /// <param name="column">column of the relevent node</param>
        /// <returns>
        /// A list of all through edge connected neighbours of the given node
        /// </returns>
        private List<Node> FindNeighboursByNode(int row, int column)
        {
            List<Node> possibleNeighbours = new List<Node>();
            List<Node> foundNeighbours = new List<Node>();

            // check if upper neighbour exists
            if (row - 1 >= 0 && nodes[row - 1, column] != null)
            {
                possibleNeighbours.Add(nodes[row - 1, column]);
            }

            // check if lower neighbour exists
            if (row + 1 < matrixSize && nodes[row + 1, column] != null)
            {
                possibleNeighbours.Add(nodes[row + 1, column]);
            }

            // check if left neighbour exists
            if (column - 1 >= 0 && nodes[row, column - 1] != null)
            {
                possibleNeighbours.Add(nodes[row, column - 1]);
            }

            // check if right neighbour exists
            if (column + 1 < matrixSize && nodes[row, column + 1] != null)
            {
                possibleNeighbours.Add(nodes[row, column + 1]);
            }

            // review found neighbours for existing edge connections
            foreach (Node node in possibleNeighbours)
            {
                if (findEdgeByNodes(nodes[row, column], node) != null)
                {
                    foundNeighbours.Add(node);
                }
            }

            return foundNeighbours;
        }

        /// <summary>
        /// Find an edge that connects 2 specific nodes
        /// To be able to decide if 2 nodes are connected
        /// </summary>
        /// <param name="n1">first node</param>
        /// <param name="n2">second node</param>
        /// <returns>
        /// An Edge between the 2 given nodes (if it exists)
        /// </returns>
        private Edge findEdgeByNodes(Node n1, Node n2)
        {
            // Iterate over all edges
            foreach (Edge e in edges)
            {
                // compare the nodes of the edge e and compare it with the given nodes
                if ((e.getNode1().Equals(n1) && e.getNode2().Equals(n2)) || (e.getNode1().Equals(n2) && e.getNode2().Equals(n1)))
                {
                    return e;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Class representing a node
    /// Contains its chance to be activated, if its a trap (ice) and its activation status
    /// </summary>
    public class Node
    {
        private bool status;
        private bool ice;
        private double chance;

        private int row;
        private int column;

        public Node(int row, int column)
        {
            status = false;
            ice = false;
            this.chance = GameState.skillChance;
            this.chance = chance;
            this.row = row;
            this.column = column;
        }

        public bool getStatus()
        {
            return status;
        }

        public void setStatus()
        {
            status = true;
        }

        public bool isIce()
        {
            return ice;
        }

        public void setIce()
        {
            ice = true;
        }

        public int getColumn()
        {
            return column;
        }

        public int getRow()
        {
            return row;
        }

        public double getChance()
        {
            return this.chance;
        }

        public override string ToString()
        {
            return "N[y=" + row + ", x=" + column + ", active=" + status + "]";
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;

            if (!obj.GetType().Equals(this.GetType())) return false;

            Node other = (Node)obj;
            if (other.column == this.column && other.row == this.row)
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Class representing an edge
    /// Edges hold information over the 2 nodes its connecting, activation status and identifing number
    /// </summary>
    public class Edge
    {
        private bool status;
        private int number;
        private Node n1;
        private Node n2;

        public Edge(int number, Node n1, Node n2)
        {
            this.number = number;
            status = false;
            this.n1 = n1;
            this.n2 = n2;
        }

        public Node getNode1()
        {
            return n1;
        }

        public Node getNode2()
        {
            return n2;
        }

        public bool getStatus()
        {
            return status;
        }

        public void setStatus()
        {
            this.status = true;
        }

        public void resetStatus()
        {
            this.status = false;
        }

        public int getNumber()
        {
            return this.number;
        }

        public override string ToString()
        {
            return "E[n1=" + n1.getStatus() + ", n2=" + n2.getStatus() + ", active=" + status + "]";
        }
    }

    }
