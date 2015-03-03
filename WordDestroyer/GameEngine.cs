namespace WordDestroyerGame
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GameEngine
    {
        public void StartGame()
        {
            InitializeConsoleWindow();
            GameMainLoop();
        }

        private void InitializeConsoleWindow()
        {
            Console.CursorVisible = false;
            Console.Title = "Word Destroyer Game";
            Console.SetWindowSize(45, 45);
            Console.SetBufferSize(45, 45);
            Console.Write(string.Empty);
        }

        private void GameMainLoop()
        {
            MainMenu mainMenu = new MainMenu();

            while (true)
            {
                mainMenu.Draw(new Point(Console.WindowWidth / 8, Console.WindowHeight / 2));
                mainMenu.InteractionLoop();

                if (mainMenu.NewGameStarted == true)
                {
                    throw new NotImplementedException();
                    // TODO: The game field plus the runtime menu should be drawn and the game should start.
                }
            }
        }

        private void StartNewGame()
        {

        }
    }
}
