namespace WordDestroyerGame
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Speech.Synthesis;
    using System.Diagnostics;
    using System.Threading;

    
    public class MainMenu : IDrawableObject 
    {
        private int selectedMainMenuLine = new int();
        private bool newGameStarted;
        public readonly char optionSelector = '>';
        public List<string> UserMenuList = new List<string>() { " New Game", " Hall of Fame", " Instructions", " About", " Exit" };
        public Stopwatch AnimationStopwatch = new Stopwatch();
        SpeechSynthesizer natashaVolkova = new SpeechSynthesizer();     //Not necessary, but cool. Natasha Volkova --> RA3 Sniper. Synth needs new name

        public bool NewGameStarted
        {
            get { return newGameStarted; }
            set { newGameStarted = value; }
        }

        public void InteractionLoop()
        {

            // TODO: This method should listen for key input (arrow up/down) and call another methods 
            // TODO: This method should listen for key input (arrow up/down) and call another method 
            // that should handle the interactions. For instance if the current selection is "Instructions" and the 
            // user hits "Enter" then we should read the instructions from "Instructions.txt" file and draw them on the Console. 
            // Don't forget to add "Return" option in order the user to turn back into the main menu.

            ConsoleKeyInfo userInputKey;

            do      
            {
                if (this.newGameStarted == true)
                {
                    break;
                }
                userInputKey = Console.ReadKey(true); //Does not show the key 

                switch (userInputKey.Key)        
                {
                    case ConsoleKey.DownArrow:  //Which keys will we be waiting for?
                        Console.Clear();
                        if (selectedMainMenuLine == UserMenuList.Count-1)
                        {
                            selectedMainMenuLine = -1;
                        }
                        selectedMainMenuLine += 1;
                        DrawMainMenu(selectedMainMenuLine);
                        break;
                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        if (selectedMainMenuLine == 0)
                        {
                            selectedMainMenuLine = UserMenuList.Count;
                        }
                        selectedMainMenuLine -= 1;
                        DrawMainMenu(selectedMainMenuLine);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        DrawSubMenu();
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

                        //this.Draw();
                        break;

                    default:
                        Console.Clear();
                        break;
                    // etc..

                }
            } while (userInputKey.Key != ConsoleKey.Escape);
        }

        public void Draw()
        {   
            AnimationStopwatch = new Stopwatch();
            // TODO: The name of the game should be drawn along with the main menu navigaiton options and selector.
            string startMessage = string.Format("Welcome to the {0}!", Console.Title);    //Needs to be moved elsewhere so it is only executed once
            Console.SetCursorPosition(Console.WindowWidth / 8, Console.WindowHeight / 2);
            for (int i = 0; i < startMessage.Length; )
            {
                AnimationStopwatch.Start();
                //if (AnimationStopwatch.ElapsedMilliseconds == 100)  //Do not erase this line! Atleast not now
                //{
                    Console.Write(startMessage[i]);
                    //Console.Title += startMessage[i];
                    i++;
                    AnimationStopwatch.Reset();
                    Thread.Sleep(50);
                //}
            }
            AnimationStopwatch.Start();

            //natashaVolkova.Speak("Welcome to Word Destroyer, a game created by team wyvern");  //Rough synth. Needs edits

            //The logic below is in the method DrawMainMenu(). If you need animation.stopwatch you can add it there.
            //while (true)    //Placeholder method of measuring time used. Needs replacement or improvement
            //{
            //    //if (AnimationStopwatch.ElapsedMilliseconds > 1500)    //Disabled for programming purposes. Gotta be re-enabled
            //    {
            //        Console.Clear();
            //        Console.SetCursorPosition(Console.WindowWidth / 2 - 8, y);    //Placeholder calculations need to be imroved to dynamically scale  (Written in 1680x1050)
            //        Console.Write(string.Join("\n" + new string(' ', Console.WindowWidth / 2 - 8), UserMenuList));    //WriteLine switched to string.Join for easy maintenance and modification
            //        break;
            //    }
            //}
            Thread.Sleep(1000);
            DrawMainMenu(selectedMainMenuLine);
        }

        private void DrawMainMenu(int selectedMainMenuLine)
        {
            Console.Clear();
            
            for (int i = 0; i < UserMenuList.Count; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + i);
                if (i==selectedMainMenuLine)
                {
                    Console.Write(optionSelector);
                }
                Console.Write(UserMenuList[i]);
            }
        }

        private void DrawSubMenu()
        {
            switch (selectedMainMenuLine)
            {
                case 0: this.newGameStarted = true;
                    break;
                case 1: DrawHallOfFameOption();
                    break;
                case 2: DrawInstructionsOption();
                    break;
                case 3: DrawAboutOption();
                    break;
                case 4: Console.Clear();
                    Console.WriteLine("GoodBye!");
                    System.Environment.Exit(1);
                    break;
            }
        }

        private void DrawHallOfFameOption()
        {
            // TODO: This method will be called when HallOfFame option is selected and "Enter" key is pressed.
            // Probably string.Join a list containing scores and a list containing name or maybe a dicitonary?

            using (StreamReader reader = new StreamReader(@"..\..\Data\HallOfFame.txt"))
            {
                string fileContents = reader.ReadToEnd();
                Console.WriteLine(fileContents);

            } 
        }

        private void DrawInstructionsOption()
        {
            using (StreamReader reader =  new StreamReader(@"..\..\Data\Instructions.txt"))
            {
                string fileContents = reader.ReadToEnd();
                Console.WriteLine(fileContents);
               
            }
        }

        private void DrawAboutOption()
        {
            using (StreamReader reader = new StreamReader(@"..\..\Data\About.txt"))
            {
                string fileContents = reader.ReadToEnd();
                Console.WriteLine(fileContents);

            }
        }
    }
}
