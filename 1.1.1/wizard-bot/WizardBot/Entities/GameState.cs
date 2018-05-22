using System.Collections.Generic;

namespace WizardBot.Entities
{
    public class GameState
    {
        public GameDetails GameDetails { get; set; }
        public List<Player> Players { get; set; }
        public GameMapPosition[][] GameMap { get; set; }

    }
}