namespace PlaceholderAPI.Placeholders
{
    using Exiled.API.Features;
    using Exiled.API.Features.Roles;
    using PlaceholderAPI.API.Abstract;

    /// <summary>
    /// Implementation of the Player Placeholders.
    /// </summary>
    public class Scp079Placeholder : PlaceholderExpansion
    {
        /// <inheritdoc/>
        public override string Author { get; set; } = "NotZer0Two";

        /// <inheritdoc/>
        public override string Identifier { get; set; } = "scp079";

        /// <inheritdoc/>
        public override string RequiredPlugin { get; set; } = null;

        /// <inheritdoc/>
        public override string OnRequest(Player player, string param)
        {
            if (player.Role is not Scp079Role role)
            {
                return null;
            }

            switch (param.ToLower())
            {
                case "experience":
                    return role.Experience.ToString();
                case "level":
                    return role.Level.ToString();
                case "untilnextlevel":
                    return role.NextLevelThreshold.ToString();
                case "energy":
                    return role.Energy.ToString();
                case "maxenergy":
                    return role.MaxEnergy.ToString();
                case "roomlockdowncooldown":
                    return role.RoomLockdownCooldown.ToString();
                case "roomlockdown":
                    return role.RemainingLockdownDuration.ToString();
                case "blackouts":
                    return role.BlackoutCount.ToString();
                case "maxblackouts":
                    return role.BlackoutCapacity.ToString();
                case "blackoutzonecooldown":
                    return role.BlackoutZoneCooldown.ToString();
                case "haslostsignal":
                    return role.IsLost.ToString();
                case "lostsignal":
                    return role.LostTime.ToString();
                case "energyregeneration":
                    return role.EnergyRegenerationSpeed.ToString();
            }

            return null;
        }
    }
}
