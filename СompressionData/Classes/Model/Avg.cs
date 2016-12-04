using System.Collections.Generic;

namespace СompressionData.Classes.Model
{
    public class Avg
    {
        public float x;
        public float y;

        public List<Vector> elements = new List<Vector>();

        Avg(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        Avg()
        {
            x = 0;
            y = 0;
        }
    }
}