using System.Collections.Generic;

namespace СompressionData.Classes.Model
{
    public class Avg
    {
        public float X;
        public float Y;

        public List<Vector> Elements = new List<Vector>();

        public Avg(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Avg()
        {
            X = 0;
            Y = 0;
        }
    }
}