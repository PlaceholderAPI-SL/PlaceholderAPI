namespace PlaceholderAPI.Placeholders
{
    using Exiled.API.Features;
    using Exiled.API.Features.Roles;
    using PlaceholderAPI.API.Abstract;
    using System.Linq;

    /// <summary>
    /// Implementation of the Player Placeholders.
    /// </summary>
    public class Scp049Placeholder : PlaceholderExpansion
    {
        /// <inheritdoc/>
        public override string Author { get; set; } = "NotZer0Two";

        /// <inheritdoc/>
        public override string Identifier { get; set; } = "scp049";

        /// <inheritdoc/>
        public override string RequiredPlugin { get; set; } = null;

        /// <inheritdoc/>
        public override string OnRequest(Player player, string param)
        {
            if (player.Role is not Scp049Role role)
            {
                return null;
            }

            switch (param.ToLower())
            {
                case "isresurrecting":
                    return role.IsRecalling.ToString();
                case "iscalling":
                    return role.IsCallActive.ToString();
                case "zombies":
                    return role.DeadZombies.Count().ToString();
                case "callcooldown":
                    return role.CallCooldown.ToString();
                case "goodsensecooldown":
                    return role.RemainingGoodSenseDuration.ToString();
                case "attackcooldown":
                    return role.RemainingAttackCooldown.ToString();
                case "callduration":
                    return role.RemainingCallDuration.ToString();
            }

            return null;
        }
    }
}
