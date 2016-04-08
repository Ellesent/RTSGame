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
        bool shouldMove;
        List<Vector2> follow;
        Vector2 target;
        Vector2 direction;
        float distance;

        #endregion
        public Unit(UnitType type, Game game, string textureName, SpriteBatch spriteBatch, Vector2 position, Graph<int>.Node<int> node) : base(game, textureName, spriteBatch, position, node)
        {
            this.type = type;
            follow = new List<Vector2>();
            shouldMove = false;

            if (type == UnitType.peon)
            {
                velocity = new Vector2(0.4f, 0.4f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (shouldMove == true && follow.Count > 0)
            {
                target = follow[follow.Count - 1];
                if (target != position)
                {
                    velocity = target - position;
                    velocity.Normalize();
                    velocity = velocity * 0.1f;

                    direction = target - position;
                    distance = direction.Length();
                }
                if (distance < 5f)
                {
                    //index--;

                    follow.Remove(target);
                }

                position += velocity * gameTime.ElapsedGameTime.Milliseconds;
            }
          
        }

        public void Move(List<Vector2> follow)
        {
            this.follow = follow;
            shouldMove = true;
        }
    }
}

