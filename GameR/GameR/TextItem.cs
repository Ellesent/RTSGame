using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameR
{
    class TextItem : DrawableGameComponent
    {
        #region Fields

        Vector2 position;
        string text;
        SpriteBatch spriteBatch;
        SpriteFont font;

        #endregion

        #region Properties

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        #endregion

        #region Constructor

        public TextItem(Game game, SpriteBatch spriteBatch, Vector2 position, string text) : base(game)
        {
            game.Components.Add(this);
            this.position = position;
            this.text = text;
            this.spriteBatch = spriteBatch;
            font = game.Content.Load<SpriteFont>("Arial");
        }

        #endregion

        #region Public Methods

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            base.Draw(gameTime);
            spriteBatch.DrawString(font, text, position, Color.Black);
            spriteBatch.End();
        }

        #endregion
    }
}
