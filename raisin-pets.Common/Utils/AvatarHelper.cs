namespace raisin_pets.Common.Utils;

public static class AvatarHelper
{
    private const string BaseEndpoint = "https://eu.ui-avatars.com/api/";
    private static readonly string[] BackgroundColors =
    {
        "EC681E", // shade of orange
        "FDAB2F", // shade of orange
        "FD792F", // shade of orange
        "2F82FD", // shade of blue
        "2FCCFD", // shade of blue
        "2F75FD", // shade of blue
        "2F9AFD" // shade of blue
    };
    private static readonly Random RandomPicker = new();

    /// <summary>
    /// Create an avatar URI based on the name of the user.
    /// </summary>
    /// <param name="firstNames"> A string containing the user's first name(s). </param>
    /// <param name="lastNames"> A string containing the user's last name(s). </param>
    /// <returns> An URI of the user's avatar. </returns>
    public static string GetAvatar(
        string firstNames,
        string lastNames) => BaseEndpoint +
                             $"?size=512" +
                             $"&color=fff" +
                             $"&background={GetBackgroundColor()}" +
                             $"&length=2" +
                             $"&name={firstNames.First()}" + 
                             $"{lastNames.First()}";

    #region Private methods

    /// <summary>
    /// Get a random color from the list of background colors
    /// </summary>
    /// <returns> A hex without the "#" representing the color. </returns>
    private static string GetBackgroundColor() => BackgroundColors.ElementAt(RandomPicker.Next(BackgroundColors.Length));

    #endregion
}