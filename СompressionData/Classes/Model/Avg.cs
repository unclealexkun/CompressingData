using System.Collections.Generic;

namespace СompressionData.Classes.Model
{
    public class Avg
    {
        public float x;
        public float y;

        public List<Vector> elements = new List<Vector>();

        public Avg(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Avg()
        {
            x = 0;
            y = 0;
        }
    }
}