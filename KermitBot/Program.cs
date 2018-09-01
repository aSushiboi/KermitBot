using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace KermitBot
{
    class Program
    {
        // Serves as asynchronous functionality with a synchronous method 
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _service;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            // singletons used for client and commands so we have the same client and commands at the same time
            // Also turn the ServiceColection into a service provider to prevent having wrong types and shit 
            _service = new ServiceCollection().AddSingleton(_client).AddSingleton(_commands).BuildServiceProvider();

            string botToken = "NDgyMDM0MTMwODc2MzY2ODQ5.Dl_EIg.TKMT95vUe4q3fU9q74T_4Y4S408";

            // event subscriptions
            _client.Log += Log;
            _client.UserJoined += AnnounceUserJoined;


            // Register command modules
            await RegisterCommandsAsync();
            // Log in
            await _client.LoginAsync(Discord.TokenType.Bot, botToken);
            // Start the client 
            await _client.StartAsync();
            // Delay task forever so the client stays 
            await Task.Delay(-1);

        }

        private async Task AnnounceUserJoined(SocketGuildUser user)
        {
            // Guild information contained in user object 
            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            // Use string interpolation to be able to add the user mention. user.Mention grabs the user's numerical id   
            await channel.SendMessageAsync($"Hi, {user.Mention}, ! Hope you have a wonderful time!");
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            // Mak sure that the message is null or is from a bot, then return the task 
            if (message is null || message.Author.IsBot) return;

            // argPos is used within the if statement, where it always references to the integer of variable argPos. 
            // It is assigned an integer so hopefully it'll be different after going through the method 
            // Specificaly when the prefix ends 
            int argPos = 0;
            // If the message has a string prefix of "tnt" or if the client has mentioned the bot
            if (message.HasStringPrefix("kermit-", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                // Create a new context, takes a client and message 
                var context = new SocketCommandContext(_client, message);
                // argPos has been updated at this point 
                // Execute the commands, pass in the command context, pass in the pointer to start looking for command name and pass the services collection that also auto eject any dependencies 
                var result = await _commands.ExecuteAsync(context, argPos, _service);
                // If result is not a success, print the error
                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }
    }
}