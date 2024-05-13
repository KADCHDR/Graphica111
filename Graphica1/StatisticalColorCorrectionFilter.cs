using System;
using System.Drawing;

namespace Graphica1
{
    public class StatisticalColorCorrectionFilter : Filters
    {
        private double meanR, meanG, meanB;
        private double stdDevR, stdDevG, stdDevB; // отклонения средние и стандартные для каждого цветового канала

        public StatisticalColorCorrectionFilter(double meanR, double meanG, double meanB, double stdDevR, double stdDevG, double stdDevB)
        {
            this.meanR = meanR;
            this.meanG = meanG;
            this.meanB = meanB;
            this.stdDevR = stdDevR;
            this.stdDevG = stdDevG;
            this.stdDevB = stdDevB; // инициализация средних и стандарного отклонения
        }

        public override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);

            int r = Clamp((int)((sourceColor.R - meanR) * (stdDevR / 128) + meanR), 0, 255); //вычисление новых значений пикселя  Они вычитают среднее значение для каждого канала из значения пикселя, умножают на отношение стандартного отклонения к 128 (это может быть коэффициент коррекции, который можно настроить), и добавляют среднее значение обратно. Результат округляется до ближайшего целого числа с помощью приведения
            int g = Clamp((int)((sourceColor.G - meanG) * (stdDevG / 128) + meanG), 0, 255);
            int b = Clamp((int)((sourceColor.B - meanB) * (stdDevB / 128) + meanB), 0, 255);

            return Color.FromArgb(r, g, b);
        }
    }
}