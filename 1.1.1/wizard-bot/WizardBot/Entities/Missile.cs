using WizardBot.Enums;

namespace WizardBot.Entities
{
    public class Missile
    {
        public int Damage { get; set; }
        public int Speed { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public PlayerType CellOwner { get; set; }
    }
}