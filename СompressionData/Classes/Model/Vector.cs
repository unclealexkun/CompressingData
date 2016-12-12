namespace СompressionData.Classes.Model
{
    public class Vector
    {
        public Pixel X;
        public Pixel Y;
        public Vector(Pixel x, Pixel y)
        {
            X = x;
            Y = y;
        }

        public Vector()
        {
            X = new Pixel();
            Y = new Pixel();
        }
    }
}