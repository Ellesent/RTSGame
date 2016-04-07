using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameR
{
    class Unit : Sprite
    {
        #region Fields

        UnitType type;


        #endregion
        public Unit(UnitType type, Game game, string textureName, SpriteBatch spriteBatch, Vector2 position, Graph<int>.Node<int> node) : base(game, textureName, spriteBatch, position, node)
        {
            this.type = type;
        }
    }
}

