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
        bool isTrained;
        float trainingTime;
        int goldCarrying;
        Building targetBuilding;
        bool isActing;
        Agent agent;
        Graph<int>.Node<int> node;

        #endregion

        #region Properties

        public bool IsTrained
        {
            get { return isTrained; }
            set { isTrained = value; }
        }

        public int GoldCarrying
        {
            get { return goldCarrying; }
            set { goldCarrying = value; }
        }

        public bool IsActing
        {
            get { return isActing; }
            set { isActing = value; }
        }

        #endregion

        public Unit(Agent agent, UnitType type, Game game, string textureName, SpriteBatch spriteBatch, Vector2 position, Graph<int>.Node<int> node) : base(game, textureName, spriteBatch, position, node)
        {
            this.node = node;
            this.type = type;
            this.agent = agent;
            isActing = false;
            goldCarrying = 0;
            follow = new List<Vector2>();
            shouldMove = false;
            isTrained = false;

            if (type == UnitType.peon)
            {
                velocity = new Vector2(0.4f, 0.4f);
                trainingTime = 1000f;
            }
            else if (type == UnitType.soldier)
            {
                velocity = new Vector2(0.6f, 0.6f);
                trainingTime = 2000f;
            }
            color = Color.Red;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (isTrained == false)
            {
                trainingTime -= gameTime.ElapsedGameTime.Milliseconds;

                if (trainingTime <= 0)
                {
                    color = Color.White;
                    isTrained = true;
                }

            }
            

            if (shouldMove == true && follow != null && follow.Count > 0)
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

            else if (follow.Count == 0 && shouldMove == true && targetBuilding.Type == BuildingType.goldMine)
            {
                goldCarrying = 5;
                node = targetBuilding.Node;
                agent.GoToBase(this);
                //shouldMove = false;
            }

            else if (follow.Count == 0 && shouldMove == true && targetBuilding.Type == BuildingType.homeBase)
            {
                agent.Gold += goldCarrying;
                agent.goldAmount.Text = agent.Gold.ToString();
                node = targetBuilding.Node;
                shouldMove = false;
            }
          
        }

        public void Move(List<Vector2> follow, Building end)
        {
            targetBuilding = end; 
            this.follow = follow;
            shouldMove = true;
        }
    }
}

