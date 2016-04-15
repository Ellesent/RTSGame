using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameR
{
    class Agent : GameComponent
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
        Game game;
        SpriteBatch spriteBatch;
        public TextItem goldAmount;

        int peonCost;
        int soldierCost;
        int barracksCost;
        int goldRefineryCost;

        #endregion


        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        #region Constructor
        public Agent(Graph<int> graph , Game game, SpriteBatch spriteBatch) : base (game)
        {
            game.Components.Add(this);
            this.graph = graph;
            this.game = game;
            this.spriteBatch = spriteBatch;  
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

            goldAmount = new TextItem(game, spriteBatch, baseNode.Neighbors[1].Cell.position, gold.ToString());
            TrainPeon();
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (units[0].IsTrained && !units[0].IsActing)
            {
                GoToMine(units[0]);
            }

        }

        public void GetPath(Unit unit, Building building)
        {
 
            unit.Move(graph.BreadthFirstSearch(unit.Node, building.Node), building);
           
        }

        public void TrainPeon()
        {
            Graph<int>.Node<int> peonNode = baseNode.Neighbors[0];
            Unit peon = new Unit(this, UnitType.peon, game, "Peon", spriteBatch, peonNode.Cell.position + new Vector2(peonNode.Cell.Size / 2), peonNode);
            gold -= 50;
            goldAmount.Text = gold.ToString();
            units.Add(peon);
        }

        public void GoToMine(Unit unit)
        {
            if (unit.IsTrained)
            {
                GetPath(unit, mine);
                unit.IsActing = true;
            }
        }

        public void GoToBase(Unit unit)
        {
            GetPath(unit, homeBase);
        }
        #endregion
    }
}
