﻿namespace WordDestroyerGame
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class GameEngine
    {
        public GameEngine()
        {
            this.InitializeConsoleWindow();
            this.FillWordsDictionary();
            this.FillAlphabetDictionary();
        }

        Random randomGenerator = new Random();
        private Dictionary<char, List<WordObject>> wordsDictionary = new Dictionary<char, List<WordObject>>();
        private Dictionary<char, bool> alphabetDictionary = new Dictionary<char, bool>();
        private List<WordObject> visibleWords = new List<WordObject>();
        private WordObject SelectedWord { get; set; }
        private bool IsWordSelected { get; set; }

        public void StartGame()
        {
            MainMenu mainMenu = new MainMenu();

            while (true)
            {
                mainMenu.Draw();
                mainMenu.InteractionLoop();

                if (mainMenu.NewGameStarted == true)
                {
                    // TODO: The game field plus the runtime menu should be drawn and the game should start.
                    this.StartNewGame();
                    break;
                }
            }
        }

        private void InitializeConsoleWindow()
        {
            Console.CursorVisible = false;
            Console.Title = "Word Destroyer Game";
            Console.SetWindowSize(45, 45);
            Console.SetBufferSize(45, 45);
            Console.Write(string.Empty);
            
        }

        private void StartNewGame()
        {
            Console.Clear();
            ReadAllWords();

       
            Thread thread = new Thread(() => this.DrawWords());
            thread.Start();

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (this.IsWordSelected)
                {
                    if (this.SelectedWord.Element.Text[0] == key.KeyChar)
                    {
                        this.SelectedWord.Element.Text = this.SelectedWord.Element.Text.Remove(0, 1);

                        if (this.SelectedWord.Element.Text == string.Empty)
                        {
                            this.IsWordSelected = false;
                            this.SelectedWord.IsSelected = false;
                            this.SelectedWord.IsVisible = false;
                            this.SelectedWord.IsDestroyed = true;
                        }
                    }
                }
                else
                {
                    this.SelectedWord = visibleWords.Where(x => x.Element.Text[0] == key.KeyChar).FirstOrDefault();

                    if (this.SelectedWord != null)
                    {
                        this.IsWordSelected = true;
                        this.SelectedWord.IsSelected = true;
                        this.SelectedWord.Element.Text = this.SelectedWord.Element.Text.Remove(0, 1);
                    }
                }
            }
        }

        private void FillWordsDictionary()
        {
            for (int i = 97; i < 123; i++)
            {
                this.wordsDictionary.Add(Convert.ToChar(i), new List<WordObject>());               
            }
        }

        private void FillAlphabetDictionary()
        {
            for (int i = 97; i < 123; i++)
            {
                alphabetDictionary.Add(Convert.ToChar(i), false);
            }
        }

        private void ResetAlphabetDictionary()
        {
            this.alphabetDictionary.Clear();
            this.FillAlphabetDictionary();
        }

        private void DrawWords()
        {
            Stopwatch newGameStopwatch = new Stopwatch();
            newGameStopwatch.Start();
            int livesCount = 3;
            int playFieldWidth = 20;
            Random randomGen = new Random();

            while (true)
            {
                if (newGameStopwatch.ElapsedMilliseconds % 3000 < 500)
                {
                    char randomLetter = this.GetRandomLetter();
                    WordObject currentWord = wordsDictionary[randomLetter]
                        .Where(x => x.IsVisible == false && x.IsMissed == false && x.IsDestroyed == false)
                        .FirstOrDefault();
                    

                    if (currentWord != null)
                    {
                        currentWord.IsVisible = true;                        
                    }
                }

                foreach (var currentWordCollection in wordsDictionary.ToArray())
                {
                    foreach (var word in currentWordCollection.Value)
                    {
                        if (word.IsVisible)
                        {
                            Point currentPoint = word.Element.CoordinatePoint;
                            currentPoint.Y += 1;
                            currentPoint.X = randomGen.Next(0, playFieldWidth);
                            word.Element.CoordinatePoint = currentPoint;
                        }
                    }
                }

                Console.Clear();

                foreach (var currentWordCollection in wordsDictionary.ToArray())
                {
                    foreach (var word in currentWordCollection.Value)
                    {
                        if (word.IsVisible)
                        {
                            word.Draw();
                            this.visibleWords.Add(word);
                        }
                    }
                }
                PrintStringOnPosition(30, 10, "Lives:" + livesCount, ConsoleColor.White);
                PrintStringOnPosition(30, 11, "Points:" , ConsoleColor.White);

                Thread.Sleep(500);
                this.visibleWords.Clear();
            }
        }
        static void PrintStringOnPosition(int col, int row, string str, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.SetCursorPosition(col, row);
            Console.ForegroundColor = color;
            Console.Write(str);
        }
        private char GetRandomLetter()
        {
            if (!this.alphabetDictionary.ContainsValue(false))
            {
                this.ResetAlphabetDictionary();
            }

            char randomChar = Convert.ToChar(this.randomGenerator.Next(97, 123));

            // If this letter has been used, recursively call this method again
            if (this.alphabetDictionary[randomChar] == true)
            {
                GetRandomLetter();
            }

            this.alphabetDictionary[randomChar] = true;
            return randomChar;
        }

        private void ReadAllWords()
        {
            string currentWord = null;
            StreamReader wordsReader = new StreamReader(@"..\..\Data\SomeWords.txt");

            using (wordsReader)
            {
                while ((currentWord = wordsReader.ReadLine()) != null)
                {
                    if (wordsDictionary.ContainsKey(currentWord[0]))
                    {
                        wordsDictionary[currentWord[0]].Add(new WordObject(currentWord, new Point(Console.WindowWidth / 2, 0)));                        
                    }
                }
            }
        }
    }
}
