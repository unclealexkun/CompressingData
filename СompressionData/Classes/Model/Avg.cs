using System.Collections.Generic;

namespace СompressionData.Classes.Model
{
    public class Avg
    {
        public Pixel X;
        public Pixel Y;

        public List<Vector> Elements = new List<Vector>();

        public Avg(Pixel x, Pixel y)
        {
            X = x;
            Y = y;
        }

        public Avg()
        {
            X = new Pixel();
            Y = new Pixel();
        }
    }
}