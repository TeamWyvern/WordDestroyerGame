namespace WordDestroyerGame
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SpaceshipObject : GameObjectBase, IDrawableObject
    {
        public SpaceshipObject(Point point)
        {
            //    /\
            //   (  )
            //   (  )
            //  /|/\|\
            // /_||||_\

            this.Element = new UIElement(point);
            this.Element.DrawableObject = new char[5][];
            this.Element.DrawableObject[0] = new char[] { ' ', ' ', ' ', '/', '\\', ' ', ' ', ' ' };  
            this.Element.DrawableObject[1] = new char[] { ' ', ' ', '(', ' ', ' ', ')', ' ', ' ' };
            this.Element.DrawableObject[2] = new char[] { ' ', ' ', '(', ' ', ' ', ')', ' ', ' ' };
            this.Element.DrawableObject[3] = new char[] { ' ', '/', '|', '/', '\\', '|', '\\', ' ' };
            this.Element.DrawableObject[4] = new char[] { '/', '_', '|', '|', '|', '|', '_', '\\' };

            this.Element.CoordinatePoint = point;
        }

        public void Draw()
        {
            Console.SetCursorPosition(this.Element.CoordinatePoint.X, this.Element.CoordinatePoint.Y);

            for (int i = 0; i < 5; i++)
            {
                foreach (var character in this.Element.DrawableObject[i])
                {
                    Console.Write(character);
                }

                this.Element.IncreaseY();
                Console.SetCursorPosition(this.Element.CoordinatePoint.X, this.Element.CoordinatePoint.Y);
            }
        }
    }
}
