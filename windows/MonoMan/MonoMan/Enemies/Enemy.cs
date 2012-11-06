using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoMan
{
    abstract class Enemy : GameObject
    {
        

        public abstract void LoadContent(ContentManager aContent);
        public abstract void Update(GameTime aGameTime);
        public abstract void Draw(SpriteBatch aSpriteBatch);
        public abstract void OnHit(int aDamage);
    }
}
