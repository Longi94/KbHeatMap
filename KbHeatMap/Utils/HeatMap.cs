using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace KbHeatMap.Utils
{
    public class ColorHeatMap
    {
        public ColorHeatMap()
        {
            InitColorsBlocks();
        }

        private void InitColorsBlocks()
        {
            ColorsOfMap.AddRange(new[]
            {
                Color.FromRgb(0, 0, 0xFF), //Blue
                Color.FromRgb(0, 0xFF, 0xFF), //Cyan
                Color.FromRgb(0, 0xFF, 0), //Green
                Color.FromRgb(0xFF, 0xFF, 0), //Yellow
                Color.FromRgb(0xFF, 0, 0), //Red
            });
        }

        public Color GetColorForValue(double val, double maxVal)
        {
            double valPercent = val / maxVal; // value%

            if (valPercent >= 1)
            {
                return ColorsOfMap.Last();
            }

            double colorPercent = 1d / (ColorsOfMap.Count - 1); // % of each block of color. the last is the "100% Color"
            double blockOfColor = valPercent / colorPercent; // the integer part repersents how many block to skip
            int blockIdx = (int) Math.Truncate(blockOfColor); // Idx of 
            double valPercentResidual = valPercent - (blockIdx * colorPercent); //remove the part represented of block 
            double percentOfColor = valPercentResidual / colorPercent; // % of color of this block that will be filled

            Color cTarget = ColorsOfMap[blockIdx];
            Color cNext = ColorsOfMap[blockIdx + 1];

            var deltaR = cNext.R - cTarget.R;
            var deltaG = cNext.G - cTarget.G;
            var deltaB = cNext.B - cTarget.B;

            var r = cTarget.R + deltaR * percentOfColor;
            var g = cTarget.G + deltaG * percentOfColor;
            var b = cTarget.B + deltaB * percentOfColor;

            Color c = ColorsOfMap[0];
            try
            {
                c = Color.FromRgb((byte) r, (byte) g, (byte) b);
            }
            catch (Exception)
            {
                // ignored
            }

            return c;
        }

        public List<Color> ColorsOfMap = new List<Color>();
    }
}
