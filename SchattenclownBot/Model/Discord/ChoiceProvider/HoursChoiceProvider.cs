using DisCatSharp.ApplicationCommands;
using DisCatSharp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchattenclownBot.Model.Discord.ChoiceProvider
{
    /// <summary>
    /// The Hour choice provider.
    /// </summary>
    public class HoursChoiceProvider : IChoiceProvider
    {
        /// <summary>
        /// Providers the choices.
        /// </summary>
        /// <returns>choices</returns>
        public async Task<IEnumerable<DiscordApplicationCommandOptionChoice>> Provider()
        {
            DiscordApplicationCommandOptionChoice[] choices = new DiscordApplicationCommandOptionChoice[25];

            for (int i = 0; i < 25; i++)
            {
                choices[i] = new DiscordApplicationCommandOptionChoice(i.ToString(), i.ToString());
            }

            return choices;
        }
    }
}
