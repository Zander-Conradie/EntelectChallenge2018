using Newtonsoft.Json;
using WizardBot.Enums;

namespace WizardBot.Entities
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PlayerType PlayerType { get; set; }
    }
}