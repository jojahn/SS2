using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ss2
{
    class GameState
    {
        private Node[,] nodes = new Node[3, 3];
        private List<Edge> edges;
        private int matrixSize;
        EventBus eventBus = EventBus.getEventBus();

        private int clickedNodes = 0;


        public GameState(int n)
        {
            this.edges = new List<Edge>();
            initialize(n);
            eventBus.subscribe(new ResetEvent().GetType(), reset);
        }

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

        private void reset(EventObject ev) {
            Console.WriteLine("//// reset: " + ev + " ////");
            initialize(matrixSize);
        }

        public void setNode(int row, int column)
        {
            if (row > matrixSize - 1 || column > matrixSize - 1)
            {
                return;
            }

            if (nodes[row, column].getStatus())
            {
                return;
            }

            List<Node> list = FindNeighboursByNode(row, column);
            foreach (Node node in list)
            {
                if (node.getStatus())
                {
                    Edge e = findEdgeByNodes(nodes[row, column], node);
                    e.setStatus();
                    eventBus.publish(new EdgeSetEvent(e.getNumber()));
                }
            }

            nodes[row, column].setStatus();

            clickedNodes++;
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

        public void addNewEdge(int number, int rowA, int columnA, int rowB, int columnB) {
            edges.Add(new Edge(number, nodes[rowA, columnA], nodes[rowB, columnB]));
        }

        private List<Node> FindNeighboursByNode(int row, int column)
        {
            List<Node> possibleNeighbours = new List<Node>();
            List<Node> foundNeighbours = new List<Node>();


            if (row - 1 >= 0 && nodes[row - 1, column] != null)
            {
                possibleNeighbours.Add(nodes[row - 1, column]);
            }

            if (row + 1 < matrixSize && nodes[row + 1, column] != null)
            {
                possibleNeighbours.Add(nodes[row + 1, column]);
            }

            if (column - 1 >= 0 && nodes[row, column - 1] != null)
            {
                possibleNeighbours.Add(nodes[row, column - 1]);
            }

            if (column + 1 < matrixSize && nodes[row, column + 1] != null)
            {
                possibleNeighbours.Add(nodes[row, column + 1]);
            }

            foreach (Node node in possibleNeighbours)
            {
                if (findEdgeByNodes(nodes[row, column], node) != null)
                {
                    foundNeighbours.Add(node);
                }
            }

            return foundNeighbours;
        }

        private Edge findEdgeByNodes(Node n1, Node n2)
        {
            foreach (Edge e in edges)
            {
                if ((e.getNode1().Equals(n1) && e.getNode2().Equals(n2)) || (e.getNode1().Equals(n2) && e.getNode2().Equals(n1)))
                {
                    return e;
                }
            }
            return null;
        }
    }

    public class Node
    {
        private bool status;
        private bool trap;

        private int row;
        private int column;

        public Node(int row, int column)
        {
            status = false;
            trap = false;
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

        public bool isTrap()
        {
            return trap;
        }

        public void setTrap()
        {
            trap = true;
        }

        public int getColumn()
        {
            return column;
        }

        public int getRow()
        {
            return row;
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
