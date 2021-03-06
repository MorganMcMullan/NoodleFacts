﻿using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using NoodleFacts.Models;
using NoodleFacts.Services;

namespace NoodleFacts
{
	class Program
	{
		private DiscordSocketClient _client;

		private IConfiguration _config;

		public static void Main(string[] args)
		=> new Program().MainAsync().GetAwaiter().GetResult();

		public async Task MainAsync()
		{
			_client = new DiscordSocketClient();

			_config = BuildConfig();

			var services = ConfigureServices();
			services.GetRequiredService<LogService>();
			await services.GetRequiredService<CommandHandlingService>().InitializeAsync(services);

			var botToken = Environment.GetEnvironmentVariable("DISCORD_TOKEN");

			try
			{
				await _client.LoginAsync(TokenType.Bot, botToken);
			}
			catch (Discord.Net.HttpException ex) when (ex.HttpCode == HttpStatusCode.Unauthorized)
			{
				Console.WriteLine("Couldn't log in to Discord using DISCORD_TOKEN=\"{0}\". Is it empty or incorrect?", botToken);

				Environment.Exit(1);
			}

			await _client.StartAsync();

			// Block this task until the program is closed.
			await Task.Delay(-1);
		}
		private IServiceProvider ConfigureServices()
		{
			return new ServiceCollection()
				// Base
				.AddSingleton(_client)
				.AddSingleton<CommandService>()
				.AddSingleton<CommandHandlingService>()
				// Logging
				.AddLogging()
				.AddSingleton<LogService>()
				// Extra
				.AddSingleton(_config)
				// Add additional services here...
				.AddDbContext<RileyContext>(options => options.UseSqlite(_config["dbFile"]))
				.BuildServiceProvider();
		}

		private IConfiguration BuildConfig()
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("config.json")
				.Build();
		}
	}
}
