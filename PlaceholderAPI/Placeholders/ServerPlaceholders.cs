namespace PlaceholderAPI.Placeholders
{
    using Exiled.API.Features;
    using PlaceholderAPI.API.Abstract;

    /// <summary>
    /// Implementation of the Player Placeholders.
    /// </summary>
    public class ServerPlaceholders : PlaceholderExpansion
    {
        /// <inheritdoc/>
        public override string Author { get; set; } = "NotZer0Two";

        /// <inheritdoc/>
        public override string Identifier { get; set; } = "server";

        /// <inheritdoc/>
        public override string RequiredPlugin { get; set; } = null;

        /// <inheritdoc/>
        public override string OnOfflineRequest(string param)
        {
            switch (param.ToLower())
            {
                case "name":
                    return Server.Name;
                case "ip":
                    return Server.IpAddress;
                case "port":
                    return Server.Port.ToString();
                case "count":
                    return Server.PlayerCount.ToString();
                case "version":
                    return Server.Version;
                case "isverified":
                    return Server.IsVerified.ToString();
                case "tps":
                    return Server.Tps.ToString();
                case "maxtps":
                    return ServerStatic.ServerTickrate.ToString();
                case "friendlyfire":
                    return Server.FriendlyFire.ToString();
                case "iswhitelisted":
                    return Server.IsWhitelisted.ToString();
            }

            return null;
        }
    }
}
