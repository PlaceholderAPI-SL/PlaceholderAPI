﻿namespace PlaceholderAPI.Commands.Sub
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

                case "forceupdate":
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


                    ECloudDatabase.ClearCache();

                    ECloudDatabase.UpdateData();
                    response = "[PAPI] Cache is cleaned";
                    return true;

                default:
                    response = "[PAPI] This subcommand doesn't exist";
                    return false;
            }
        }
    }
}
