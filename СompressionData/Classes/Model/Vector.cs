namespace СompressionData.Classes.Model
{
    public class Vector
    {
        public float X;
        public float Y;

        public byte Alpha;

        public byte R;
        public byte G;
        public byte B;

        public Vector(float x, float y, byte r, byte g, byte b, byte alpha)
        {
            X = x;
            Y = y;
            R = r;
            G = g;
            B = b;
            Alpha = alpha;
        }

        public Vector()
        {
            X = 0;
            Y = 0;
            R = 0;
            G = 0;
            B = 0;
            Alpha = 0;
        }
    }
}