namespace Emojify;

public static class EmojiExtensions
{
    // Extension method for int
    public static string Emojify(this int codePoint)
    {
        throw new NotImplementedException("Implement me!");
    }

    // Extension method for string
    public static string Emojify(this string emojiName)
    {
        if (string.IsNullOrWhiteSpace(emojiName))
            return null;

        // Try to parse the string to an Emoji enum value
        if (Enum.TryParse(typeof(Emoji), emojiName, true, out var result))
        {
            var emoji = (Emoji)result;
            return emoji.GetEmojiString();
        }
        else
        {
            // Optionally, handle cases where the name is not found
            Console.WriteLine($"Emoji '{emojiName}' not found.");
            return null;
        }
    }

    // Extension method for Emoji enum
    public static string GetEmojiString(this Emoji emoji)
    {
        var codePoints = GetCodePoints(emoji);
        if (codePoints == null)
            return null;

        string result = "";
        foreach (var codePoint in codePoints)
        {
            result += char.ConvertFromUtf32(codePoint);
        }
        return result;
    }

    // Helper method to get code points from the Emoji enum
    private static int[] GetCodePoints(Emoji emoji)
    {
        var type = emoji.GetType();
        var memberInfo = type.GetMember(emoji.ToString());
        if (memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(EmojiCodePointsAttribute), false);
            if (attrs.Length > 0)
            {
                return ((EmojiCodePointsAttribute)attrs[0]).CodePoints;
            }
        }
        return null;
    }
}