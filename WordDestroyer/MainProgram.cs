namespace WordDestroyerGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MainProgram
    {
        static void Main(string[] args)
        {
            GameEngine gameEngine = new GameEngine();
            gameEngine.StartGame();
        }
    }
}
