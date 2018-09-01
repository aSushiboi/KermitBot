using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KermitBot.Modules
{
    public class at : ModuleBase<SocketCommandContext>
    {
        [Command("at")]
        public async Task atSync(SocketGuildUser user, int number)
        {
            for (int i = 1; i < number + 1; i++)
            {
                if (i == 1)
                {
                    await ReplyAsync($"@ing {user.Mention} " + i + " time!");
                }
                else
                {
                    await ReplyAsync($"@ing {user.Mention} " + i + " times!");
                }
            }
            await ReplyAsync("*Badaboom~* - Kermit");
        }
    }
}