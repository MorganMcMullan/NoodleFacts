using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NoodleFacts.Facts
{
    public static class NoodleFactList
    {
        static public List<NoodleFact> FactList { get; private set; }

        static public int LastFact {get; private set; }

        static NoodleFactList()
        {
            FactList = JsonConvert.DeserializeObject<List<NoodleFact>>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "facts.json")));
        }

        public static string GetRandomFact()
        {
            Random random = new Random();

            LastFact = random.Next(FactList.Count);

            return FactList[LastFact].Fact;
        }

        public static string GetFact(int factId) 
        {
            return FactList[factId].Fact;
        }

        public static void AddFact(string fact)
        {
            FactList.Add(new NoodleFact(fact));
            LastFact = FactList.Count - 1;
        }

        public static void RemoveFact(int factNumber) 
        {
            FactList.RemoveAt(factNumber);
        }

        public static void LoadFacts()
        {
            FactList = JsonConvert.DeserializeObject<List<NoodleFact>>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "facts.json")));
        }

        public static async void SaveFacts() 
        {
            string factListJson = JsonConvert.SerializeObject(FactList);
            await File.WriteAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "facts.json"), factListJson);
        }
    }
}