namespace WordDestroyerGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SpaceshipObject : GameObjectBase, IDrawableObject
    {
        public SpaceshipObject(char[][] drawableObject)
        {
            this.Element.DrawableObject = drawableObject;
        }

        public void Draw(System.Drawing.Point point)
        {
            throw new NotImplementedException();
        }
    }
}
