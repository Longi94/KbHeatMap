using System;
using System.Windows.Media;

namespace KbHeatMap.Model
{
    public class ChromaColor
    {

        public static readonly ChromaColor Black = new ChromaColor(0x000000);
        public static readonly ChromaColor Red = new ChromaColor(0xFF0000);
        public static readonly ChromaColor Green = new ChromaColor(0x00FF00);
        public static readonly ChromaColor Blue = new ChromaColor(0x0000FF);
        public static readonly ChromaColor HotPink = new ChromaColor(0xFF69B4);
        public static readonly ChromaColor Orange = new ChromaColor(0xFFA500);
        public static readonly ChromaColor Magenta = new ChromaColor(0xFF00FF);
        public static readonly ChromaColor Cyan = new ChromaColor(0x00FFFF);
        public static readonly ChromaColor Purple = new ChromaColor(0x800080);
        public static readonly ChromaColor White = new ChromaColor(0xFFFFFF);
        public static readonly ChromaColor Yellow = new ChromaColor(0xFFFF00);

        public Color Color;

        public bool IsKey;

        public ChromaColor(int color)
        {
            byte[] bytes = BitConverter.GetBytes(color);
            Color = Color.FromRgb(bytes[2], bytes[1], bytes[0]);
        }

        public ChromaColor(Color color)
        {
            Color = color;
        }

        public uint ToBgr()
        {

            uint r = Color.R;
            uint g = Color.G;
            uint b = Color.B;

            var bgr = (b << 16) | (g << 8) | r;

            if (IsKey)
            {
                bgr |= 0xFF000000;
            }

            return bgr;
        }

        public string ToHex() => $"#{Color.R:X2}{Color.G:X2}{Color.B:X2}";
    }
}
