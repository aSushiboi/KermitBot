using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Audio;

namespace KermitBot.Modules
{
    public class sad : ModuleBase<SocketCommandContext>
    {
        [Command("sad")]
        public async Task sadSync()
        {
           await ReplyAsync("Someone is sad. This should help https://www.youtube.com/watch?v=kJQP7kiw5Fk ");
        }
    }
}
