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
       static Random randomBase;
       //static Random randomBaseY;
        int x;
        int y;
        Graph<int>.Node<int>[,] nodes;
        Building homeBase;

        #endregion

        #region Constructor
        public Agent(Graph<int>.Node<int>[,] nodes, Game game, SpriteBatch spriteBatch)
        {
            this.nodes = nodes;
            randomBase = new Random();
            units = new List<Unit>();
            gold = 100;
            x = randomBase.Next(0, 2);
            if (x == 1)
            {
                x = nodes.GetLength(0) - 1;
            }

            y = randomBase.Next(0, 2);
            if (y == 1)
            {
                y = nodes.GetLength(1) - 1;
            }

            baseNode = nodes[x, y];
            baseNode.Cell.GetColor = Color.AntiqueWhite;
            baseNode.Cell.IsObstacle = true; 

            homeBase = new Building(BuildingType.homeBase, game, "base", spriteBatch, baseNode.Cell.position + new Vector2(baseNode.Cell.Size / 2), baseNode);


        }
        #endregion
    }
}
