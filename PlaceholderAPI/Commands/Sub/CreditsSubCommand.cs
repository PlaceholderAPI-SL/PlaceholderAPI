namespace PlaceholderAPI.Commands.Sub
{
    using System;
    using CommandSystem;

    /// <summary>
    /// Credits Command.
    /// </summary>
    public class CreditsSubCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; } = "credits";

        /// <inheritdoc />
        public string[] Aliases { get; } = [];

        /// <inheritdoc />
        public string Description { get; } = "Credits for PlaceholderAPI";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = $"Welcome!\nPlaceholderAPI ({PlaceholderAPIPlugin.Instance.Version}v) Made by {PlaceholderAPIPlugin.Instance.Author}\nUsage:\npapi ecloud Name\npapi credits";
            return true;
        }
    }
}
