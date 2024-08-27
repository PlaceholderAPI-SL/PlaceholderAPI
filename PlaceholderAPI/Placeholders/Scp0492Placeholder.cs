namespace PlaceholderAPI.Placeholders
{
    using Exiled.API.Features;
    using Exiled.API.Features.Roles;
    using PlaceholderAPI.API.Abstract;
    using System.Linq;

    /// <summary>
    /// Implementation of the Player Placeholders.
    /// </summary>
    public class Scp0492Placeholder : PlaceholderExpansion
    {
        /// <inheritdoc/>
        public override string Author { get; set; } = "NotZer0Two";

        /// <inheritdoc/>
        public override string Identifier { get; set; } = "scp0492";

        /// <inheritdoc/>
        public override string RequiredPlugin { get; set; } = null;

        /// <inheritdoc/>
        public override string OnRequest(Player player, string param)
        {
            if (player.Role is not Scp0492Role role)
            {
                return null;
            }

            switch (param.ToLower())
            {
                case "resurrected":
                    return role.ResurrectNumber.ToString();
                case "isbloodlust":
                    return role.BloodlustActive.ToString();
                case "isconsuming":
                    return role.IsConsuming.ToString();
                case "attackcooldown":
                    return role.AttackCooldown.ToString();
            }

            return null;
        }
    }
}
