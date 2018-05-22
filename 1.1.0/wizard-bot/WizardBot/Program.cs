using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using StarterBot.Entities;
using WizardBot.Logic;

namespace StarterBot
{
    public class Program
    {
        private static long _choiceTime = 2000;
        private static string _commandFileName = "command.txt";
        private static string _StopFileName = "stop.txt";
        private static string _testFileName = "test.txt";
        private static string _stateFileName = "state.json";

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            Stopwatch testwatch = new Stopwatch();
            if (File.Exists(_testFileName))
            {
                testwatch = JsonConvert.DeserializeObject<Stopwatch>(File.ReadAllText(_StopFileName));
            }

            stopwatch.Start();
            //GameState original_gameState = JsonConvert.DeserializeObject<GameState>(File.ReadAllText(_stateFileName));

            //while(stopwatch.ElapsedMilliseconds+ellapse < _choiceTime)
            //{

            //    var finalcommand = new Logic().Run();
            //    var command = new BotA(original_gameState).Run();
            //    //var command = new BotB(original_gameState).Run()
            //    ;

            //    ellapse = stopwatch.ElapsedMilliseconds - ellapse;
            //}
            //Get Next Command
            

            //Set Next Command
            while (stopwatch.IsRunning)
            {
                if (stopwatch.ElapsedMilliseconds == 1000)
                {
                    File.WriteAllText(_commandFileName, "0,0,1");
                    File.WriteAllText(_StopFileName, JsonConvert.SerializeObject(stopwatch));
                    long Time = stopwatch.ElapsedMilliseconds - testwatch.ElapsedMilliseconds;
                    File.WriteAllText(_testFileName, Time.ToString());
                    break;
                }
            }

        }

    }
}