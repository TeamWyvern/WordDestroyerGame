namespace WordDestroyerGame
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Speech.Synthesis;

    public class MainMenu : IDrawableObject //I have no idea how to work with interfaces. Need help with this
    {
        public readonly char optionSelector = '@';
        public readonly string NewGameOption = "New Game";
        public readonly string HallOfFameOption = "Hall of Fame";
        public readonly string InstructionsOption = "Instructions";
        public readonly string AboutOption = "About";
        public readonly string ExitOption = "Exit";
        public readonly string ReturnOption = "Return";

        public List<string> UserMenuList = new List<string>() { "New Game", "Hall of Fame", "Instructions", "About", "Exit" };
        public Stopwatch AnimationStopwatch = new Stopwatch();
        SpeechSynthesizer natashaVolkova = new SpeechSynthesizer();     //Not necessary, but cool. Natasha Volkova --> RA3 Sniper. Synth needs new name

        public bool NewGameStarted { get; set; }

        public void InteractionLoop()
        {
            // TODO: This method should listen for key input (mouse up/down --> what do you mean mouse? ) and call another method 
            // that should handle the interactions. For instance if the current selection is "Instructions" and the 
            // user hits "Enter" then we should read the instructions from "Instructions.txt" file and draw them on the Console. 
            // Don't forget to add "Return" option in order the user to turn back into the main menu.

            ConsoleKeyInfo inputWaiter;         //Needs a better name

            do      //Placeholder code. Needs to be finished
            {
                inputWaiter = Console.ReadKey(true); //Does not show the key 

                switch (inputWaiter.Key)        //Needs finishing
                {
                    case ConsoleKey.DownArrow:  //Which keys will we be waiting for?
                        Console.Clear();
                        DrawHallOfFameOption(); //Example code
                        break;
                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        break;
                    case ConsoleKey.Backspace:
                        Console.Clear();
                           AnimationStopwatch.Reset();
                            AnimationStopwatch.Start();
                        while (AnimationStopwatch.ElapsedMilliseconds<500)  //Trying to get this loading animtion to work damnit!
                        {
                            if (AnimationStopwatch.ElapsedMilliseconds > 100 && AnimationStopwatch.ElapsedMilliseconds < 200)
                            {
                                Console.Clear();
                                Console.WriteLine("Loading : <1>"); //Current symbols are placeholders
                            }
                            if (AnimationStopwatch.ElapsedMilliseconds > 200 && AnimationStopwatch.ElapsedMilliseconds < 300)
                            {
                                Console.Clear();
                                Console.WriteLine("Loading : <2>");
                            }
                            if (AnimationStopwatch.ElapsedMilliseconds > 300 && AnimationStopwatch.ElapsedMilliseconds < 400)
                            {
                                Console.Clear();
                                Console.WriteLine("Loading : <3>");
                            }
                            if (AnimationStopwatch.ElapsedMilliseconds > 400 && AnimationStopwatch.ElapsedMilliseconds < 500)
                            {
                                Console.Clear();
                                Console.WriteLine("Loading : <4>");
                                break;
                            }
                            
                        }

                        Draw();
                        break;

                    default:
                        Console.Clear();
                        break;
                    // etc..
                }
            } while (inputWaiter.Key != ConsoleKey.Escape);
        }

        public void Draw()
        {
            // TODO: The name of the game should be drawn along with the main menu navigaiton options and selector.
            string startMessage = "Welcome to the game/Placeholder";    //Needs to be moved elsewhere so it is only executed once
            int y = Console.WindowHeight / 2;
            int x = (Console.WindowWidth / 2) - (startMessage.Length / 2);        //Needs fine adjustments
            Console.SetCursorPosition(x, y);
            Console.Title = string.Empty;
            for (int i = 0; i < startMessage.Length; )
            {
                AnimationStopwatch.Start();
                //if (AnimationStopwatch.ElapsedMilliseconds == 100)  //Do not erase this line! Atleast not now
                {
                    Console.Write(startMessage[i]);
                    Console.Title += startMessage[i];
                    i++;
                    AnimationStopwatch.Reset();
                }
            }
            AnimationStopwatch.Start();

            //natashaVolkova.Speak("Welcome to Word Destroyer, a game created by team wyvern");  //Rough synth. Needs edits

            while (true)    //Placeholder method of measuring time used. Needs replacement or improvement
            {
                //if (AnimationStopwatch.ElapsedMilliseconds > 1500)    //Disabled for programming purposes. Gotta be re-enabled
                {
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 8, y);    //Placeholder calculations need to be imroved to dynamically scale  (Written in 1680x1050)
                    Console.Write(string.Join("\n" + new string(' ', Console.WindowWidth / 2 - 8), UserMenuList));    //WriteLine switched to string.Join for easy maintenance and modification
                    break;
                }
            }
        }

        private void DrawHallOfFameOption()
        {
            // TODO: This method will be called when HallOfFame option is selected and "Enter" key is pressed.
            // Probably string.Join a list containing scores and a list containing name or maybe a dicitonary?
        }

        private void DrawInstructionsOption()
        {
            // TODO: This method will be called when Instructions option is selected and "Enter" key is pressed.
        }

        private void DrawAboutOption()
        {
            // TODO: This method will be called when About option is selected and "Enter" key is pressed.
        }

        public void Draw(Point point)
        {
            throw new NotImplementedException();
        }
    }
}
