using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using NoodleFacts.Facts;

namespace NoodleFacts.Modules
{
    public class NoodleFactModule : ModuleBase<SocketCommandContext>
    {
        [Command("Hi Riley")]
        public async Task Great1()
        {
            await GetRandomNoodleFactAsync();
        }

        [Command("Hi Riley!")]
        public async Task Great2()
        {
            await GetRandomNoodleFactAsync();
        }

        [Command("Hi Riley.")]
        public async Task Great3()
        {
            await GetRandomNoodleFactAsync();
        }

        [Command("mlem")]
        public async Task Mlem1()
        {
            string reply = "Mlem!";
            Random random = new Random();
            if (random.Next(100)+1 == 100) 
            {
                reply = "*Yawn*";
            }
            await ReplyAsync(reply);
        }


        [Command("mlem!")]
        public async Task Mlem2()
        {
            string reply = "Mlem!";
            Random random = new Random();
            if (random.Next(100)+1 == 100) 
            {
                reply = "*Yawn*";
            }
            await ReplyAsync(reply);
        }

        [Command("mlem,")]
        public async Task Mlem3()
        {
            string reply = "Mlem!";
            Random random = new Random();
            if (random.Next(100)+1 == 100) 
            {
                reply = "*Yawn*";
            }
            await ReplyAsync(reply);
        }

        public async Task GetRandomNoodleFactAsync()
        {
            string fact = NoodleFactList.GetRandomFact();
            fact = fact == "" ? "Snakes are cute" : fact;
            await ReplyAsync("Mlem! Hi " + Context.User.Mention + "! Did you know that: " + fact);
        }

        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("!noodlefact")]
        public async Task GetNoodleFactAsync(int factId)
        {
            try
            {
                factId--;
                string fact = NoodleFactList.GetFact(factId);
                fact = fact == "" ? "Snakes are cute" : fact;
                await ReplyAsync("Mlem! Hi " + Context.User.Mention + "! Did you know that: " + fact);
            }
            catch (Exception)
            {
                await ReplyAsync("Mlem! That's not a valid fact number!");
            }
        }
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("!noodlefactcount")]
        public async Task GetNoodleFactCountAsync()
        {
            int factCount = NoodleFactList.FactList.Count;
            await ReplyAsync("Mlem! I know " + factCount + " noodle facts!");
        }

        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("+noodlefact")]
        public async Task AddNoodleFactAsync([Remainder] string newFact)
        {
            NoodleFactList.AddFact(newFact);
            NoodleFactList.SaveFacts();

            await ReplyAsync("Mlem! I learned something new!");
        }

        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("-noodlefact")]
        public async Task RemoveNoodleFactAsync(int factId)
        {
            NoodleFactList.RemoveFact(factId);
            NoodleFactList.SaveFacts();

            await ReplyAsync("Mlem! I have forgotten that fact");
        }

        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("!lastfact")]
        public async Task GetLastFactAsync()
        {
            int lastfact = NoodleFactList.LastFact;
            var reply = "Mlem! The last fact was: " + NoodleFactList.FactList[lastfact].Fact;
            reply += "\nMlem! The last fact was number: " + lastfact;
            await ReplyAsync(reply);
        }
    }
}