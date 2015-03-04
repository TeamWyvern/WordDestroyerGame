namespace WordDestroyerGame
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
        private int CurrentCharIndex { get; set; }
        private bool IsWordSelected { get; set; }
        private int LivesCount = 3;
        private long Score = 0;
        private int Coefficient = 0;

        public void StartGame()
        {
            MainMenu mainMenu = new MainMenu();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
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

            SpaceshipObject spaceship = new SpaceshipObject(new Point(12, 39));
            spaceship.Draw();

            Thread thread = new Thread(() => this.DrawWords());
            thread.Start();

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (!Char.IsLetter(key.KeyChar))
                {
                    continue;
                }

                if (this.IsWordSelected)
                {
                    if (this.SelectedWord.Element.Text[this.CurrentCharIndex] == key.KeyChar)
                    {
                        char[] text = this.SelectedWord.Element.Text.ToCharArray();
                        text[this.CurrentCharIndex] = ' ';
                        this.SelectedWord.Element.Text = new string(text);
                        this.CurrentCharIndex++;

                        if (this.CheckWordIsCompleted(this.SelectedWord.Element.Text))
                        {
                            this.SelectedWord.Draw();
                            this.IsWordSelected = false;
                            this.SelectedWord.IsSelected = false;
                            this.SelectedWord.IsVisible = false;
                            this.SelectedWord.IsDestroyed = true;
                            this.Score += this.Coefficient * 10;
                            this.Coefficient = 0;
                            this.CurrentCharIndex = 0;
                        }
                    }
                }
                else
                {
                    this.SelectedWord = visibleWords.Where(x => x.Element.Text[0] == key.KeyChar).FirstOrDefault();

                    if (this.SelectedWord != null)
                    {
                        this.Coefficient = this.SelectedWord.Element.Text.Length;
                        this.IsWordSelected = true;
                        this.SelectedWord.IsSelected = true;
                        char[] text = this.SelectedWord.Element.Text.ToCharArray();
                        text[this.CurrentCharIndex] = ' ';
                        this.SelectedWord.Element.Text = new string(text);
                        this.CurrentCharIndex++;
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

        private bool CheckWordIsCompleted(string text)
        {
            foreach (var item in text.ToCharArray())
            {
                if (item != ' ')
                {
                    return false;
                }
            }

            return true;
        }

        private void DrawWords()
        {
            Stopwatch newGameStopwatch = new Stopwatch();
            newGameStopwatch.Start();
            
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
                            if (currentPoint.Y > 0 && currentPoint.Y < 2)
                            {
                                currentPoint.X = randomGenerator.Next(0, 25);
                            }
                            word.Element.CoordinatePoint = currentPoint;
                            if (currentPoint.Y == 45)
                            {
                                this.LivesCount--;

                            }
                        }
                    }
                }
      
                //Console.Clear();

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

                //separation line
                for (int i = 0; i < 45; i++)
                {
                    PrintStringOnPosition(30, i, "|", ConsoleColor.Green);
                }
                PrintStringOnPosition(32, 10, "Lives: " + this.LivesCount, ConsoleColor.Green);
                PrintStringOnPosition(32, 11, "Points: " + this.Score, ConsoleColor.Green);
                if (this.LivesCount == 0)
                {
                    PrintStringOnPosition(10, 10, "Game over", ConsoleColor.Red);
                    PrintStringOnPosition(10, 11, "Press ENTER to exit", ConsoleColor.Red);
                    Console.WriteLine();
                    Environment.Exit(0);
                }

                Thread.Sleep(350);
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
                randomChar = GetRandomLetter();
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
                        wordsDictionary[currentWord[0]].Add(new WordObject(currentWord.Trim(), new Point(Console.WindowWidth / 2, 0)));
                    }
                }
            }
        }
    }
}
