namespace WordDestroyerGame
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WordObject : GameObjectBase
    {
        public WordObject(string text, Point coordinatePoint)
        {
            this.Element.Text = text;
            this.Element.CoordinatePoint = coordinatePoint;
        }

        public BonusObject Bonus { get; set; }
        public bool IsDestroyed { get; set; }
    }
}
