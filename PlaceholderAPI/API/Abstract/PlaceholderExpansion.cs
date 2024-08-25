namespace PlaceholderAPI.API.Abstract
{
    using System.Linq;
    using Exiled.API.Features;
    using Exiled.Loader;

    /// <summary>
    /// Base method for Implementing Placeholders on plugins.
    /// </summary>
    public abstract class PlaceholderExpansion
    {
        /// <summary>
        /// Gets or sets the author of the Placeholder.
        /// </summary>
        public abstract string Author { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the Placeholder.
        /// </summary>
        public abstract string Identifier { get; set; }

        /// <summary>
        /// Gets or sets plugin Required for registering the Placeholder.
        /// </summary>
        public abstract string RequiredPlugin { get; set; }

        /// <summary>
        /// Checks if the Placeholder is registered.
        /// </summary>
        /// <returns>if the Placeholder is registered.</returns>
        public bool IsRegistered()
        {
            return (this.RequiredPlugin != null && Loader.Plugins.Any(x => x.Name == this.RequiredPlugin)) && PlaceholderAPI.Placeholders.TryGetValue(this.Identifier, out _);
        }

        /// <summary>
        /// Check if the Placeholder can be registered.
        /// </summary>
        /// <returns>Returns if it can be registered.</returns>
        public virtual bool CanRegister()
        {
            return !PlaceholderAPI.Placeholders.TryGetValue(this.Identifier, out _);
        }

        /// <summary>
        /// The method that controlls the placeholder.
        /// </summary>
        /// <param name="player">The player that is needed for the placeholder.</param>
        /// <param name="param">The parameters passed by the identifier.</param>
        /// <returns>the placeholder modified.</returns>
        public virtual string OnRequest(Player player, string param)
        {
            return null;
        }

        /// <summary>
        /// The method that controlls the placeholder.
        /// </summary>
        /// <param name="param">The parameters passed by the identifier.</param>
        /// <returns>the placeholder modified.</returns>
        public virtual string OnOfflineRequest(string param)
        {
            return null;
        }

        /// <summary>
        /// Registers the Placeholder.
        /// </summary>
        /// <returns>If the placeholder is registered.</returns>
        public bool Register()
        {
            if (!this.CanRegister())
            {
                return false;
            }

            if (PlaceholderAPI.Placeholders.TryGetValue(this.Identifier, out _))
            {
                return false;
            }

            Log.Info($"New Placeholder ({this.Identifier}) made by {this.Author} has been registered.");
            PlaceholderAPI.Placeholders.Add(this.Identifier, this);
            return true;
        }

        /// <summary>
        /// Registers the Placeholder.
        /// </summary>
        /// <returns>If the placeholder is registered.</returns>
        public bool Unregister()
        {
            if (!this.IsRegistered())
            {
                return false;
            }

            PlaceholderAPI.Placeholders.Remove(this.Identifier);
            return true;
        }
    }
}
