using System;
using System.Collections.Generic;
using System.Linq;

namespace pro
{
    class Program
    {
        static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        static void Main(string[] args)
        {
            var stickSuccess = 0;
            var switchSuccess = 0;

            for (var i = 0; i < 100000; i++)
            {
                var result = RunTest();
                if (result.resultStick)
                    stickSuccess++;
                    
                if (result.resultSwitch)
                    switchSuccess++;
            }

            Console.WriteLine($"stickSuccess: {stickSuccess}, switchSuccess: {switchSuccess}");
        }

        static List<bool> BuildChoices(int length)
        {
            var choices = new List<(bool, int)>
            {
                (true, Random.Next())
            };

            for (var i = 1; i < length; i++)
                choices.Add((false, Random.Next()));

            return choices
                .OrderBy(x => x.Item2)
                .Select(x => x.Item1)
                .ToList();
        }

        static (bool resultSwitch, bool resultStick) RunTest()
        {
            var choices = BuildChoices(3);
            
            var choice = Random.Next(choices.Count);
            var hostChoice = GetHostChoice(choices, choice);

            return (
                choices[SwitchChoice(choices, choice, hostChoice)],
                choices[choice]);
        }

        static int GetHostChoice(List<bool> choices, int choice)
        {
            var potentials = choices
                .Select((c, i) => (c: c, i: i))
                .Where(x => x.i != choice && !x.c)
                .ToList();

            return potentials[Random.Next(potentials.Count)].i;
        }

        static int SwitchChoice(List<bool> choices, int choice, int hostChoice)
        {
            var potentials = choices
                .Select((c, i) => (c: c, i: i))
                .Where(x => x.i != choice && x.i != hostChoice)
                .ToList();

            return potentials[Random.Next(potentials.Count)].i;
        }

        static int OneInThree()
        {
            return Random.Next(3) + 1;
        }
    }
}
