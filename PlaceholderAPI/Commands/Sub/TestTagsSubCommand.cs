namespace PlaceholderAPI.Commands.Sub
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using PlaceholderAPI.API;

    /// <summary>
    /// Parsing command for testing.
    /// </summary>
    public class TestTagsSubCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; } = "parse";

        /// <inheritdoc />
        public string[] Aliases { get; } = ["test"];

        /// <inheritdoc />
        public string Description { get; } = "Parse PlaceholderAPI";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("papi.ecloud.test"))
            {
                response = "[PAPI] You don't have enough permission";
                return false;
            }

            response = $"[PAPI] {PlaceholderAPI.SetPlaceholders(Player.Get(sender), string.Join(" ", arguments))} %player_name% %server_ip%";
            return true;
        }
    }
}
