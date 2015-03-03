namespace WordDestroyerGame
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UIElement
    {
        public UIElement(Point coordinatePoint)
        {
            this.CoordinatePoint = coordinatePoint;
        }

        public Point CoordinatePoint { get; set; }
        public string Text { get; set; }
        public char[][] DrawableObject { get; set; }

        public void IncreaseY()
        {
            Point currentPoint = this.CoordinatePoint;
            currentPoint.Y += 1;
            this.CoordinatePoint = currentPoint;
        }
    }
}
