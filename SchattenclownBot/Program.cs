using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using SchattenclownBot.Model.Discord;
using SchattenclownBot.Model.Persistence;
using SchattenclownBot.Model.Objects;

namespace SchattenclownBot
{
    /// <summary>
    /// The program boot class.
    /// </summary>
    class Program
    {
        private static DiscordBot dBot;
        /// <summary>
        /// the boot task
        /// </summary>
        /// <returns>Nothing</returns>
        static async Task Main()
        {
            DB_ScTimer dB_Timer = new DB_ScTimer();
            DB_ScTimer.CreateTable_Timer();

            dBot = new DiscordBot();
            await dBot.RunAsync();
        }
    }
}
