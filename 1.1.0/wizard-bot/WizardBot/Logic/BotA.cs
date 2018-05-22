﻿using System;
using System.Collections.Generic;
using System.Linq;
using StarterBot.Entities;
using StarterBot.Enums;

namespace StarterBot
{
    public class BotA
    {
        private readonly GameState _gameState;
        private readonly int _attackCost;
        private readonly int _defenseCost;
        private readonly int _energyCost;
        private readonly int _mapWidth;
        private readonly int _mapHeight;
        private readonly Player _player;
        private readonly Player _opponent;
        private readonly Random _random;

        private int _rethink;
        private int _bestRethinkScore;

        public BotA(GameState gameState)
        {
            this._gameState = gameState;
            this._mapHeight = gameState.GameDetails.MapHeight;
            this._mapWidth = gameState.GameDetails.MapWidth;
            this._attackCost = gameState.GameDetails.BuildingPrices[BuildingType.Attack];
            this._defenseCost = gameState.GameDetails.BuildingPrices[BuildingType.Defense];
            this._energyCost = gameState.GameDetails.BuildingPrices[BuildingType.Energy];
            this._random = new Random((int) DateTime.Now.Ticks);

            _rethink = 0;

            _opponent = gameState.Players.Single(x => x.PlayerType == PlayerType.B);
            _player = gameState.Players.Single(x => x.PlayerType == PlayerType.A);
        }

        public string Run()
        {
            var commandToReturn = "";

            //This will check if there is enough energy to build any building before processing any commands
            if (_player.Energy < _defenseCost && _player.Energy < _energyCost && _player.Energy < _attackCost)
            {
                return commandToReturn;
            }
           // _opponent.
            //Get all opponent attack buildings
            var opponentAttackBuildings = GetBuildings(PlayerType.B, BuildingType.Attack);

            //Get all my attack buildings
            var myAttackBuildings = GetBuildings(PlayerType.A, BuildingType.Attack);
            //Get all my defense buildings
            var myDefenseBuildings = GetBuildings(PlayerType.A, BuildingType.Defense);

            var myBuildings = new List<CellStateContainer>();

            //Combine all buildings into a single list
            myBuildings.AddRange(myAttackBuildings);
            myBuildings.AddRange(myDefenseBuildings);

            //Do a random command if enemy has no attack buildings to defend against
            if (!opponentAttackBuildings.Any())
            {
                commandToReturn = GetRandomCommand(myBuildings);
            }
            else
            {
                //Get all rows with enemy buildings where I don't have a defense building
                var rows = GetEnemyBuildingRows(opponentAttackBuildings, myDefenseBuildings);

                //Place defense building randomly in first row from list
                if (rows.Count > 0)
                {
                    commandToReturn = GetValidAttackCommand(rows[0], myBuildings);
                }
                else
                {
                    commandToReturn = GetRandomCommand(myBuildings);
                }
            }

            return commandToReturn;
        }


        //public string Think()
        //{


        //    return commandToReturn;
        //}

        //Get a valid attack command for a row


        private string GetValidAttackCommand(int yCoordinate, List<CellStateContainer> myBuildings)
        {
            var xRandom = _random.Next(_mapWidth / 2);

            while (myBuildings.Any(x => x.X == xRandom && x.Y == yCoordinate && x.Buildings.Any()))
            {
                xRandom = _random.Next(_mapWidth / 2);
            }

            return $"{xRandom},{yCoordinate},{(int)BuildingType.Defense}";
        }

        //Get random valid command
        private string GetRandomCommand(List<CellStateContainer> myBuildings)
        {
            //Place building randomly on my half of the map
            var xRandom = _random.Next(_mapWidth / 2);
            var yRandom = _random.Next(_mapHeight);
            var btRandom = _random.Next(Enum.GetNames(typeof(BuildingType)).Length);

            while (myBuildings.Any(x => x.X == xRandom && x.Y == yRandom && x.Buildings.Any()))
            {
                xRandom = _random.Next(_mapWidth / 2);
                yRandom = _random.Next(_mapHeight);
            }

            return $"{xRandom},{yRandom},{btRandom}";
        }

        //Get all rows which contain an enemy building where I don't have a defense building
        private List<int> GetEnemyBuildingRows(List<CellStateContainer> opponentAttackBuildings,
            List<CellStateContainer> myDefenseBuildings)
        {
            var rows = new List<int>();
            foreach (var enemyAttackBuilding in opponentAttackBuildings)
            {
                if (rows.Contains(enemyAttackBuilding.Y)) continue;
                rows.Add(enemyAttackBuilding.Y);
            }

            foreach (var myDefenseBuilding in myDefenseBuildings)
            {
                if (rows.Contains(myDefenseBuilding.Y))
                {
                    rows.Remove(myDefenseBuilding.Y);
                }
            }

            return rows;
        }

        //Get buildings of a certain type for a certain player
        private List<CellStateContainer> GetBuildings(PlayerType playerType, BuildingType buildingType)
        {
            var list = new List<CellStateContainer>();
            foreach (var cellStateContainers in _gameState.GameMap)
            {
                foreach (var cellStateContainer in cellStateContainers)
                {
                    if (cellStateContainer.CellOwner == playerType &&
                        cellStateContainer.Buildings.Any(x => x.BuildingType == buildingType))
                    {
                        list.Add(cellStateContainer);
                    }
                }
            }

            return list;
        }
    }
}