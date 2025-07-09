namespace PlaceholderAPI.API
{
    using Exiled.API.Features;

    /// <summary>
    /// Used to identify Relational Placeholders.
    /// </summary>
    public interface IRelational
    {

        /// <summary>
        /// The method that controlls the placeholder.
        /// </summary>
        /// <param name="one">The player that is needed for the placeholder.</param>
        /// <param name="two">The second player that is needed for the placeholder.</param>
        /// <param name="param">The parameters passed by the identifier.</param>
        /// <returns>the placeholder modified.</returns>
        public string OnRequest(Player one, Player two, string param);
    }
}
