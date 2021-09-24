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
            DB_Timer dB_Timer = new DB_Timer();
            DB_Timer.CreateTable_Timer();

            dBot = new DiscordBot();
            await dBot.RunAsync();
        }
    }
}
