using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameR
{
    /// <summary>
    /// Graph class that provides the back end of the grid
    /// </summary>
    /// <typeparam name="T">Graph can be any type</typeparam>
    class Graph<T> : GameComponent
    {
        #region Fields

        Node<T>[,] nodes;       //list of nodes
        Node<T> startNode;      //Start node for search
        Node<T> endNode;        //end node for search
        Queue<Node<T>> toVisit; //Queue for breadth first search
        bool GO;                //used to initiate search
        //Random randomI;         //get a random node in the array's X position
       // Random randomJ;         //get a random node in the array's Y position
        //int arrayX;             //hold random array X number
        //int arrayY;             //hold a random array Y number
        bool hasEndNode;
        KeyboardState oldState; //hold keyboard's old state
        KeyboardState newState; //hold keyboard's new state

        //public static bool ready; 

        public List<Vector2> follow;

        #endregion


        #region Constructor

        /// <summary>
        /// Graph Constructor
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="cells">The array of grid cells</param>
        public Graph(Game game, GridCell[,] cells) : base(game)
        {

            //add this to the component list
            game.Components.Add(this);

            hasEndNode = false;

            follow = new List<Vector2>();

            //initialize random variables
            //randomI = new Random();
           // randomJ = new Random();

            //set search boolean to false and set the timer to 200 milliseconds
            //GO = false;
            // timer = 200;



            //Initialiize nodes array to the size of the grid cells array
            nodes = new Node<T>[cells.GetLength(0), cells.GetLength(1)];

            //Have each node hold its alllocated grid cell
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    nodes[i, j] = new Node<T>(cells[i, j]);
                }
            }

            //Add an edge between nodes based on the nodes location
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                //add edges based on x location
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    if (i == 0)
                    {

                        nodes[i, j].AddEdge(nodes[i + 1, j]);
                    }
                    else if (i == nodes.GetLength(0) - 1)
                    {
                        nodes[i, j].AddEdge(nodes[i - 1, j]);
                    }

                    else
                    {
                        nodes[i, j].AddEdge(nodes[i + 1, j]);
                        nodes[i, j].AddEdge(nodes[i - 1, j]);
                    }

                    //add edges based on y location
                    if (j == 0)
                    {
                        nodes[i, j].AddEdge(nodes[i, j + 1]);
                    }
                    else if (j == nodes.GetLength(1) - 1)
                    {
                        nodes[i, j].AddEdge(nodes[i, j - 1]);
                    }

                    else
                    {
                        nodes[i, j].AddEdge(nodes[i, j + 1]);
                        nodes[i, j].AddEdge(nodes[i, j - 1]);
                    }
                }
            }

            //get the random x and y location of nodes array
            //arrayX = randomI.Next(nodes.GetLength(0) - 1);
           // arrayY = randomJ.Next(nodes.GetLength(1) - 1);

            //set start node to random location
           // startNode = nodes[arrayX, arrayY];

            ////get the random x and y location of nodes array
            //arrayX = randomI.Next(nodes.GetLength(0) - 1);
            //arrayY = randomJ.Next(nodes.GetLength(1) - 1);

            ////set end node to random location
            //endNode = nodes[arrayX, arrayY];

            //set the start node,s color to red and the end node's color to green
           // startNode.Cell.GetColor = Color.Red;

        }

        #endregion


        #region Properties

        public Node<T>[,] Nodes
        {
            get { return nodes; }
        }

        #endregion

        #region Public Methods

        public void Reset()
        {
            //get new locations for start and end node
            //arrayX = randomI.Next(nodes.GetLength(0) - 1);
            //arrayY = randomJ.Next(nodes.GetLength(1) - 1);
            //startNode = nodes[arrayX, arrayY];



            //arrayX = randomI.Next(nodes.GetLength(0) - 1);
            //arrayY = randomJ.Next(nodes.GetLength(1) - 1);
            //endNode = nodes[arrayX, arrayY];

            //reset the timer
            //timer = 100;

            //for each node, reset the color, distance, the wasVisited boolean, and restart the search
            foreach (Node<T> n in nodes)
            {
                n.Cell.GetColor = Color.CornflowerBlue;
                n.Distance = Int32.MaxValue;
                n.WasVisited = false;
                // startNode = endNode;
                startNode.Cell.GetColor = Color.Red;
                n.Cell.IsDestination = false;
                n.Cell.IsObstacle = false;
                //endNode.Cell.GetColor = Color.Green;

                if (toVisit != null)
                {
                    toVisit.Clear();
                }
                n.BackNode = null;
                // ready = false;


                // BreadthFirstSearch();

            }
            hasEndNode = false;
        }

        /// <summary>
        /// Update method for a graph
        /// </summary>
        /// <param name="gameTime"></param>
        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (hasEndNode == false)
            {
                foreach (Node<T> n in nodes)
                {
                    if (n.Cell.IsDestination == true)
                    {
                        endNode = n;
                        endNode.Cell.GetColor = Color.Green;
                        hasEndNode = true;
                    }

                }
            }

            if (hasEndNode == true)
            {

            }

            //get old state and new state of keyboard
            oldState = newState;
            newState = Keyboard.GetState();

            //If the space key is pressed, reset the graph
            if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
            {

                Reset();

            }

        }

        #endregion

        /// <summary>
        /// start the setup for the breadth first search
        /// </summary>
        #region BreadthFirstSearch
        public List<Vector2> BreadthFirstSearch(Node<T> startNode, Node<T> endNode)
        {
            this.startNode = startNode;
            this.endNode = endNode;
            //initiliaze queue and add the start node
            toVisit = new Queue<Node<T>>();
            toVisit.Enqueue(startNode);
            startNode.WasVisited = true;
            //follow.Enqueue(startNode.Cell.position);

            //for each node, set the distance to the max number
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    nodes[i, j].Distance = Int32.MaxValue;
                }
            }

            //if the tovisit queue is not empty, GO is tue, and the timer is over, start the search
            while (toVisit.Count > 0)
            {
                Console.WriteLine("Number of nodes = " + Node<T>.numberNodes);

                //dequeue the first node and set it to the current node
                Node<T> curr = toVisit.Dequeue();

                //change it's color to yellow
                //if (curr != startNode)
                //{
                //    curr.Cell.GetColor = Color.Yellow;
                //}

                //for each neighbor of the current cell..
                foreach (Node<T> n in curr.Neighbors)
                {
                    //if one of the neighbors is the end node, we found the end so get out of the loop
                    if (n == endNode)
                    {
                        n.BackNode = curr;
                        //follow.Enqueue(curr.Cell.position + new Vector2(curr.Cell.Size / 2, curr.Cell.Size / 2));

                        Node<T> t = curr;

                        follow.Add(endNode.Cell.position + new Vector2(endNode.Cell.Size / 2));

                        while (t != startNode)
                        {
                           // t.Cell.GetColor = Color.Blue;
                            follow.Add(t.Cell.position + new Vector2(t.Cell.Size / 2));
                            t = t.BackNode;
                        }
                        follow.Add(startNode.Cell.position + new Vector2(startNode.Cell.Size / 2));

                        return follow;
                    }

                    //if the node has already been visited, do nothing, we have found the shortest path to that node
                    if (n.WasVisited == true || n.Cell.IsObstacle)
                    {

                    }

                    //else...
                    else
                    {
                        //calculate distance of this neighbor to current node
                        float fullDistance = n.Distance - curr.Distance;

                        //set node to visited, and set the back node to the current node
                        n.WasVisited = true;
                        n.BackNode = curr;
                        //follow.Enqueue(curr.Cell.position + new Vector2(curr.Cell.Size / 2, curr.Cell.Size / 2));
                        n.Cell.startPoint = curr.Cell.position;
                        n.Cell.endPoint = n.Cell.position;

                        //put the node in the queue and set it's color to orange
                        toVisit.Enqueue(n);

                        //if (n != startNode)
                        //{
                        //    n.Cell.GetColor = Color.Orange;
                        //}
                    }
                }
            }

            return null;
        }
        #endregion
        #endregion

        /// <summary>
        /// A node class
        /// </summary>
        /// <typeparam name="T">Anything can bea node</typeparam>
        #region Node Class

        #region Fields
        public class Node<N>
        {
            List<Node<N>> neighbors;    //A list of the node's neighbors
            List<Edge<N>> edges;        //a list of the node's edges with other nodes
            GridCell currCell;          //the grid cell connected to this node
            bool wasVisited;            //was the node visited in the search already?
            Node<T> backNode;           //holds the node this node came from
            int distance;               //holds the distance of this node

            public static int numberNodes = 0;

            #endregion

            #region Properties

            /// <summary>
            /// Get and set the grid cell attached to this node
            /// </summary>
            public GridCell Cell
            {
                get { return currCell; }
                set { currCell = value; }
            }

            /// <summary>
            /// get and set the backnode of this node
            /// </summary>
            public Node<T> BackNode
            {
                get { return backNode; }
                set { backNode = value; }
            }

            //get and set whether the node has been visited
            public bool WasVisited
            {
                get { return wasVisited; }
                set { wasVisited = value; }
            }

            /// <summary>
            /// get and set the list of neigbors of this node
            /// </summary>
            public List<Node<N>> Neighbors
            {
                get { return neighbors; }
                set { neighbors = value; }
            }

            /// <summary>
            /// get and set the distance of this node
            /// </summary>
            public int Distance
            {
                get { return distance; }
                set { distance = value; }
            }

            //get the list of edges of the node
            public List<Edge<N>> Edges
            {
                get { return edges; }
            }

            #endregion

            #region Constructor
            /// <summary>
            /// The constructor for a node
            /// </summary>
            /// <param name="cell">the grid cell this node is attached to</param>
            public Node(GridCell cell)
            {
                //initialize the list of edges and neighbors, and store the grid cell
                edges = new List<Edge<N>>();
                neighbors = new List<Node<N>>();
                currCell = cell;
                backNode = null;

                numberNodes++;
            }

            #endregion

            #region Public Methods
            /// <summary>
            /// Add an edge between this node and another node
            /// </summary>
            /// <param name="node">the node to make an edge to</param>
            public void AddEdge(Node<N> node)
            {
                //store edge between this node and the other node
                Edge<N> edge = new Edge<N>(this, node);

                //add the edge to the edge list
                edges.Add(edge);

                //add the passed in node as a neighbor to this node
                neighbors.Add(node);
            }
        }

        #endregion
        #endregion

        #region Edge Class

        /// <summary>
        /// An Edge class
        /// </summary>
        /// <typeparam name="T">An edge can be any type</typeparam>
        public class Edge<E>
        {
            #region Fields

            Node<E> one;    //Node one of the edge
            Node<E> two;    //Node two of the edge

            #endregion

            #region Constructor
            /// <summary>
            /// A constructor for an edge
            /// </summary>
            /// <param name="one">node one of the edge connection</param>
            /// <param name="two">node two of the edge connection</param>
            public Edge(Node<E> one, Node<E> two)
            {
                ///store the two nodes of the dge
                this.one = one;
                this.two = two;
            }

            #endregion
        }

        #endregion
    }
}
