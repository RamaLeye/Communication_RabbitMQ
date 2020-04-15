using System;
using System.Collections.Generic;
using System.Text;

namespace LogEmitter
{
    class RandomObjects
    {
        public RandomObjects()
        {

        }
        public string randomMsg()
        {
            var random = new Random();
            var list = new List<string> { "Pluie", "Neige", "Soleil", "Sable",
                "Danse", "Chant", "Nourriture", "Espoir", "Boisson", "Moustache",
                "Clown", "Jeux", "C#", "Code", "Emitter", "Receiver" };

            int index = random.Next(list.Count);
            return list[index];
        }

        public string randomSeverity()
        {
            var random = new Random();
            var list = new List<string> { "info", "warning", "error", "critical" };

            int index = random.Next(list.Count);
            return list[index];
        }
    }
}
