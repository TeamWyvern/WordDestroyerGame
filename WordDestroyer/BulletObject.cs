namespace WordDestroyerGame
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BulletObject : GameObjectBase, IDrawableObject
    {
        public BulletObject(Point coordinatePoint, string text)
        {
            this.Element.CoordinatePoint = coordinatePoint;
            this.Element.Text = text;
        }

        public void Draw(Point point)
        {
            throw new NotImplementedException();
        }
    }
}
