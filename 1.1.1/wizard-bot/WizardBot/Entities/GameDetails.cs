using System;
using System.Collections.Generic;
using WizardBot.Enums;

namespace WizardBot.Entities
{
    public class GameDetails
    {
        public int Round { get; set; }
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public int RoundIncomeEnergy { get; set; }



        public Dictionary<BuildingType, BuildingStats> BuildingsStats { get; set; }
    }
}