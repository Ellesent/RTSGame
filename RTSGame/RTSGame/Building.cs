using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTSGame
{
    class Building : Sprite
    {
        BuildingType type;

        public Building(BuildingType type, Game game, string textureName, SpriteBatch spriteBatch, Vector2 position, Graph<int>.Node<int> node) : base(game, textureName, spriteBatch, position, node)
        {
            game.Components.Add(this);
            this.type = type;

        }
    }
}
