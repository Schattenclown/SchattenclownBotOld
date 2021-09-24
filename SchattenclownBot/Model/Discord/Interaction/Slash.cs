using DisCatSharp;
using DisCatSharp.ApplicationCommands;
using DisCatSharp.Entities;
using DisCatSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchattenclownBot.Model.Discord.ChoiceProvider;
using SchattenclownBot.Model.Objects;

namespace SchattenclownBot.Model.Discord.Interaction
{
    /// <summary>
    /// The slash commands.
    /// </summary>
    internal class Slash : ApplicationCommandsModule
    {
        /// <summary>
        /// Send the help of this bot.
        /// </summary>
        /// <param name="ic">The interaction context.</param>
        [SlashCommand("help", "Schattenclown Help", true)]
        public static async Task HelpAsync(InteractionContext ic)
        {
            await ic.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder()
            {
                Title = "Help",
                Description = "This is the command help for the Schattenclown Bot",
                Color = new DiscordColor(245, 107, 0)
            };
            eb.AddField("/invite", "Send´s an invite link!");
            eb.AddField("/Timer", "Set´s a Timer!");
            eb.WithAuthor("Schattenclown help");
            eb.WithFooter("(✿◠‿◠) thanks for using me");
            eb.WithTimestamp(DateTime.Now);

            await ic.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(eb.Build()));
        }

        [SlashCommand("timer", "Set a timer", true)]
        public static async Task Timer(InteractionContext ic, [ChoiceProvider(typeof(HoursChoiceProvider))][Option("HOURS", "HOURS")] string hours, [ChoiceProvider(typeof(MinutesChoiceProvider))][Option("MINUTES", "MINUTES")] string minutes)
        {
            await ic.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Creating Timer..."));

            var hoursChoiceProdiver = new HoursChoiceProvider();
            var hoursChoices = await hoursChoiceProdiver.Provider();

            var minutesChoiceProvider = new MinutesChoiceProvider();
            var minutesChoices = await minutesChoiceProvider.Provider();

            DateTime dateTimeNow = DateTime.Now;
            ScTimer timer = new ScTimer
            {
                ChannelId = ic.Channel.Id,
                MemberId = ic.Member.Id
            };

            int addHours = Convert.ToInt32(hoursChoices.First(c => c.Value.ToString() == hours).Name);
            timer.NotificationTime = dateTimeNow.AddHours(addHours);

            int addMinutes = Convert.ToInt32(minutesChoices.First(c => c.Value.ToString() == minutes).Name);
            timer.NotificationTime = timer.NotificationTime.AddMinutes(addMinutes);
                   
            ScTimer.Add(timer);

            await ic.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Timer set for {timer.NotificationTime}!"));
        }

        /// <summary>
        /// Generates an Invite link.
        /// </summary>
        /// <param name="ic">The ic.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("invite", "Invite ListforgeNotify", true)]
        public static async Task InviteAsync(InteractionContext ic)
        {
            await ic.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            var bot_invite = ic.Client.GetInAppOAuth(Permissions.Administrator);

            await ic.EditResponseAsync(new DiscordWebhookBuilder().WithContent(bot_invite.AbsoluteUri));
        }

        /// <summary>
        /// Gets the user's avatar & banner.
        /// </summary>
        /// <param name="ctx">The contextmenu context.</param>
        [ContextMenu(ApplicationCommandType.User, "Get avatar & banner")]
        public static async Task GetUserBannerAsync(ContextMenuContext ctx)
        {
            var user = await ctx.Client.GetUserAsync(ctx.TargetUser.Id, true);

            var eb = new DiscordEmbedBuilder
            {
                Title = $"Avatar & Banner of {user.Username}",
                ImageUrl = user.BannerHash != null ? user.BannerUrl : null
            }.
            WithThumbnail(user.AvatarUrl).
            WithColor(user.BannerColor ?? DiscordColor.Aquamarine).
            WithFooter($"Requested by {ctx.Member.DisplayName}", ctx.Member.AvatarUrl).
            WithAuthor($"{user.Username}", user.AvatarUrl, user.AvatarUrl);
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(eb.Build()));
        }
    }
}
