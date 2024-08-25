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
                case "count":
                    return Server.PlayerCount.ToString();
            }

            return null;
        }
    }
}
