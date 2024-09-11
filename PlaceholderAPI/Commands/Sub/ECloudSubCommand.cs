namespace PlaceholderAPI.Commands.Sub
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using PlaceholderAPI.Cloud;
    using PlaceholderAPI.Cloud.Beans;
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
            if (arguments.Count < 1)
            {
                response = "[PAPI] You need to input the subcommand: forceupdate, download.";
                return false;
            }

            switch (arguments.At(0))
            {
                case "download":
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

                    if (arguments.Count < 2 || !ECloudDatabase.TryGetExpansion(arguments.At(1), out ECloudExpansion expansion))
                    {
                        response = "[PAPI] The repository name is missing.";
                        return false;
                    }

                    if (expansion.Hidden)
                    {
                        response = "[PAPI] The repository name is missing.";
                        return false;
                    }

                    if (expansion.Internal)
                    {
                        response = "[PAPI] The repository is internal so you don't need to download it.";
                        return false;
                    }

                    if (!expansion.Verified && !PlaceholderAPIPlugin.Instance.Config.DownloadUnsafeFromEcloud)
                    {
                        response = "[PAPI] The repository is unsafe.";
                        return false;
                    }

                    ECloudDownloader.Download(expansion.RepoId);

                    response = "[PAPI] Your Expansion is being downloaded";
                    return true;

                case "refresh":
                    if (!sender.CheckPermission("papi.ecloud.refresh"))
                    {
                        response = "[PAPI] You don't have enough permission";
                        return false;
                    }

                    if (!PlaceholderAPIPlugin.Instance.Config.ConnectToEcloud)
                    {
                        response = "[PAPI] The ECloud is disabled from the Config";
                        return false;
                    }


                    ECloudDatabase.ClearCache();

                    ECloudDatabase.UpdateData();
                    response = "[PAPI] Cache has been refreshed";
                    return true;

                case "clear":
                    if (!sender.CheckPermission("papi.ecloud.clear"))
                    {
                        response = "[PAPI] You don't have enough permission";
                        return false;
                    }

                    if (!PlaceholderAPIPlugin.Instance.Config.ConnectToEcloud)
                    {
                        response = "[PAPI] The ECloud is disabled from the Config";
                        return false;
                    }


                    ECloudDatabase.ClearCache();

                    response = "[PAPI] Cache has been cleared";
                    return true;

                case "info":
                    if (!sender.CheckPermission("papi.ecloud.info"))
                    {
                        response = "[PAPI] You don't have enough permission";
                        return false;
                    }

                    if (!PlaceholderAPIPlugin.Instance.Config.ConnectToEcloud)
                    {
                        response = "[PAPI] The ECloud is disabled from the Config";
                        return false;
                    }

                    if (arguments.Count < 2 || !ECloudDatabase.TryGetExpansion(arguments.At(1), out ECloudExpansion expansion2))
                    {
                        response = "[PAPI] The repository name is missing.";
                        return false;
                    }

                    response = $"\nInformation retrived for {expansion2.Id}:" +
                               $"\n  Verified: {(expansion2.Verified ? "Yes" : "NO")}" +
                               $"\n  Link: {expansion2.GitHub}" +
                               $"\n  Owner: {expansion2.DiscordId}" +
                               $"\n  Internal: {(expansion2.Internal ? "Yes" : "NO")}";
                    return true;



                default:
                    response = "[PAPI] This subcommand doesn't exist";
                    return false;
            }
        }
    }
}
