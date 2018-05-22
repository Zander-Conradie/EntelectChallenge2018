using System.Collections.Generic;
using WizardBot.Enums;

namespace WizardBot.Entities
{
    public class GameMapPosition
    {
        public int X { get; set; } 
        public int Y { get; set; } 
        public PlayerType CellOwner { get; set; } 
        public List<Building> Buildings { get; set; } 
        public List<Missile> Missiles { get; set; } 
    }
}