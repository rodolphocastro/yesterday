using System;

namespace Api.Models
{
    /// <summary>
    /// User profile for Yesterday.
    /// </summary>
    public class Profile : IUpdatableFrom<Profile>
    {
        public string Id { get; internal set; }
        public string Nickname { get; set; }
        // TODO: Add Social Medias

        public Profile ApplyChanges(Profile from)
        {
            if (from is null)
            {
                throw new ArgumentNullException(nameof(from));
            }

            Nickname = from.Nickname;
            return this;
        }
    }
}
