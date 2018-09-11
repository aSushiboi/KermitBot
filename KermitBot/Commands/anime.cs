using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Audio;
namespace KermitBot.Commands
{
    public class anime: ModuleBase<SocketCommandContext>
    {
        [Command("anime")]
        public async Task animeSync()
        {
             await ReplyAsync("Did someone post anime?");
             await ReplyAsync("https://cdn.discordapp.com/attachments/228936388068900874/489112096965918770/unknown.png");
         }
    }
}
