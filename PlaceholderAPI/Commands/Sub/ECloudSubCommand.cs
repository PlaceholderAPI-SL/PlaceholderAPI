namespace PlaceholderAPI.Commands.Sub
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using PlaceholderAPI.Cloud;
    using PlaceholderAPI.Cloud.Helper;

    /// <summary>
    /// ECloud Support command.
    /// </summary>
    public class ECloudSubCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; } = "ecloud";

        /// <inheritdoc />
        public string[] Aliases { get; } = ["cloud"];

        /// <inheritdoc />
        public string Description { get; } = "Online database for PlaceholderAPI";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("papi.ecloud.download"))
            {
                response = "[PAPI] You don't have enough permission";
                return false;
            }

            if (!PlaceholderAPIPlugin.Instance.Config.ConnectToEcloud)
            {
                response = "[PAPI] The ECloud is disabled from the Config";
                return false;
            }

            ECloudDatabase.UpdateData();

            if (arguments.Count < 0 || !ECloudDatabase.TryGetUrl(arguments.At(0), out long id))
            {
                response = "[PAPI] The repository name is missing.";
                return false;
            }

            ECloudDownloader.Download(id);

            response = "[PAPI] Your Expansion is being downloaded";
            return true;
        }
    }
}
