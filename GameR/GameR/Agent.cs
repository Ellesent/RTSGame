using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameR
{
    class Agent
    {

        #region Fields

        List<Unit> units;
        int gold;
        Graph<int>.Node<int> baseNode;
        Graph<int>.Node<int> goldNode;
       static Random randomBase;
       //static Random randomBaseY;
        int x;
        int y;
        int goldX;
        int goldY;
        Graph<int> graph;
        Building homeBase;
        Building mine;

        int peonCost;
        int soldierCost;
        int barracksCost;
        int goldRefineryCost;

        #endregion

        #region Constructor
        public Agent(Graph<int> graph , Game game, SpriteBatch spriteBatch)
        {
            this.graph = graph;
            randomBase = new Random();
            units = new List<Unit>();
            gold = 100;

            do
            {
                x = randomBase.Next(0, 2);
                if (x == 1)
                {
                    x = graph.Nodes.GetLength(0) - 1;
                }

                y = randomBase.Next(0, 2);
                if (y == 1)
                {
                    y = graph.Nodes.GetLength(1) - 1;
                }
            }
            while (graph.Nodes[x,y].Cell.IsObstacle);

          

            baseNode = graph.Nodes[x, y];
            baseNode.Cell.GetColor = Color.AntiqueWhite;
            baseNode.Cell.IsObstacle = true; 

            homeBase = new Building(BuildingType.homeBase, game, "base", spriteBatch, baseNode.Cell.position + new Vector2(baseNode.Cell.Size / 2), baseNode);

            do
            {
                goldX = randomBase.Next(0, graph.Nodes.GetLength(0) - 1);
                goldY = randomBase.Next(0, graph.Nodes.GetLength(1) - 1);
            } while (graph.Nodes[goldX, goldY].Cell.IsObstacle);

            goldNode = graph.Nodes[goldX, goldY];
            goldNode.Cell.IsObstacle = true;
            goldNode.Cell.GetColor = Color.Beige;
            mine = new Building(BuildingType.goldMine, game, "GoldMine", spriteBatch, goldNode.Cell.position + new Vector2(goldNode.Cell.Size / 2), goldNode);

            Graph<int>.Node<int> peonNode = baseNode.Neighbors[0];
            Unit peon = new Unit(UnitType.peon, game, "Peon", spriteBatch, peonNode.Cell.position + new Vector2(peonNode.Cell.Size / 2), peonNode);

            GetPath(peon, mine);

        }

        public void GetPath(Unit unit, Building building)
        {
            unit.Move(graph.BreadthFirstSearch(unit.Node, building.Node));
        }
        #endregion
    }
}
