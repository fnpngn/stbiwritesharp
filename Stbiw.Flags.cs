namespace StbiwSharp
{
    public enum ImageChannels : int
    {
        Y = 1,
        Ya = 2,
        Rgb = 3,
        Rgba = 4
    }

    public enum StbiFlip : int
    {
        None = 0,
        FlipVertically = 1
    }

    public enum TgaRle : int
    {
        Disabled = 0,
        Enabled = 1
    }

    public enum PngFilter : int
    {
        Disabled = -1,
        FilterMode0 = 0,
        FilterMode1 = 1,
        FilterMode2 = 2,
        FilterMode3 = 3,
        FilterMode4 = 4,
        FilterMode5 = 5,
    }

    public enum PngCompressionLevel
    {
        Compression0 = 0,
        Compression1 = 1,
        Compression2 = 2,
        Compression3 = 3,
        Compression4 = 4,
        Compression5 = 5,
        Compression6 = 6,
        Compression7 = 7,
        Compression8 = 8,
    }
}
