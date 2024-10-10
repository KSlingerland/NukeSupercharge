namespace Emojify
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EmojiCodePointsAttribute : Attribute
    {
        public int[] CodePoints { get; }

        public EmojiCodePointsAttribute(params int[] codePoints)
        {
            CodePoints = codePoints;
        }
    }
    
    public enum Emoji
    {
        [EmojiCodePoints(0x1F600)]
        Grinning,

        [EmojiCodePoints(0x1F601)]
        Beaming,

        [EmojiCodePoints(0x1F602)]
        TearsOfJoy,

        [EmojiCodePoints(0x1F603)]
        Smiling,

        [EmojiCodePoints(0x1F604)]
        Laughing,

        [EmojiCodePoints(0x1F605)]
        SweatSmile,

        [EmojiCodePoints(0x1F606)]
        Squinting,

        [EmojiCodePoints(0x1F923)]
        Rolling,

        [EmojiCodePoints(0x1F60D)]
        HeartEyes,

        [EmojiCodePoints(0x1F618)]
        KissingHeart,
        
        [EmojiCodePoints(0x1F1FA, 0x1F1F8)]
        UnitedStates,

        [EmojiCodePoints(0x1F1EC, 0x1F1E7)]
        UnitedKingdom,

        [EmojiCodePoints(0x1F1E9, 0x1F1EA)]
        Germany,

        [EmojiCodePoints(0x1F1E8, 0x1F1F3)]
        China,

        [EmojiCodePoints(0x1F1EF, 0x1F1F5)]
        Japan,

        [EmojiCodePoints(0x1F1F7, 0x1F1FA)]
        Russia,

        [EmojiCodePoints(0x1F1E6, 0x1F1FA)]
        Australia,

        [EmojiCodePoints(0x1F1E7, 0x1F1F7)]
        Brazil,
    }
}