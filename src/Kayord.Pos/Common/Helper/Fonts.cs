namespace Kayord.Pos.Common.Helper;

public static class Fonts
{
    private static Stream? _font;
    public static Stream GetFont()
    {
        if (_font == null)
        {
            _font = File.OpenRead("assets/roboto.ttf");
        }
        return _font;
    }
}