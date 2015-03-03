namespace WordDestroyerGame
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class WordObject : GameObjectBase, IDrawableObject
    {
        public WordObject(string text, Point coordinatePoint)
        {
            this.Element = new UIElement(coordinatePoint);
            this.Element.Text = text;
            this.Element.CoordinatePoint = coordinatePoint;
        }

        public BonusObject Bonus { get; set; }
        public bool IsVisible { get; set; }
        public bool IsDestroyed { get; set; }
        public bool IsMissed { get; set; }
        public bool IsSelected { get; set; }

        public override string ToString()
        {
            return this.Element.Text;
        }

        public void Draw()
        {
            if (!this.IsDestroyed)
            {
                if (this.Element.CoordinatePoint.Y == Console.BufferHeight)
                {
                    this.IsVisible = false;
                    this.IsMissed = true;
                }
                else
                {
                    if (this.IsVisible && this.IsMissed == false && this.IsDestroyed == false)
                    {
                        Console.SetCursorPosition(this.Element.CoordinatePoint.X, this.Element.CoordinatePoint.Y - 1);
                        string empty = new string(' ', this.Element.Text.Length);
                        Console.WriteLine(empty);

                        if (this.Element.Text.Length == 1)
                        {
                            return;
                        }

                        Console.SetCursorPosition(this.Element.CoordinatePoint.X, this.Element.CoordinatePoint.Y);

                        if (this.IsSelected)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(this.ToString());
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;                  
                            Console.Write(this.ToString());
                        }
                    }
                }
            }
        }
    }
}
