namespace PlaceholderAPI.Placeholders
{
    using Exiled.API.Features;
    using PlaceholderAPI.API.Abstract;

    /// <summary>
    /// Implementation of the Player Placeholders.
    /// </summary>
    public class PlayerPlaceholders : PlaceholderExpansion
    {
        /// <inheritdoc/>
        public override string Author { get; set; } = "NotZer0Two";

        /// <inheritdoc/>
        public override string Identifier { get; set; } = "player";

        /// <inheritdoc/>
        public override string RequiredPlugin { get; set; } = null;

        /// <inheritdoc/>
        public override string OnRequest(Player player, string param)
        {
            switch (param.ToLower())
            {
                case "name":
                    return player.Nickname;
                case "displayname":
                    return player.DisplayNickname;
                case "ip":
                    return player.IPAddress;
                case "dnt":
                    return player.DoNotTrack.ToString();
                case "health":
                    return player.Health.ToString();
                case "maxhealth":
                    return player.MaxHealth.ToString();
                case "ping":
                    return player.Ping.ToString();
                case "id":
                    return player.RawUserId;
                case "fullid":
                    return player.UserId;
                case "auth":
                    return player.AuthenticationType.ToString();
                case "iswhitelisted":
                    return player.IsWhitelisted.ToString();
                case "hasremoteadmin":
                    return player.RemoteAdminAccess.ToString();
                case "velocity":
                    return player.Velocity.ToString();
                case "team":
                    return player.Role.Team.ToString();
                case "role":
                    return player.Role.Type.ToString();
                case "leadingteam":
                    return player.LeadingTeam.ToString();
                case "iscuffed":
                    return player.IsCuffed.ToString();
                case "x":
                    return player.Position.x.ToString();
                case "y":
                    return player.Position.y.ToString();
                case "z":
                    return player.Position.z.ToString();
            }

            return null;
        }
    }
}
