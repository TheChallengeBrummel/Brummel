using System.Collections.Generic;
using SkiaSharp;

namespace Mobile.Views
{
    internal class ColorProvider
    {
        private int lastIndex = 0;
        private readonly List<SKColor> colors;

        public ColorProvider()
        {
            colors = new List<SKColor>()
            {
                SKColor.Parse("#54A2FF"),
                SKColor.Parse("#4DE89D"),
                SKColor.Parse("#CDFF61"),
                SKColor.Parse("#E8BB4D"),
                SKColor.Parse("#FF714B"),
            };
        }

        public SKColor GetNextColor()
        {
            return colors[++lastIndex % colors.Count];
        }
    }
}