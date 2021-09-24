using DisCatSharp.ApplicationCommands;
using DisCatSharp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchattenclownBot.Model.Discord.ChoiceProvider
{
    /// <summary>
    /// The Hour choice provider.
    /// </summary>
    public class MinutesChoiceProvider : IChoiceProvider
    {
        /// <summary>
        /// Providers the choices.
        /// </summary>
        /// <returns>choices</returns>
        public async Task<IEnumerable<DiscordApplicationCommandOptionChoice>> Provider()
        {
            DiscordApplicationCommandOptionChoice[] choices = new DiscordApplicationCommandOptionChoice[12];

            int min = 0;

            for (int i = 0; i < 12; i++)
            {
                choices[i] = new DiscordApplicationCommandOptionChoice(min.ToString(), i.ToString());
                min += 5;
            }

            return choices;
        }
    }
}
