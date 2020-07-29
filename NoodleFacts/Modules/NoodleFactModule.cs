using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using NoodleFacts.Models;

namespace NoodleFacts.Modules
{
	public class NoodleFactModule : ModuleBase<SocketCommandContext>
	{
		private readonly RileyContext dbContext;

		private int lastFact;

		public NoodleFactModule(RileyContext context)
		{
			lastFact = 0;
			dbContext = context;
		}

		[Command("Hi Riley")]
		public async Task Great1([Remainder] string ignore = null)
		{
			await GetRandomNoodleFactAsync();
		}

		[Command("Hi Riley!")]
		public async Task Great2([Remainder] string ignore = null)
		{
			await GetRandomNoodleFactAsync();
		}

		[Command("Hi Riley.")]
		public async Task Great3([Remainder] string ignore = null)
		{
			await GetRandomNoodleFactAsync();
		}

		[Command("mlem")]
		public async Task Mlem1([Remainder] string ignore = null)
		{
			string reply = "Mlem!";
			Random random = new Random();
			if (random.Next(100) + 1 == 100)
			{
				reply = "*Yawn*";
			}
			await ReplyAsync(reply);
		}


		[Command("mlem!")]
		public async Task Mlem2([Remainder] string ignore = null)
		{
			string reply = "Mlem!";
			Random random = new Random();
			if (random.Next(100) + 1 == 100)
			{
				reply = "*Yawn*";
			}
			await ReplyAsync(reply);
		}

		[Command("mlem.")]
		public async Task Mlem3([Remainder] string ignore = null)
		{
			string reply = "Mlem!";
			Random random = new Random();
			if (random.Next(100) + 1 == 100)
			{
				reply = "*Yawn*";
			}
			await ReplyAsync(reply);
		}

		[Command("mlem?")]
		public async Task Mlem4([Remainder] string ignore = null)
		{
			string reply = "Mlem!";
			Random random = new Random();
			if (random.Next(100) + 1 == 100)
			{
				reply = "*Yawn*";
			}
			await ReplyAsync(reply);
		}

		[Command("🐀")]
		public async Task Nom1([Remainder] string ignore = null)
		{
			string reply = "*Nom*";
			await ReplyAsync(reply);
		}

		[Command("🐁")]
		public async Task Nom2([Remainder] string ignore = null)
		{
			string reply = "*Nom*";
			await ReplyAsync(reply);
		}

		[Command("🐭")]
		public async Task Nom3([Remainder] string ignore = null)
		{
			string reply = "*Nom*";
			await ReplyAsync(reply);
		}

		[Command("!inabox")]
		public async Task InABox([Remainder] string ignore = null)
		{
			await ReplyAsync("I'm living in a box now. 🐳📦");
		}

		public async Task GetRandomNoodleFactAsync([Remainder] string ignore = null)
		{
			lastFact = new Random().Next(dbContext.Facts.Count());
			string fact = dbContext.Facts.First(f => f.FactID == lastFact).FactText;
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
				string fact = dbContext.Facts.First(f => f.FactID == factId).FactText;
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
		public async Task GetNoodleFactCountAsync([Remainder] string ignore = null)
		{
			int factCount = dbContext.Facts.Count();
			await ReplyAsync("Mlem! I know " + factCount + " noodle facts!");
		}

		[RequireUserPermission(GuildPermission.Administrator)]
		[Command("+noodlefact")]
		public async Task AddNoodleFactAsync([Remainder] string newFact)
		{
			await dbContext.Facts.AddAsync(new Fact
			{
				FactText = newFact
			});
			await dbContext.SaveChangesAsync();

			await ReplyAsync("Mlem! I learned something new!");
		}

		[RequireUserPermission(GuildPermission.Administrator)]
		[Command("-noodlefact")]
		public async Task RemoveNoodleFactAsync(int factId)
		{
			dbContext.Facts.Remove(dbContext.Facts.First(f => f.FactID == factId));
			await dbContext.SaveChangesAsync();

			await ReplyAsync("Mlem! I have forgotten that fact");
		}

		[RequireUserPermission(GuildPermission.Administrator)]
		[Command("!lastfact")]
		public async Task GetLastFactAsync([Remainder] string ignore = null)
		{
			Fact fact = await dbContext.Facts.FirstAsync(f => f.FactID == lastFact);
			var reply = "Mlem! The last fact was: " + fact.FactText;
			reply += "\nMlem! The last fact was number: " + fact.FactID + 1;
			await ReplyAsync(reply);
		}
	}
}