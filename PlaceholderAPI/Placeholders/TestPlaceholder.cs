namespace PlaceholderAPI.Placeholders
{
    using Exiled.API.Features;
    using PlaceholderAPI.API;
    using PlaceholderAPI.API.Abstract;


    /// <summary>
    /// Test Placeholder, yes what did you expect?
    /// </summary>
    public class TestPlaceholder : PlaceholderExpansion, IRelational
    {
        /// <inheritdoc/>
        public override string Author { get; set; } = "NotZer0Two";

        /// <inheritdoc/>
        public override string Identifier { get; set; } = "example";

        /// <inheritdoc/>
        public override string RequiredPlugin { get; set; } = null;

        /// <inheritdoc/>
        public string OnRequest(Player one, Player two, string param)
        {
            if (one == null || two == null)
                return null;

            if (param.Contains("hello"))
                return (one.Nickname == two.Nickname).ToString();

            return null;
        }
    }
}
