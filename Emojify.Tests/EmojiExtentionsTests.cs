namespace Emojify.Tests
{
    public class EmojiExtensionsTests
    {
        [TestCase("Grinning", "😀")]
        [TestCase("Beaming", "😁")]
        [TestCase("TearsOfJoy", "😂")]
        [TestCase("Smiling", "😃")]
        [TestCase("Laughing", "😄")]
        [TestCase("SweatSmile", "😅")]
        [TestCase("Squinting", "😆")]
        [TestCase("Rolling", "🤣")]
        [TestCase("HeartEyes", "😍")]
        [TestCase("KissingHeart", "😘")]
        [TestCase("UnitedStates", "🇺🇸")]
        [TestCase("UnitedKingdom", "🇬🇧")]
        [TestCase("Germany", "🇩🇪")]
        [TestCase("China", "🇨🇳")]
        [TestCase("Japan", "🇯🇵")]
        [TestCase("Russia", "🇷🇺")]
        [TestCase("Australia", "🇦🇺")]
        [TestCase("Brazil", "🇧🇷")]
        [TestCase("UnknownEmoji", null)]
        public void StringEmojify_ShouldReturnCorrectEmoji(string emojiName, string? expected)
        {
            var result = emojiName.Emojify();
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void StringEmojify_ShouldReturnNull_WhenEmptyString()
        {
            var result = string.Empty.Emojify();
            Assert.IsNull(result);
        }

        [Test]
        public void StringEmojify_ShouldReturnNull_WhenNullString()
        {
            string input = null;
            var result = input.Emojify();
            Assert.IsNull(result);
        }

        [Test]
        public void IntEmojify_ShouldThrowNotImplementedException()
        {
            int codePoint = 0x1F600;
            Assert.Throws<NotImplementedException>(() => codePoint.Emojify());
        }

        [TestCase(Emoji.Grinning, "😀")]
        [TestCase(Emoji.Beaming, "😁")]
        [TestCase(Emoji.HeartEyes, "😍")]
        [TestCase(Emoji.UnitedStates, "🇺🇸")]
        public void GetEmojiString_ShouldReturnCorrectEmoji(Emoji emoji, string expected)
        {
            var result = emoji.GetEmojiString();
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetEmojiString_ShouldReturnNull_WhenNoCodePoints()
        {
            var emoji = (Emoji)(-1);
            var result = emoji.GetEmojiString();
            Assert.IsNull(result);
        }
    }
}