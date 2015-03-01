namespace WordDestroyerGame
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MainMenu : IDrawableObject
    {
        public readonly char optionSelector = '@';
        public readonly string NewGameOption = "New Game";
        public readonly string HallOfFameOption = "Hall of Fame";
        public readonly string InstructionsOption = "Instructions";
        public readonly string AboutOption = "About";
        public readonly string ExitOption = "Exit";
        public readonly string ReturnOption = "Return";

        public bool NewGameStarted { get; set; }

        public void InteractionLoop()
        {
          
            // TODO: This method should listen for key input (mouse up/down) and call another methods 
            // that should handle the interactions. For instance if the current selection is "Instructions" and the 
            // user hits "Enter" then we should read the instructions from "Instructions.txt" file and draw them on the Console. 
            // Don't forget to add "Return" option in order the user to turn back into the main menu.
        }

        public void Draw()
        {   
            // TODO: The name of the game should be drawn along with the main menu navigaiton options and selector.
        }

        private void DrawHallOfFameOption()
        {
            using (StreamReader reader = new StreamReader("HallOfFame.txt"))
            {
                string fileContents = reader.ReadToEnd();
                Console.WriteLine(fileContents);

            }
 
        }

        private void DrawInstructionsOption()
        {
            using (StreamReader reader =  new StreamReader("Instructions.txt"))
            {
                string fileContents = reader.ReadToEnd();
                Console.WriteLine(fileContents);
               
            }
          
        }

        private void DrawAboutOption()
        {
            using (StreamReader reader = new StreamReader("About.txt"))
            {
                string fileContents = reader.ReadToEnd();
                Console.WriteLine(fileContents);

            }
            
        }

        public void Draw(Point point)
        {
            throw new NotImplementedException();
        }
    }
}
