using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace RTSGame
{
    class Agent
    {

        #region Fields

        List<Unit> units;
        int gold;
        Graph<int>.Node<int> baseNode;
        Random randomBaseX;
        Random randomBaseY;
        int x;
        int y;
        Graph<int>.Node<int>[,] nodes;
        Building homeBase;

        #endregion

        #region Constructor
        public Agent (Graph<int>.Node<int>[,] nodes, Game game, SpriteBatch spriteBatch )
        {
            randomBaseX = new Random();
            randomBaseY = new Random();
            units = new List<Unit>();
            gold = 100;
            x = randomBaseX.Next(0, 2);
            if (x == 1)
            {
                x = nodes.GetLength(0) - 1;
            }

            y = randomBaseY.Next(0, 2);
            if (y == 1)
            {
                x = nodes.GetLength(1) - 1; 
            }

            baseNode = nodes[x, y];

            homeBase = new Building(BuildingType.homeBase, game, "base", spriteBatch, baseNode.Cell.position, baseNode);

            
        }
        #endregion
    }
}
